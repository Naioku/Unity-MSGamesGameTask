using UnityEngine;

namespace Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private void Update()
        {
            // transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
