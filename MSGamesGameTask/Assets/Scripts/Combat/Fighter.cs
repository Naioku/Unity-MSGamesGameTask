using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Fighter: MonoBehaviour
    {
        [Tooltip("Order has to be the same as in Weapon's equipped prefabs.")]
        [SerializeField] private Transform[] weaponSlots;
        
        [SerializeField] private Weapon defaultWeapon;
        [SerializeField] private Attack[] attacks = new Attack[2];

        private Dictionary<AttackType, Attack> _attacksLookup;
        private Weapon _currentWeapon;

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        public Attack GetAttack(AttackType attackType)
        {
            BuildAttacksLookup();
            return _attacksLookup[attackType];
        }
        
        // Called by animation event
        public void Shoot(int weaponIndex)
        {
            print(weaponIndex);
        }

        private void BuildAttacksLookup()
        {
            if (_attacksLookup != null) return;

            _attacksLookup = new Dictionary<AttackType, Attack>();
            foreach (Attack attack in attacks)
            {
                _attacksLookup.Add(attack.AttackType, attack);
            }
        }

        private void EquipWeapon(Weapon weapon)
        {
            weapon.Equip(weaponSlots, GetComponent<Animator>());
            _currentWeapon = weapon;
        }
    }

    public enum AttackType
    {
        LeftHanded,
        RightHanded
    }
}
