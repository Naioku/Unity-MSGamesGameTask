using System.Collections.Generic;
using UnityEngine;

namespace AdrianKomuda.Scripts.Combat
{
    public class WeaponSet
    {
        private readonly Dictionary<AttackSlotType, WeaponController> _currentlyEquippedWeaponsLookup;
        private readonly Projectile _projectile;
        private readonly float _damage;

        public WeaponSet(
            Dictionary<AttackSlotType,
            WeaponController> currentlyEquippedWeaponsLookup,
            Projectile projectile,
            float damage)
        {
            _currentlyEquippedWeaponsLookup = currentlyEquippedWeaponsLookup;
            _projectile = projectile;
            _damage = damage;
        }
        
        public void LunchProjectile
        (
            AttackSlotType attackSlotType,
            Transform weaponSlot,
            Vector3 attackDirection,
            Fighter caster,
            float additionalDamage
        )
        {
            Projectile projectileInstance = Object.Instantiate(
                _projectile,
                GetProjectileSpawnPoint(attackSlotType, weaponSlot).position,
                Quaternion.identity);
            
            projectileInstance.Prepare(attackDirection, _damage + additionalDamage, caster);
        }
        
        private Transform GetProjectileSpawnPoint(AttackSlotType attackSlotType, Transform weaponSlot)
        {
            WeaponController weapon = _currentlyEquippedWeaponsLookup[attackSlotType];
            Transform projectileSpawnPoint = weapon.ProjectileSpawnPoint;
            
            return projectileSpawnPoint != null ? projectileSpawnPoint : weaponSlot;
        }
    }
}
