using System.Collections.Generic;
using Controllers.SceneController.SpherePlacer;
using RNMessageSystem.ActionParams;
using UnityEngine;

namespace Controllers.SceneController
{
    public class SceneController
    {
        private List<SphereObject> _spheres;
        private SphereObject _spherePrefab;
        private SpherePlacer.SpherePlacer _spherePlacer;

        public SceneController()
        {
            _spherePrefab = Resources.Load<SphereObject>("Sphere");
            _spheres = new List<SphereObject>();
        }

        public void Init(SceneInitParams initParams)
        {
            _spherePlacer = new SpherePlacer.SpherePlacer(LocationingType.Table, Vector2.zero, initParams.Tracks.Count);

            foreach (var track in initParams.Tracks)
            {
                var sphere = Object.Instantiate(_spherePrefab);
                sphere.transform.position = _spherePlacer.GetNextPosition();
            }
        }

        public void Deinit() { }
    }
}