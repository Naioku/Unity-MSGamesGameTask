using StateMachine.Player;
using UnityEngine;

namespace Combat.PowerUps
{
    public class IncreaseDamagePowerUp : PowerUpBehaviour
    {
        public override void Perform(PlayerStateMachine playerStateMachine)
        {
            Debug.Log("Performing " + name);
        }
    }
}
