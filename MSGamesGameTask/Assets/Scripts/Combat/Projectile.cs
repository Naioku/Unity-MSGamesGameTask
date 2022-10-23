using UnityEngine;

namespace Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private GameObject[] destroyOnHit;
        [SerializeField] private float lifeAfterImpact = 2f;


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
        
        private void OnTriggerEnter(Collider other)
        {
            speed = 0f;
            
            if (hitEffect != null)
            {
                Instantiate(hitEffect, other.ClosestPoint(transform.position), Quaternion.identity);
            }

            foreach (var obj in destroyOnHit)
            {
                Destroy(obj);
            }
            
            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
