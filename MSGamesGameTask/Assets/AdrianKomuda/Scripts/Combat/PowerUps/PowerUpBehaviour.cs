using AdrianKomuda.Scripts.StateMachine.Player;
using UnityEngine;

namespace AdrianKomuda.Scripts.Combat.PowerUps
{
    public abstract class PowerUpBehaviour : MonoBehaviour
    {
        public abstract void Perform(PlayerStateMachine playerStateMachine);
    }
}
