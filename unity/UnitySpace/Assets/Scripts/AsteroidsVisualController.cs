using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidsVisualController : MonoBehaviour
{
    [SerializeField]
    private Material _material;

    [SerializeField]
    private Texture2D _texture2D;

    [SerializeField, Range(1, 1024)]
    private int textureSize;

    [SerializeField, Range(0.0001f, 10)]
    private float frequency;

    [SerializeField, Range(0.0001f, 10)]
    private float power;

    [SerializeField, Range(0.0001f, 10)]
    private float speed;

    [SerializeField]
    private List<MeshRenderer> MeshRenderers;

    private void Start()
    {
        _texture2D = new Texture2D(textureSize, textureSize, TextureFormat.RG16, false)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };

        var noiseR = new Noise(DateTime.Now.Millisecond);
        var noiseG = new Noise(DateTime.Now.Millisecond / 2);

        for (int i = 0; i < textureSize; i++)
        {
            for (int j = 0; j < textureSize; j++)
            {
                var rValue = noiseR.Evaluate(new Vector3(i, j) * frequency) * power;
                var gValue = noiseG.Evaluate(new Vector3(i, j) * frequency) * power;

                _texture2D.SetPixel(i, j, new Color(rValue, gValue, 0f));
            }
        }

        _texture2D.Apply();

        _material.SetTexture("_Noise", _texture2D);
        _material.SetFloat("Speed", speed);

        MaterialPropertyBlock props = new MaterialPropertyBlock();

        foreach (var meshRenderer in MeshRenderers)
        {
            var color = Random.ColorHSV
            (
                0,
                1,
                1,
                1,
                1,
                1,
                0.7f,
                0.9f
            );

            Vector3 lightVector = (Vector3.up * Random.Range(-20f, 20f) +
                                  Vector3.right * Random.Range(-20f, 20f) +
                                  Vector3.back * 20).normalized;
            props.SetColor("_Color", color);
            props.SetVector("_LightVector", lightVector);
            meshRenderer.SetPropertyBlock(props);
        }
    }
}