using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdrianKomuda.Scripts.Combat
{
    public class Fighter: MonoBehaviour
    {
        public float AdditionalDamage { private get; set; }
        public bool ReadyForNextAttack { get; private set; } = true;

        [SerializeField] private WeaponSO defaultWeaponSo;
        [SerializeField] protected Attack[] attacks = new Attack[2];
        [SerializeField] public float delayBetweenAttacks = 1f;

        private Dictionary<AttackSlotType, Attack> _attacksLookup;
        private WeaponSet _currentWeapon;
        private Coroutine _timerCoroutine;

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
            _currentWeapon.LunchProjectile(
                attackSlotType,
                _attacksLookup[attackSlotType].WeaponSlot,
                transform.forward,
                this,
                AdditionalDamage);
        }

        public void StartTimer()
        {
            ReadyForNextAttack = false;
            
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            yield return new WaitForSecondsRealtime(delayBetweenAttacks);
            ReadyForNextAttack = true;
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
