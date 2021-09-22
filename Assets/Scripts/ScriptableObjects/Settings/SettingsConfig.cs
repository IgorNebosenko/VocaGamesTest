using UnityEngine;

namespace Game.ScriptableObjects.Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Configs/Settings")]
    public class SettingsConfig : ScriptableObject
    {
        public float minValueAngle = -360f;
        public float maxValueAngle = 360f;
        [Space]
        public float minSpeed = 1f;
        public float maxSpeed = 5f;
        [Space]
        public float timeInterpolation = 1f;
    }
}