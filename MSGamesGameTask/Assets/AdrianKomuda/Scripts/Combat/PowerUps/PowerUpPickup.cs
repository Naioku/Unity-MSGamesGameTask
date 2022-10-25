using AdrianKomuda.Scripts.StateMachine.Player;
using UnityEngine;

namespace AdrianKomuda.Scripts.Combat.PowerUps
{
    public class PowerUpPickup : MonoBehaviour
    {
        public PowerUpSpawner PowerUpSpawner { private get; set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerStateMachine playerStateMachine)) return;
            Pickup(playerStateMachine);
        }
        
        private void Pickup(PlayerStateMachine playerStateMachine)
        {
            GetComponent<PowerUpBehaviour>().Perform(playerStateMachine);
            PowerUpSpawner.HidePickup(this);
        }
    }
}
