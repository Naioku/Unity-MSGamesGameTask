using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Combat
{
    public class Fighter: MonoBehaviour
    {
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
        public void Shoot(AttackType attackType)
        {
            Vector3 mouseWorldPosition = GetComponent<InputReader>().MouseWorldPosition;
            _currentWeapon.LunchProjectile(attackType, _attacksLookup[attackType].WeaponSlot, transform.forward);
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
            BuildAttacksLookup();
            
            weapon.Equip(_attacksLookup, GetComponent<Animator>());
            _currentWeapon = weapon;
        }
    }

    public enum AttackType
    {
        LeftHanded,
        RightHanded
    }
}
