using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdrianKomuda.Scripts.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private WeaponToEquip[] weaponsToEquip;
        [SerializeField] private float damage = 1f;
        [SerializeField] private Projectile projectile;

        private Dictionary<AttackSlotType, WeaponController> _weaponsToEquipLookup;

        public WeaponSet Equip(Dictionary<AttackSlotType, Attack> attacksLookup, Animator animator)
        {
            ReloadAnimations(animator);
            return SpawnPrefabs(attacksLookup);
        }

        private WeaponSet SpawnPrefabs(Dictionary<AttackSlotType, Attack> attacksLookup)
        {
            BuildLookup();
            
            Dictionary<AttackSlotType, WeaponController> weaponsInstancesLookup = new();
            foreach (var equippedWeapon in _weaponsToEquipLookup)
            {
                WeaponController instance = Instantiate(equippedWeapon.Value, attacksLookup[equippedWeapon.Key].WeaponSlot);
                weaponsInstancesLookup.Add(equippedWeapon.Key, instance);
            }

            return new WeaponSet(weaponsInstancesLookup, projectile, damage);
        }

        private void BuildLookup()
        {
            if (_weaponsToEquipLookup != null) return;

            _weaponsToEquipLookup = new Dictionary<AttackSlotType, WeaponController>();
            foreach (WeaponToEquip weapon in weaponsToEquip)
            {
                _weaponsToEquipLookup.Add(weapon.attackSlotType, weapon.prefab);
            }
        }

        private void ReloadAnimations(Animator animator)
        {
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }
    }

    [Serializable]
    internal class WeaponToEquip
    {
        public AttackSlotType attackSlotType;
        public WeaponController prefab;
    }
}
