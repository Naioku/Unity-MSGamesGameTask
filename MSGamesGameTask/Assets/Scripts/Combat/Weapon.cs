using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private EquippedWeapon[] equippedWeapons;
        [SerializeField] private float damage = 1f;
        [SerializeField] private Projectile projectile;

        private Dictionary<AttackType, WeaponEquippedController> _equippedWeaponsLookup;

        public void Equip(Dictionary<AttackType, Attack> attacksLookup, Animator animator)
        {
            SpawnPrefabs(attacksLookup);
            ReloadAnimations(animator);
        }

        private void SpawnPrefabs(Dictionary<AttackType, Attack> attacksLookup)
        {
            BuildLookup();
            
            foreach (var equippedWeapon in _equippedWeaponsLookup)
            {
                Instantiate(equippedWeapon.Value, attacksLookup[equippedWeapon.Key].WeaponSlot);
            }
        }

        private void BuildLookup()
        {
            if (_equippedWeaponsLookup != null) return;

            _equippedWeaponsLookup = new Dictionary<AttackType, WeaponEquippedController>();
            foreach (EquippedWeapon weapon in equippedWeapons)
            {
                _equippedWeaponsLookup.Add(weapon.attackType, weapon.equippedPrefab);
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
  
            // projectileInstance.SetTarget(instigator, target, damage);
        }

        private Transform GetProjectileSpawnPoint(AttackType attackType, Transform weaponSlot)
        {
            BuildLookup();
            
            WeaponEquippedController weaponEquippedController = _equippedWeaponsLookup[attackType];
            Transform projectileSpawnPoint = weaponEquippedController.ProjectileSpawnPoint;
           
            return projectileSpawnPoint != null ? projectileSpawnPoint : weaponSlot;
        }
    }

    [Serializable]
    internal class EquippedWeapon
    {
        public AttackType attackType;
        public WeaponEquippedController equippedPrefab;
    }
}
