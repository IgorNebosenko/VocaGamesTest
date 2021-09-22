using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "Abilities Config", menuName = "Configs/Abilities")]
    public class AblitiesConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityData> listAbilities;

        public List<AbilityData> ListAbilities => listAbilities;
    }
}