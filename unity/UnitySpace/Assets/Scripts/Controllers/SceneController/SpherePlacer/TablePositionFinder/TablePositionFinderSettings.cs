using UnityEngine;

namespace Controllers.SceneController.SpherePlacer.TablePositionFinder
{
    [CreateAssetMenu(fileName = "TablePositionFinderSettings",menuName = "Kernelics/TablePositionFinderSettings", order = 100)]
    public class TablePositionFinderSettings : ScriptableObject
    {
        public Vector2 Spacing;
        public int ElementsInARow;
    }
}