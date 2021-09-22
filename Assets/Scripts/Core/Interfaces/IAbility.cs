using Game.ScriptableObjects.Abilities;

namespace Game.Core.Interfaces
{
    public interface IAbility
    {
        void Init(AbilityData data);
        void Execute();
    }
}