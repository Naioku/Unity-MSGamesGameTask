using System.Collections;
using UnityEngine;

namespace Core
{
    public class PowerUpPickup : MonoBehaviour
    {
        [SerializeField] private float respawnTime = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (!LayerMask.NameToLayer("Player").Equals(other.gameObject.layer)) return;
            Pickup();
        }
        
        private void Pickup()
        {
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
