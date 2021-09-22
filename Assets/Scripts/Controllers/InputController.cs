using System;
using UnityEngine;

namespace Game.Controllers
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCodeRespawn = KeyCode.R;

        public event Action buttonRespawnClicked;

        private void Update()
        {
            if (Input.GetKeyDown(_keyCodeRespawn))
            {
                buttonRespawnClicked?.Invoke();
            }
        }
    }
}