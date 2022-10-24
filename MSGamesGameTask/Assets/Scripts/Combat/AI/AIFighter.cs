using System.Collections;
using UnityEngine;

namespace Combat.AI
{
    public class AIFighter : Fighter
    {
        [SerializeField] public float delayBetweenAttacks = 1f;
        
        [SerializeField] private float attackRange = 10f;

        private float _timeToNextAttack;
        private Coroutine _timerCoroutine;

        public bool IsInAttackRange(Vector3 targetPosition)
        {
            return (targetPosition - transform.position).sqrMagnitude <= Mathf.Pow(attackRange, 2);
        }

        public Attack GetRandomAttack()
        {
            return attacks[Random.Range(0, attacks.Length)];
        }
        
        public bool ReadyForNextAttack()
        {
            return _timeToNextAttack <= 0f;
        }
        
        public void StartTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
            _timeToNextAttack = delayBetweenAttacks;
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            while (_timeToNextAttack > 0f)
            {
                _timeToNextAttack -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
