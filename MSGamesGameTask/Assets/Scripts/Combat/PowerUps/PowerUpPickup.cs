using System.Collections;
using StateMachine.Player;
using UnityEngine;

namespace Combat.PowerUps
{
    public class PowerUpPickup : MonoBehaviour
    {
        [SerializeField] private float respawnTime = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerStateMachine playerStateMachine)) return;
            Pickup(playerStateMachine);
        }
        
        private void Pickup(PlayerStateMachine playerStateMachine)
        {
            GetComponent<PowerUpBehaviour>().Perform(playerStateMachine);
            StartCoroutine(HideForSeconds(respawnTime));
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
