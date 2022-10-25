using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Core
{
    public class EnemySpawner : MonoBehaviour
    {
        public readonly Dictionary<GameObject, Coroutine> Timers = new();

        [SerializeField] private SpawnedEnemyController enemyPrefab;
        [SerializeField] private float spawnAfterDeathDelay = 4f;

        private IObjectPool<SpawnedEnemyController> _enemyPool;

        private void Start()
        {
            _enemyPool = new ObjectPool<SpawnedEnemyController>(
                CreateProjectile,
                OnGet,
                OnRelease,
                OnDestroyObject);

            _enemyPool.Get();
        }

        public void Release(GameObject gameObj)
        {
            StartTimer(gameObj);
        }
        
        private SpawnedEnemyController CreateProjectile()
        {
            SpawnedEnemyController enemyInstance = Instantiate(
                enemyPrefab, 
                GetRandomPosition(),
                Quaternion.identity,
                transform);
            
            enemyInstance.EnemyPool = _enemyPool;
            return enemyInstance;
        }

        private void OnGet(SpawnedEnemyController obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = GetRandomPosition();

            obj.EnemySpawner = this;
        }

        private void OnRelease(SpawnedEnemyController obj)
        {
            if (!obj.enabled) return;
            
            obj.gameObject.SetActive(false);
        }

        private void OnDestroyObject(SpawnedEnemyController obj)
        {
            Destroy(obj.gameObject);
        }
        
        private void StartTimer(GameObject gameObj)
        {
            if (Timers.ContainsKey(gameObj))
            {
                Coroutine timer = Timers[gameObj];
                if (Timers[gameObj] != null)
                {
                    StopCoroutine(timer);
                }
            }
            
            Timers[gameObj] = StartCoroutine(TimerCoroutine());
        }
        
        private IEnumerator TimerCoroutine()
        {
            yield return new WaitForSecondsRealtime(spawnAfterDeathDelay);
            _enemyPool.Get();
        }

        private static Vector3 GetRandomPosition()
        {
            return Vector3.zero;
        }
    }
}
