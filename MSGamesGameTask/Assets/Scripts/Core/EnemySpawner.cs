using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Core
{
    public class EnemySpawner : MonoBehaviour
    {
        public readonly Dictionary<GameObject, Coroutine> Timers = new();

        [SerializeField] private SpawnedEnemyController enemyPrefab;
        [SerializeField] private float spawnAfterDeathDelay = 4f;

        private IObjectPool<SpawnedEnemyController> _enemyPool;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

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
                GetRandomPositionOnNavmesh(),
                Quaternion.identity,
                transform);
            
            enemyInstance.EnemyPool = _enemyPool;
            return enemyInstance;
        }

        private void OnGet(SpawnedEnemyController obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = GetRandomPositionOnNavmesh();

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

        private Vector3 GetRandomPositionOnNavmesh()
        {
            Bounds colliderBounds = _collider.bounds;
            Vector3 minBound = colliderBounds.min;
            Vector3 maxBound = colliderBounds.max;

            Vector3 randomPosition = new Vector3(
                Random.Range(minBound.x, maxBound.x),
                Random.Range(minBound.y, maxBound.y));
            
            if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            
            return Vector3.zero;
        }
    }
}
