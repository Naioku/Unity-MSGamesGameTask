using UnityEngine;

namespace Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;

        public void SetDirection(Vector3 direction)
        {
            transform.forward = direction;
        }
        
        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
