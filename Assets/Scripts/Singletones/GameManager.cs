using Game.Controllers;
using Game.ScriptableObjects.Abilities;
using Game.ScriptableObjects.Settings;
using UnityEngine;

namespace Game.Singletones
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private GameplayController gameplayController;
        [SerializeField] private AbilitiesController abilitiesController;
        [Space]
        [SerializeField] private AblitiesConfig abilitiesConfig;
        [SerializeField] private SettingsConfig settingsConfig;

        private static GameManager gameManagerInstance;

        public static GameManager GameManagerInstance => gameManagerInstance;
        public InputController InputController => inputController;
        public AbilitiesController AbilitiesController => abilitiesController;
        public SettingsConfig SettingsConfig => settingsConfig;

        private void Awake()
        {
            gameManagerInstance ??= this;
        }

        private void Start()
        {
            abilitiesController.SetAbilities(abilitiesConfig.ListAbilities);
            gameplayController.Init();
        }
    }
}