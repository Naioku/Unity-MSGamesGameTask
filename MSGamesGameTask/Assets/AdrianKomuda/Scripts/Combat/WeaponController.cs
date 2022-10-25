using UnityEngine;

namespace AdrianKomuda.Scripts.Combat
{
    public class WeaponController : MonoBehaviour
    {
        [field: SerializeField] public Transform ProjectileSpawnPoint { get; private set; }
    }
}
