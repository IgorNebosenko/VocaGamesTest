using Game.Controllers;
using Game.ScriptableObjects.Abilities;

namespace Game.Core.Interfaces
{
    public interface IAbility
    {
        AbilityData AbilityData { get; }
        void Init(AbilityData data);
        void Execute(PlayerController player, float param, bool isTwin);
        void Respawn();
    }
}