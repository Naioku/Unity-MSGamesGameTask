﻿using System;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public class Attack
    {
        [field: SerializeField] public string AnimationName { get; private set; }
        [field: SerializeField] public AttackSlotType AttackSlotType { get; private set; }
        [field: SerializeField] public Transform WeaponSlot { get; private set; }
        [field: SerializeField] public float TransitionDuration { get; private set; }
        [field: SerializeField] public float NextComboAttackNormalizedTime { get; private set; }
    }
}
