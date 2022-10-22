using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride;
        
        [Tooltip("Order has to be the same as in Fighter's weapon slots.")]
        [SerializeField] private WeaponEquippedController[] equippedPrefabs;

        public void Equip(Transform[] weaponSlots, Animator animator)
        {
            SpawnPrefabs(weaponSlots);
            ReloadAnimations(animator);
        }

        private void SpawnPrefabs(Transform[] weaponSlots)
        {
            int shortestLenght = Mathf.Min(weaponSlots.Length, equippedPrefabs.Length);
            
            for (int i = 0; i < shortestLenght; i++)
            {
                Instantiate(equippedPrefabs[i], weaponSlots[i]);
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
}
