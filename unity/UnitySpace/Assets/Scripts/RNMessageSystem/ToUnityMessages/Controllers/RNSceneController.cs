using Newtonsoft.Json;
using RNMessageSystem.ToUnityMessages.ActionParams;
using UnitySpace;

namespace RNMessageSystem.ToUnityMessages.Controllers
{
    [ControllerName("Scene")]
    public class RnSceneController : RnMessageController
    {
        [ActionName("Init")]
        private void Init(string parameters)
        {
            var sceneInitParams = JsonConvert.DeserializeObject<SceneInitParams>(parameters);
            MainController.Instance.StartCoroutine(MainController.Instance.SceneController.Init(sceneInitParams));
        }

        [ActionName("Deinit")]
        private void Deinit(string parameters)
        {
            MainController.Instance.SceneController.Deinit();
        }
    }
}