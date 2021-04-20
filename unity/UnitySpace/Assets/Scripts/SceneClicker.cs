using Controllers.SceneController;
using RNMessageSystem.ToRNMessages;
using UnityEngine;

namespace UnitySpace
{
    public class SceneClicker
    {
        private Camera _camera;
        public void Init(Camera camera)
        {
            _camera = camera;
            MainController.Instance.InputController.LongPressed += OnLongPressed;
        }

        private void OnLongPressed(Vector2 position)
        {
            var ray = _camera.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out var hit, Constants.SpheresMask))
            {
                var sphereId = hit.transform.GetComponent<SphereObject>().Id;
                MainController.Instance.RnMessagesSystem.SendMessageToRN("InputInfo", new LongPressData
                {
                    TargetObject = sphereId
                });
            }
        }

        public void Deinit()
        {
            MainController.Instance.InputController.LongPressed -= OnLongPressed;
        }
    }
}