using System;
using UnityEngine;

namespace Controllers.SceneController.SpherePlacer
{
    public class SpherePlacer
    {
        private int _positionNumber;

        private readonly IPositionFinder _positionFinder;

        public SpherePlacer(LocationingType locationingType, Vector2 startPosition, int elementsCount)
        {
            switch (locationingType)
            {
                case LocationingType.Table:
                    _positionFinder = new TablePositionFinder.TablePositionFinder(elementsCount, startPosition);
                    break;
                case LocationingType.Spiral:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(locationingType), locationingType, null);
            }
        }

        public Vector2 GetNextPosition()
        {
            return _positionFinder.GetPosition(_positionNumber);
        }

        public void Reset()
        {
            _positionNumber = 0;
        }
    }
}