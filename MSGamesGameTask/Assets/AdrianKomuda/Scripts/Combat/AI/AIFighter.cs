using UnityEngine;

namespace AdrianKomuda.Scripts.Combat.AI
{
    public class AIFighter : Fighter
    {
        [SerializeField] private float attackRange = 10f;
        
        public bool IsInAttackRange(Vector3 targetPosition)
        {
            return (targetPosition - transform.position).sqrMagnitude <= Mathf.Pow(attackRange, 2);
        }

        public Attack GetRandomAttack()
        {
            return attacks[Random.Range(0, attacks.Length)];
        }
    }
}
