using System.Collections;
using StateMachine.Player;
using UnityEngine;

namespace Combat.PowerUps
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

        private IEnumerator HideForSeconds(float seconds)
        {
            SetPickupVisibility(false);
            yield return new WaitForSecondsRealtime(seconds);
            SetPickupVisibility(true);
        }

        private void SetPickupVisibility(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}
