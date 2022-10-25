using StateMachine.Player;
using UnityEngine;

namespace Combat.PowerUps
{
    public abstract class PowerUpBehaviour : MonoBehaviour
    {
        public abstract void Perform(PlayerStateMachine playerStateMachine);
    }
}
