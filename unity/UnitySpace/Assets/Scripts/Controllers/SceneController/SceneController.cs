using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.SceneController.SpherePlacer;
using RNMessageSystem.ToUnityMessages.ActionParams;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Controllers.SceneController
{
    public class SceneController
    {
        public Camera MainCamera { get; }
        private readonly List<SphereObject> _spheres;
        private readonly SphereObject _spherePrefab;
        private SpherePlacer.SpherePlacer _spherePlacer;

        public SceneController()
        {
            _spherePrefab = Resources.Load<SphereObject>("Sphere");
            var mainCameraPrefab = Resources.Load<Camera>("MainCamera");
            MainCamera = Object.Instantiate(mainCameraPrefab);
            _spheres = new List<SphereObject>();
        }

        public IEnumerator Init(SceneInitParams initParams)
        {
            if (_spheres.Count > 0)
            {
                DestroySpheres();
            }

            _spherePlacer =
                new SpherePlacer.SpherePlacer(LocationingType.Table, Vector2.left * 3f + Vector2.up * 8, initParams.Tracks.Count);

            List<(string, Texture2D)> textures = new List<(string, Texture2D)>(initParams.Tracks.Count);

            foreach (var track in initParams.Tracks)
            {
                string link = string.Format(initParams.DownloadLink, track.Id);

                using (var request = UnityWebRequestTexture.GetTexture(link))
                {
                    yield return request.SendWebRequest();

                    if (request.isHttpError || request.isNetworkError)
                    {
                        Debug.LogError($"NetworkError + {request.error}");
                    }
                    else
                    {
                        var texture2D = DownloadHandlerTexture.GetContent(request);
                        texture2D.wrapMode = TextureWrapMode.Clamp;
                        textures.Add((track.Id, texture2D));
                    }
                }
            }

            foreach (var (id, texture2D) in textures)
            {
                var sphere = Object.Instantiate(_spherePrefab);
                sphere.Init(id, texture2D);
                sphere.transform.position = _spherePlacer.GetNextPosition();
                _spheres.Add(sphere);
            }

            SetMaterialSettings();
        }

        private void SetMaterialSettings()
        {
            var sphereSettings = Resources.Load<SphereSettings>("SphereSettings");

            var texture2D = new Texture2D(sphereSettings.textureSize, sphereSettings.textureSize, TextureFormat.RG16, false)
            {
                filterMode = FilterMode.Point, wrapMode = TextureWrapMode.Clamp
            };

            var noiseR = new Noise(DateTime.Now.Millisecond);
            var noiseG = new Noise(DateTime.Now.Millisecond / 2);

            for (int i = 0; i < sphereSettings.textureSize; i++)
            {
                for (int j = 0; j < sphereSettings.textureSize; j++)
                {
                    var rValue = noiseR.Evaluate(new Vector3(i, j) * sphereSettings.frequency) * sphereSettings.power;
                    var gValue = noiseG.Evaluate(new Vector3(i, j) * sphereSettings.frequency) * sphereSettings.power;

                    texture2D.SetPixel(i, j, new Color(rValue, gValue, 0f));
                }
            }

            texture2D.Apply();

            sphereSettings.SphereMaterial.SetTexture("_Noise", texture2D);
            sphereSettings.SphereMaterial.SetFloat("Speed", sphereSettings.speed);
            MaterialPropertyBlock props = new MaterialPropertyBlock();

            foreach (var sphere in _spheres)
            {
                var color = Random.ColorHSV(0,
                    1,
                    1,
                    1,
                    1,
                    1,
                    0.7f,
                    0.9f);

                Vector3 lightVector = (Vector3.up * Random.Range(-10f, 10f) +
                                       Vector3.right * Random.Range(-10f, 10f) +
                                       Vector3.back * 20).normalized;
                props.SetColor("_Color", color);
                props.SetVector("_LightVector", lightVector);
                sphere.SphereMeshRenderer.SetPropertyBlock(props);
            }
        }

        public void Deinit()
        {
            DestroySpheres();
        }

        private void DestroySpheres()
        {
            foreach (var sphere in _spheres)
            {
                Object.Destroy(sphere.gameObject);
            }

            _spheres.Clear();
        }
    }
}