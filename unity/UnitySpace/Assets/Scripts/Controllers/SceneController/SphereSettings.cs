using UnityEngine;

namespace Controllers.SceneController
{
    [CreateAssetMenu(fileName = "SphereSettings", menuName = "Kernelics/SphereSettings", order = 100)]
    public class SphereSettings : ScriptableObject
    {
        [Range(1, 1024)]
        public int textureSize;

        [Range(0.0001f, 10)]
        public float frequency;

        [Range(0.0001f, 10)]
        public float power;

        [Range(0.0001f, 10)]
        public float speed;

        public Material SphereMaterial;
    }
}