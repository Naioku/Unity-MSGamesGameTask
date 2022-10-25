using UnityEngine;
using UnityEngine.Pool;

namespace Core
{
    public class SpawnedEnemyController : MonoBehaviour
    {
        public IObjectPool<SpawnedEnemyController> EnemyPool { private get; set; }
        public EnemySpawner EnemySpawner { private get; set; }
        
        public void Release()
        {
            EnemyPool.Release(this);
            EnemySpawner.Release(gameObject);
        }
    }
}
