using UnityEngine;

namespace UnitySpace
{
    public class Loader : MonoBehaviour
    {
        private void Awake()
        {
            var mainController = new GameObject("MainController").AddComponent<MainController>();
            mainController.Init();
            Destroy(gameObject);
            
        }
    }
}