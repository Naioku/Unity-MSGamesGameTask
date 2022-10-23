using UnityEngine;

namespace Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifeTime = 2f;

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void Prepare(Vector3 direction)
        {
            SetDirection(direction);
            Destroy(gameObject, lifeTime);
        }

        private void SetDirection(Vector3 direction)
        {
            transform.forward = direction;
        }
    }
}
