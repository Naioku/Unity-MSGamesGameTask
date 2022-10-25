using UnityEngine;
using UnityEngine.Pool;

namespace Core
{
    public class SpawnedEnemyController : MonoBehaviour
    {
        public IObjectPool<SpawnedEnemyController> EnemyPool { get; set; }
        public EnemySpawner EnemySpawner { get; set; }
        
        public void Release()
        {
            EnemyPool.Release(this);
            EnemySpawner.Release(gameObject);
        }
    }
}
