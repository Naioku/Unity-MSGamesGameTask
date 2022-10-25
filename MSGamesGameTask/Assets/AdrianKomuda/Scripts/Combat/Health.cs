using System;
using UnityEngine;

namespace AdrianKomuda.Scripts.Combat
{
    public class Health : MonoBehaviour
    {
        public event Action<Vector3> TakeDamageEvent;
        public event Action DeathEvent;
        
        [SerializeField] private float defaultValue = 2f;
        private float _currentValue;

        private void Start()
        {
            Restore();
        }

        public void TakeDamage(float value, Vector3 hitDirection)
        {
            TakeDamageEvent?.Invoke(hitDirection);
            _currentValue = Mathf.Max(_currentValue - value, 0f);

            if (_currentValue == 0f)
            {
                DeathEvent?.Invoke();
            }
        }

        public void Restore()
        {
            _currentValue = defaultValue;
        }
    }
}
