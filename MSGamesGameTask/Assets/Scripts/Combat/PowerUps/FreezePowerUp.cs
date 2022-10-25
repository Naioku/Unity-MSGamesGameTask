using StateMachine.Player;
using UnityEngine;

namespace Combat.PowerUps
{
    public class FreezePowerUp : PowerUpBehaviour
    {
        public override void Perform(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Performing " + name);
        }
    }
}
