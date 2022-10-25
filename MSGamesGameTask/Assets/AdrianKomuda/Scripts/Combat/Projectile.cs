using UnityEngine;

namespace AdrianKomuda.Scripts.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private GameObject[] destroyOnHit;
        [SerializeField] private float lifeAfterImpact = 2f;

        private float _damage;
        private Fighter _caster;

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _caster.gameObject.layer) return;
            if (LayerMask.NameToLayer("Pickup").Equals(other.gameObject.layer)) return;
            if (LayerMask.NameToLayer("Projectile").Equals(other.gameObject.layer)) return;
            
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

        public void Prepare(Vector3 direction, float damage, Fighter caster)
        {
            SetDirection(direction);
            SetDamage(damage);
            SetCaster(caster);
            Destroy(gameObject, lifeTime);
        }

        private void SetDirection(Vector3 direction) => transform.forward = direction;
        private void SetDamage(float damage) => _damage = damage;
        private void SetCaster(Fighter caster) => _caster = caster;
    }
}
