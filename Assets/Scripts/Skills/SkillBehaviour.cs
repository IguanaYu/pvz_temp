using UnityEngine;
using UnityEngine.Events;

namespace PVZ
{
    public abstract class SkillBehaviour : MonoBehaviour
    {
        [SerializeField] private SkillConfigBase config;

        private float _cooldownRemaining;
        private bool _isEnabled;

        protected UnitEntity Owner { get; private set; }
        protected SkillScheduler Scheduler { get; private set; }

        public UnityEvent OnExecuted { get; } = new();

        public SkillConfigBase Config => config;

        public virtual void Initialize(UnitEntity owner, SkillScheduler scheduler)
        {
            Owner = owner;
            Scheduler = scheduler;
            _isEnabled = config == null || config.EnabledByDefault;
            _cooldownRemaining = 0f;
        }

        public void HandleTick(float deltaTime)
        {
            if (!_isEnabled || config == null || !isActiveAndEnabled || Owner == null || !Owner.IsAlive)
            {
                return;
            }

            _cooldownRemaining -= deltaTime;
            if (_cooldownRemaining > 0f)
            {
                return;
            }

            if (Perform(deltaTime))
            {
                OnExecuted.Invoke();
                _cooldownRemaining = Mathf.Max(config.Cooldown, Mathf.Epsilon);
            }
        }

        public void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }

        protected void ResetCooldown()
        {
            _cooldownRemaining = Mathf.Max(config.Cooldown, Mathf.Epsilon);
        }

        protected abstract bool Perform(float deltaTime);
    }
}
