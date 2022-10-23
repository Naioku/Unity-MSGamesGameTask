using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class WeaponSet
    {
        private readonly Dictionary<AttackSlotType, WeaponController> _currentlyEquippedWeaponsLookup;
        private readonly Projectile _projectile;

        public WeaponSet(Dictionary<AttackSlotType, WeaponController> currentlyEquippedWeaponsLookup, Projectile projectile)
        {
            _currentlyEquippedWeaponsLookup = currentlyEquippedWeaponsLookup;
            _projectile = projectile;
        }
        
        public void LunchProjectile(AttackSlotType attackSlotType, Transform weaponSlot, Vector3 attackDirection)
        {
            Projectile projectileInstance = Object.Instantiate(
                _projectile,
                GetProjectileSpawnPoint(attackSlotType, weaponSlot).position,
                Quaternion.identity);
            
            projectileInstance.Prepare(attackDirection);
        }
        
        private Transform GetProjectileSpawnPoint(AttackSlotType attackSlotType, Transform weaponSlot)
        {
            WeaponController weapon = _currentlyEquippedWeaponsLookup[attackSlotType];
            Transform projectileSpawnPoint = weapon.ProjectileSpawnPoint;
            
            return projectileSpawnPoint != null ? projectileSpawnPoint : weaponSlot;
        }
    }
}
