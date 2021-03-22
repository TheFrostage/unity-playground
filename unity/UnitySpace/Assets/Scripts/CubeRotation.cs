using System;
using UnityEngine;

namespace UnitySpace
{
    public class CubeRotation : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1;
        private void Update()
        {
            transform.rotation *= Quaternion.Euler(0,Time.deltaTime * speed,0);
        }
    }
}