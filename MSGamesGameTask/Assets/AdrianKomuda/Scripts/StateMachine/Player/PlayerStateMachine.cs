using AdrianKomuda.Scripts.Combat;
using AdrianKomuda.Scripts.Core;
using AdrianKomuda.Scripts.Locomotion;
using AdrianKomuda.Scripts.Locomotion.Player;
using UnityEngine;

namespace AdrianKomuda.Scripts.StateMachine.Player
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public float AnimationCrossFadeDuration { get; private set; } = 0.1f;
        [field: SerializeField] public float AnimatorDampTime { get; private set; } = 0.05f;
        
        public InputReader InputReader { get; private set; }
        public PlayerMover PlayerMover { get; private set; }
        public ForceReceiver ForceReceiver { get; private set; }
        public Animator Animator { get; private set; }
        public Fighter Fighter { get; private set; }

        private Health _health;

        private void Awake()
        {
            InputReader = GetComponent<InputReader>();
            PlayerMover = GetComponent<PlayerMover>();
            ForceReceiver = GetComponent<ForceReceiver>();
            Animator = GetComponent<Animator>();
            Fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            SwitchState(new PlayerLocomotionState(this));
        }
        
        private void OnEnable()
        {
            _health.TakeDamageEvent += HandleTakeDamage;
            _health.DeathEvent += HandleDeath;
        }

        private void OnDisable()
        {
            _health.TakeDamageEvent -= HandleTakeDamage;
            _health.DeathEvent -= HandleDeath;
        }

        private void HandleTakeDamage(Vector3 hitDirection)
        {
            SwitchState(new PlayerImpactState(this));
        }

        private void HandleDeath()
        {
            SwitchState(new PlayerDeathState(this));
        }
    }
}
