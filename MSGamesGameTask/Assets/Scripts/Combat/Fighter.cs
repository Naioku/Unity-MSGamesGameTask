using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Fighter: MonoBehaviour
    {
        [SerializeField] private WeaponSO defaultWeaponSo;
        [SerializeField] protected Attack[] attacks = new Attack[2];

        private Dictionary<AttackSlotType, Attack> _attacksLookup;
        private WeaponSet _currentWeapon;

        private void Start()
        {
            EquipWeapon(defaultWeaponSo);
        }

        public Attack GetAttack(AttackSlotType attackSlotType)
        {
            BuildAttacksLookup();
            return _attacksLookup[attackSlotType];
        }
        
        // Called by animation event
        public void Shoot(AttackSlotType attackSlotType)
        {
            _currentWeapon.LunchProjectile(attackSlotType, _attacksLookup[attackSlotType].WeaponSlot, transform.forward, gameObject.layer);
        }

        private void BuildAttacksLookup()
        {
            if (_attacksLookup != null) return;

            _attacksLookup = new Dictionary<AttackSlotType, Attack>();
            foreach (Attack attack in attacks)
            {
                _attacksLookup.Add(attack.AttackSlotType, attack);
            }
        }

        private void EquipWeapon(WeaponSO weaponSo)
        {
            BuildAttacksLookup();
            
            _currentWeapon = weaponSo.Equip(_attacksLookup, GetComponent<Animator>());
        }
    }

    public enum AttackSlotType
    {
        LeftHand,
        RightHand,
        Mouth
    }
}
