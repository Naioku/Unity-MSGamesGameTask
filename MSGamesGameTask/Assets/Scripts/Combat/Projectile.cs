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

        private float _damage;
        private int _casterLayer;

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void Prepare(Vector3 direction, float damage, int casterLayer)
        {
            SetDirection(direction);
            SetDamage(damage);
            SetCasterLayer(casterLayer);
            Destroy(gameObject, lifeTime);
        }

        private void SetDirection(Vector3 direction) => transform.forward = direction;
        private void SetDamage(float damage) => _damage = damage;
        private void SetCasterLayer(LayerMask casterLayer) => _casterLayer = casterLayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _casterLayer) return;
            
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

            if (!other.TryGetComponent(out Health health)) return;
            Vector3 hitDirection = other.transform.position - transform.position;
            health.TakeDamage(_damage, hitDirection);
        }
    }
}
