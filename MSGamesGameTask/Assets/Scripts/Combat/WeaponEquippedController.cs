using UnityEngine;

namespace Combat
{
    public class WeaponEquippedController : MonoBehaviour
    {
        [field: SerializeField] public Transform ProjectileSpawnPoint { get; private set; }
    }
}
