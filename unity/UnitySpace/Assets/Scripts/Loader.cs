using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnitySpace
{
    public class Loader : MonoBehaviour
    {
        private void Start()
        {
            var mainController = new GameObject("MainController").AddComponent<MainController>();
            Destroy(gameObject);
            SceneManager.LoadScene("Main");
            mainController.Init();
        }
    }
}