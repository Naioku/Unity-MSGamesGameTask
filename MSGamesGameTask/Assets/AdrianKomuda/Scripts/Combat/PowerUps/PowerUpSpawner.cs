using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdrianKomuda.Scripts.Combat.PowerUps
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField] private float respawnTime = 5f;
        [SerializeField] private PowerUpPickup[] pickups;

        private void Start()
        {
            foreach (PowerUpPickup pickup in pickups)
            {
                PowerUpPickup instantiate = Instantiate(pickup, transform);
                instantiate.PowerUpSpawner = this;
            }
            SetPickupVisibility(GetRandomInstantiatedPickup(), true);
        }

        public void HidePickup(PowerUpPickup pickup)
        {
            StartCoroutine(HideForSeconds(pickup, respawnTime));
        }

        private IEnumerator HideForSeconds(PowerUpPickup pickup, float seconds)
        {
            SetPickupVisibility(pickup, false);
            yield return new WaitForSecondsRealtime(seconds);
            SetPickupVisibility(GetRandomInstantiatedPickup(), true);
        }

        private void SetPickupVisibility(PowerUpPickup pickup, bool shouldShow)
        {
            pickup.GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in pickup.transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        private PowerUpPickup GetRandomInstantiatedPickup()
        {
            return transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<PowerUpPickup>();
        }
    }
}
