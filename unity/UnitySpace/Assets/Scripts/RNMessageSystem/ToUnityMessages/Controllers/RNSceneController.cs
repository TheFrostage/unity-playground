using Newtonsoft.Json;
using RNMessageSystem.ToUnityMessages.ActionParams;
using UnitySpace;

namespace RNMessageSystem.ToUnityMessages.Controllers
{
    [ControllerName("Scene")]
    public class RnSceneController : RnMessageController
    {
        public RnSceneController()
        {
            _actions.Add("Init", Init);
            _actions.Add("Deinit", Deinit);
        }


        private void Init(string parameters)
        {
            var sceneInitParams = JsonConvert.DeserializeObject<SceneInitParams>(parameters);
            MainController.Instance.StartCoroutine(MainController.Instance.SceneController.Init(sceneInitParams));
        }
        
        private void Deinit(string parameters)
        {
            MainController.Instance.SceneController.Deinit();
        }
    }
}