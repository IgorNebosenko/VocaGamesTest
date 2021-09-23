using Game.Controllers;
using Game.Core.Interfaces;
using Game.Core.MoveSystem;
using Game.Core.Predicates;
using Game.ScriptableObjects.Abilities;
using Game.Singletones;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Game.Core.Ablilities
{
    public class ChangeColorAbility : IAbility
    {
        [Serializable]
        public class AbilityMetadata
        {
            public Color targetColor;
        }

        private AbilityMetadata _metadata;
        private AbilityData _abilityData;

        private IPredicate _predicate;
        private float _lastExecutedDistance;

        public AbilityData AbilityData => _abilityData;
        public Color TargetColor => _metadata.targetColor;

        public void Init(AbilityData data)
        {
            _abilityData = data;

            if (!string.IsNullOrEmpty(_abilityData.additionalParamJson))
            {
                _metadata = JsonUtility.FromJson<AbilityMetadata>(_abilityData.additionalParamJson);
            }

            switch (data.requiredValueType)
            {
                case ReqiredValue.More:
                    _predicate = new PredicateMore();
                    break;
                case ReqiredValue.MoreOrEqual:
                    _predicate = new PredicateMoreOrEqual();
                    break;
                case ReqiredValue.Equal:
                    _predicate = new PredicateEqual();
                    break;
                case ReqiredValue.LessOrEqual:
                    _predicate = new PredicateLessOrEqual();
                    break;
                case ReqiredValue.Less:
                    _predicate = new PredicateLess();
                    break;
                case ReqiredValue.Random:
                default:
                    _predicate = new PredicateRandom();
                    break;
            }
        }

        public void Execute(PlayerController player, float param, bool isTwin)
        {
            if (string.IsNullOrEmpty(_abilityData.additionalParamJson))
            {
                _metadata = new AbilityMetadata()
                {
                    targetColor = Random.ColorHSV()
                };
            }

            switch (_abilityData.triggerAbility)
            {
                case TriggerAbility.DistanceCovered:
                    CheckAndExecuteDistance(player, param, isTwin);
                    break;
                case TriggerAbility.Speed:
                    CheckAndExecuteSpeed(player, param, isTwin);
                    break;
                default:
                    Debug.LogError("Incorrect trigger of execute!");
                    return;
            }
        }

        public void Respawn()
        {
            _lastExecutedDistance = 0;
        }

        private void CheckAndExecuteDistance(PlayerController player, float param, bool isTwin)
        {
            if (isTwin ||_predicate.ConditionCorrect
                (param - _lastExecutedDistance, _abilityData.requiredValue))
            {
                _lastExecutedDistance = param;
                player.SetColor(_metadata.targetColor);
                PushTwinData();
            }
        }

        private void CheckAndExecuteSpeed(PlayerController player, float param, bool isTwin)
        {
            if (isTwin || _predicate.ConditionCorrect(param, _abilityData.requiredValue))
            {
                player.SetColor(_metadata.targetColor);
                PushTwinData();
            }
        }

        private void PushTwinData()
        {
            var twinData = new AbilityTwinData()
            {
                releaseTime = GameManager.GameManagerInstance.AbilitiesController.TimeDelta,
                data = _abilityData
            };

            GameManager.GameManagerInstance.AbilitiesController.currentTwinData.Enqueue(twinData);
        }
    }
}