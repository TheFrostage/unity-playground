using UnityEngine;

namespace Controllers.SceneController.SpherePlacer
{
    public interface IPositionFinder
    {
        Vector2 GetPosition(int sphereNumber);
    }
}