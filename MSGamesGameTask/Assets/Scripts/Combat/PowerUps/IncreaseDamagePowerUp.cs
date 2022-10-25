using System.Collections;
using StateMachine.Player;
using UnityEngine;

namespace Combat.PowerUps
{
    public class IncreaseDamagePowerUp : PowerUpBehaviour
    {
        [SerializeField] private float additionalDamage = 1f;
        [SerializeField] private float effectDuration = 10f;

        private Coroutine _timer;

        public override void Perform(PlayerStateMachine playerStateMachine)
        {
            playerStateMachine.Fighter.AdditionalDamage = additionalDamage;

            if (_timer != null)
            {
                StopCoroutine(_timer);
            }
            StartCoroutine(DisableEffect(playerStateMachine));
        }

        private IEnumerator DisableEffect(PlayerStateMachine playerStateMachine)
        {
            yield return new WaitForSecondsRealtime(effectDuration);
            playerStateMachine.Fighter.AdditionalDamage = 0f;
        }
    }
}
