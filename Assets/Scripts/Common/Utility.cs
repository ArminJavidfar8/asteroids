using UnityEngine;

namespace Common.Extensions
{
    public static class Utility
    {
        public static Vector2 GetRandomVector2(float min, float max)
        {
            return new Vector2(Random.Range(min, max), Random.Range(min, max));
        }
    }
}