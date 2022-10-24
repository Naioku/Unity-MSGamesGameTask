using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float currentValue = 2f;

        public void TakeDamage(float value)
        {
            currentValue = Mathf.Max(currentValue - value, 0f);
        }
    }
}
