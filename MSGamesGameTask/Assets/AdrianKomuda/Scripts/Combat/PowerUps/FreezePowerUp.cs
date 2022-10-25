using System.Collections;
using AdrianKomuda.Scripts.Core;
using AdrianKomuda.Scripts.StateMachine.AI;
using AdrianKomuda.Scripts.StateMachine.Player;
using UnityEngine;

namespace AdrianKomuda.Scripts.Combat.PowerUps
{
    public class FreezePowerUp : PowerUpBehaviour
    {
        [SerializeField] private float effectDuration = 10f;
        
        private EnemySpawner[] _enemySpawners;

        private void Start()
        {
            _enemySpawners = FindObjectsOfType<EnemySpawner>();
        }

        public override void Perform(PlayerStateMachine playerStateMachine)
        {
            foreach (EnemySpawner enemySpawner in _enemySpawners)
            {
                foreach (Transform enemy in enemySpawner.transform)
                {
                    enemy.GetComponent<AIStateMachine>().SwitchToFrozenState();
                }
            }
            
            StartCoroutine(DisableEffect());
        }
        
        private IEnumerator DisableEffect()
        {
            yield return new WaitForSecondsRealtime(effectDuration);
            foreach (EnemySpawner enemySpawner in _enemySpawners)
            {
                foreach (Transform enemy in enemySpawner.transform)
                {
                    enemy.GetComponent<AIStateMachine>().SwitchToDefaultState();
                }
            }
        }
    }
}
