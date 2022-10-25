using UnityEngine;
using UnityEngine.Pool;

namespace Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnedEnemyController enemyPrefab;

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

        private static Vector3 GetRandomPosition()
        {
            return Vector3.zero;
        }
    }
}
