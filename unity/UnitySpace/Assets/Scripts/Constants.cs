using UnityEngine;

namespace UnitySpace
{
    public class Constants
    {
        public const float LongPressMinTime = 0.2f;
        public const float LongPressMaxTime = 0.5f;
        public const float LongPressOffsetTreshhold = 5;
        public static readonly int SpheresMask = 1 << LayerMask.NameToLayer("Spheres");
    }
}