using System;
using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        public event Action<Vector3> TakeDamageEvent;
        public event Action DeathEvent;
        
        [SerializeField] private float currentValue = 2f;

        public void TakeDamage(float value, Vector3 hitDirection)
        {
            TakeDamageEvent?.Invoke(hitDirection);
            currentValue = Mathf.Max(currentValue - value, 0f);

            if (currentValue == 0f)
            {
                DeathEvent?.Invoke();
            }
        }
    }
}
