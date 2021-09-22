using Game.Controllers;
using Game.Core;
using Game.Core.Interfaces;
using Game.Core.MoveSystem;
using Game.ScriptableObjects.Abilities;
using Game.ScriptableObjects.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Singletones
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private GameplayController gameplayController;
        [Space]
        [SerializeField] private AblitiesConfig abilitiesConfig;
        [SerializeField] private SettingsConfig settingsConfig;

        private static GameManager gameManagerInstance;

        public static GameManager GameManagerInstance => gameManagerInstance;
        public InputController InputController => inputController;
        public SettingsConfig SettingsConfig => settingsConfig;

        private void Awake()
        {
            gameManagerInstance ??= this;
        }

        private void Start()
        {
            gameplayController.SetAbilities(abilitiesConfig.ListAbilities);
        }
    }
}