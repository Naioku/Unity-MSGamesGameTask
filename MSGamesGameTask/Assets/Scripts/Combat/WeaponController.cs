using UnityEngine;

namespace Combat
{
    public class WeaponController : MonoBehaviour
    {
        [field: SerializeField] public Transform ProjectileSpawnPoint { get; private set; }
    }
}
