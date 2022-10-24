using System;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class Attack
    {
        [field: SerializeField] public string AnimationName { get; private set; }
        [field: SerializeField] public AttackSlotType AttackSlotType { get; private set; }
        [field: SerializeField] public Transform WeaponSlot { get; private set; }
        [field: SerializeField] public float TransitionDuration { get; private set; } = 0.1f;
        [field: SerializeField] public float NextComboAttackNormalizedTime { get; private set; }
        [field: SerializeField] public float AttackerDisplacement { get; private set; } = 0f;
        [field: SerializeField] public float AttackerDisplacementSmoothingTime { get; private set; } = 0.1f;
        [field: Range(0f, 1f)]
        [field: SerializeField] public float DisplacementApplicationNormalizedTime { get; private set; } = 0.1f;
    }
}
