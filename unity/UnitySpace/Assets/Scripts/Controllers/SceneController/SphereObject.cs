using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.SceneController
{
    public class SphereObject : MonoBehaviour
    {
        public MeshRenderer SphereMeshRenderer => _sphereMeshRenderer;
        public string Id { get; private set; }

        [SerializeField]
        private MeshRenderer _trackMeshRenderer;

        [SerializeField]
        private MeshRenderer _sphereMeshRenderer;

        private int _widthHeightValue = 300;

        public void Init(string id, Texture2D texture2D)
        {
            Id = id;
            _trackMeshRenderer.material.mainTexture = texture2D;
        }
    }
}