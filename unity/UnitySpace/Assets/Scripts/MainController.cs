using System;
using Controllers.SceneController;
using RNMessageSystem;
using UnityEngine;
using UnitySpace.Controllers;

namespace UnitySpace
{
    public class MainController : MonoBehaviour
    {
        public event Action Updated;

        public static MainController Instance => _instance;
        private static MainController _instance;

        public MessagesSystem RnMessagesSystem { get; private set; }
        public SceneController SceneController { get; private set; }
        public InputController InputController { get; private set; }

        public SceneClicker SceneClicker { get; private set; }

        public void Init()
        {
            _instance = this;
            DontDestroyOnLoad(this);

            RnMessagesSystem = new MessagesSystem();
            SceneController = new SceneController();
            InputController = new InputController();
            SceneClicker = new SceneClicker();

            RnMessagesSystem.Init();
            SceneClicker.Init(SceneController.MainCamera);
        }

        private void Update()
        {
            Updated?.Invoke();
        }

        public void Deinit()
        {
            RnMessagesSystem.Deinit();
            SceneController.Deinit();
            InputController.Deinit();
        }
    }
}