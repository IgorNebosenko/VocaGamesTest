using Game.Singletones;
using System;
using UnityEngine;

namespace Game.Controllers
{
    public class InputController : MonoBehaviour
    {
        private KeyCode _keyCodeRespawn;

        public event Action buttonRespawnClicked;

        private void Start()
        {
            _keyCodeRespawn = GameManager.GameManagerInstance.SettingsConfig.buttonRespawn;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_keyCodeRespawn))
            {
                buttonRespawnClicked?.Invoke();
            }
        }
    }
}