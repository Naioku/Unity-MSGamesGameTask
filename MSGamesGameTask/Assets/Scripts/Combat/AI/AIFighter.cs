using UnityEngine;

namespace Combat.AI
{
    public class AIFighter : Fighter
    {
        [SerializeField] private float attackRange = 10f;
        
        public bool IsInAttackRange(Vector3 targetPosition)
        {
            return (targetPosition - transform.position).sqrMagnitude <= Mathf.Pow(attackRange, 2);
        }
    }
}
