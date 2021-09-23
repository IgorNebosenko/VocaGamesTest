using Game.Core.Ablilities;
using Game.Core.Interfaces;
using Game.ScriptableObjects.Abilities;
using Game.Events;
using System.Collections.Generic;
using UnityEngine;
using Game.Core.MoveSystem;

namespace Game.Controllers
{
    public class AbilitiesController : MonoBehaviour
    {
        private List<IAbility> _listAvailableAbilities;
        private float _reachedDistance;

        private DistanceReached _onDistanceReached;
        private SpeedReached _onSpeedReached;

        private PlayerController _player;
        private PlayerController _twin;

        private float _timeStartRun;
        private Queue<AbilityTwinData> _abilityTwinData;
        
        public Queue<AbilityTwinData> currentTwinData;

        public float TimeDelta => Time.time - _timeStartRun;

        private void Start()
        {
            _listAvailableAbilities = new List<IAbility>();
            _onDistanceReached = new DistanceReached();
            _onSpeedReached = new SpeedReached();

            _abilityTwinData = new Queue<AbilityTwinData>();
            currentTwinData = new Queue<AbilityTwinData>();
        }

        private void Update()
        {
            if (_abilityTwinData.Count > 0 && _abilityTwinData.Peek().releaseTime <= TimeDelta)
            {
                var ability = TranslateAbility(_abilityTwinData.Dequeue().data);
                ability.Execute(_twin, 0, true);
            }
        }

        public void SetAbilities(List<AbilityData> data)
        {
            foreach (var item in data)
            {
                var ability = TranslateAbility(item);
                if (ability == null)
                {
                    continue;
                }

                _listAvailableAbilities.Add(ability);
            }
        }

        public void Respawn()
        {
            _timeStartRun = Time.time;
            _reachedDistance = 0;

            foreach (var ability in _listAvailableAbilities)
            {
                ability.Respawn();
            }

            _abilityTwinData = currentTwinData;
            currentTwinData = new Queue<AbilityTwinData>();
        }

        public IAbility TranslateAbility(AbilityData data)
        {
            IAbility ability;
            switch (data.typeAbility)
            {
                case TypeAbility.ChangeCollor:
                    ability = new ChangeColorAbility();
                    break;
                default:
                    return null;
            }
            ability.Init(data);
            return ability;
        }

        public void InitPlayer(PlayerController player)
        {
            _player = player;

            foreach (var ability in _listAvailableAbilities)
            {
                switch (ability.AbilityData.triggerAbility)
                {
                    case TriggerAbility.DistanceCovered:
                        _onDistanceReached.AddListener(x => ability.Execute(_player, x, false));
                        break;
                    case TriggerAbility.Speed:
                        _onSpeedReached.AddListener(x => ability.Execute(_player, x, false));
                        break;
                    default:
                        Debug.LogError("Incorrect trigger of ability!");
                        break;
                }
            }
        }

        public void InitTwin(PlayerController player)
        {
            _twin = player;
        }

        public void ClearPlayerEvents()
        {
            foreach (var ability in _listAvailableAbilities)
            {
                switch (ability.AbilityData.triggerAbility)
                {
                    case TriggerAbility.DistanceCovered:
                        _onDistanceReached.RemoveListener(x => ability.Execute(_player, x, false));
                        break;
                    case TriggerAbility.Speed:
                        _onSpeedReached.RemoveListener(x => ability.Execute(_player, x, false));
                        break;
                    default:
                        Debug.LogError("Incorrect trigger of ability!");
                        break;
                }
            }
        }

        public void AddReachedDistance(float value)
        {
            _reachedDistance += value;
            _onDistanceReached?.Invoke(_reachedDistance);
        }

        public void SetSpeed(float speed)
        {
            _onSpeedReached?.Invoke(speed);
        }
    }
}