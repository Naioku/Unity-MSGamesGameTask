using UnityEngine;
using UnityEngine.Pool;

namespace Core
{
    public class SpawnedEnemyController : MonoBehaviour
    {
        public IObjectPool<SpawnedEnemyController> EnemyPool { get; set; }

        public void Release()
        {
            EnemyPool.Release(this);
        }
    }
}
