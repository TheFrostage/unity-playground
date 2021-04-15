using RNMessageSystem;
using UnityEngine;

namespace UnitySpace
{
    public class CubeRotation : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1;

        private Vector3 _rotationVector = Vector3.right;

        private void Start()
        {
            UnityMessageManager.Instance.OnMessage += OnMessage;
        }

        private void OnMessage(string message)
        {
            switch (message)
            {
                case "right":
                    _rotationVector = Vector3.right;
                    UnityMessageManager.Instance.SendMessageToRN("Chanded rotation to right");

                    break;
                case "up":
                    _rotationVector = Vector3.up;

                    UnityMessageManager.Instance.SendMessageToRN("Chanded rotation to up");

                    break;
                case "forward":
                    _rotationVector = Vector3.forward;

                    UnityMessageManager.Instance.SendMessageToRN("Chanded rotation to forward");

                    break;
                default:
                    Debug.Log($"Incorrect message : {message}");

                    break;
            }
        }

        private void OnDestroy()
        {
            UnityMessageManager.Instance.OnMessage -= OnMessage;
        }

        private void Update()
        {
            var rotation = _rotationVector * Time.deltaTime * speed;
            transform.rotation *= Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            if (Input.touchCount > 0)
            {
                UnityMessageManager.Instance.SendMessageToRN("Touches count = " + Input.touchCount);
                Debug.Log("Detected touch");
            }
        }
    }
}