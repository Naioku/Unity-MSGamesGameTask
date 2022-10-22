﻿using UnityEngine;

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

        private void EquipWeapon(Weapon weapon)
        {
            weapon.Equip(weaponSlots, GetComponent<Animator>());
            _currentWeapon = weapon;
        }

        public void Shoot(int weaponIndex)
        {
            print(weaponIndex);
        }
    }
}
