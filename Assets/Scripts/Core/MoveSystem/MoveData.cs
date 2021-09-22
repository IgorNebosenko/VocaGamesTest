using Game.Singletones;
using UnityEngine;

namespace Game.Core.MoveSystem
{
    public class MoveData
    {
        public float angleForMove;
        public float speed;

        public static MoveData GenerateRandom()
        {
            var config = GameManager.GameManagerInstance.SettingsConfig;

            return new MoveData()
            {
                angleForMove = Random.Range(config.minValueAngle, config.maxValueAngle),
                speed = Random.Range(config.minSpeed, config.maxSpeed)
            };
        }
    }
}