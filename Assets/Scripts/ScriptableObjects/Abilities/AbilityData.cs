using System;

namespace Game.ScriptableObjects.Abilities
{
    [Serializable]
    public class AbilityData
    {
        public TypeAbility typeAbility;
        public string additionalParamJson;

        public TriggerAbility triggerAbility;
        public ReqiredValue requiredValueType;
        public float requiredValue;
    }
}