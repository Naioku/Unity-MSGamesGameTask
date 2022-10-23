using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private WeaponToEquip[] weaponsToEquip;
        [SerializeField] private float damage = 1f;
        [SerializeField] private Projectile projectile;

        private Dictionary<AttackType, WeaponController> _weaponsToEquipLookup;
        private readonly Dictionary<AttackType, WeaponController> _currentlyEquippedWeaponsLookup = new();

        public void Equip(Dictionary<AttackType, Attack> attacksLookup, Animator animator)
        {
            SpawnPrefabs(attacksLookup);
            ReloadAnimations(animator);
        }

        private void SpawnPrefabs(Dictionary<AttackType, Attack> attacksLookup)
        {
            BuildLookup();
            _currentlyEquippedWeaponsLookup.Clear();
            
            foreach (var equippedWeapon in _weaponsToEquipLookup)
            {
                WeaponController instance = Instantiate(equippedWeapon.Value, attacksLookup[equippedWeapon.Key].WeaponSlot);
                _currentlyEquippedWeaponsLookup.Add(equippedWeapon.Key, instance);
            }
        }

        private void BuildLookup()
        {
            if (_weaponsToEquipLookup != null) return;

            _weaponsToEquipLookup = new Dictionary<AttackType, WeaponController>();
            foreach (WeaponToEquip weapon in weaponsToEquip)
            {
                _weaponsToEquipLookup.Add(weapon.attackType, weapon.prefab);
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
        
        public void LunchProjectile(AttackType attackType, Transform weaponSlot)
        {
            Projectile projectileInstance = Instantiate(
                projectile,
                GetProjectileSpawnPoint(attackType, weaponSlot).position,
                Quaternion.identity);
        }

        private Transform GetProjectileSpawnPoint(AttackType attackType, Transform weaponSlot)
        {
            BuildLookup();

            WeaponController weapon = _currentlyEquippedWeaponsLookup[attackType];
            Transform projectileSpawnPoint = weapon.ProjectileSpawnPoint;
            
            return projectileSpawnPoint != null ? projectileSpawnPoint : weaponSlot;
        }
    }

    [Serializable]
    internal class WeaponToEquip
    {
        public AttackType attackType;
        public WeaponController prefab;
    }
}
