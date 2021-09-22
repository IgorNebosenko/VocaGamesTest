using Game.Controllers;
using Game.Core.Interfaces;
using Game.Core.MoveSystem;
using Game.ScriptableObjects.Abilities;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Singletones
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [Space]
        [SerializeField] private AblitiesConfig abilitiesConfig;

        private List<IAbility> _listAvailableAbilities;
        private Queue<IAbility> _queueLastAbilities;
        private Queue<MoveData> _queueMoves;

        private static GameManager gameManagerInstance;

        private void Start()
        {
            gameManagerInstance ??= this;
        }
    }
}