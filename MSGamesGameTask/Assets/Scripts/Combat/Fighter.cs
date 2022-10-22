using UnityEngine;

namespace Combat
{
    public class Fighter: MonoBehaviour
    {
        [Tooltip("Order has to be the same as in Weapon's equipped prefabs.")]
        [SerializeField] private Transform[] weaponSlots;
        
        [SerializeField] private Weapon defaultWeapon;
        [SerializeField] private Attack[] attacks = new Attack[2];

        private Weapon _currentWeapon;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        public Attack GetAttack()
        {
            int randomIndex = Random.Range(0, attacks.Length);
            return attacks[randomIndex];
        }
        
        // Called by animation event
        public void Shoot(int weaponIndex)
        {
            print(weaponIndex);
        }

        private void EquipWeapon(Weapon weapon)
        {
            weapon.Equip(weaponSlots, GetComponent<Animator>());
            _currentWeapon = weapon;
        }
    }
}
