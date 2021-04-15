using Newtonsoft.Json;
using RNMessageSystem.ActionParams;
using UnitySpace;

namespace RNMessageSystem.Controllers
{
    [ControllerName("Scene")]
    public class RnSceneController : RnMessageController
    {
        [ActionName("Init")]
        private void Init(string parameters)
        {
            var sceneInitParams = JsonConvert.DeserializeObject<SceneInitParams>(parameters);
            MainController.Instance.SceneController.Init(sceneInitParams);
        }
    }
}