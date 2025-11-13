using System;
using UnityEngine;
using UnityEngine.Events;

namespace PVZ
{
    public class UnitEntity : MonoBehaviour
    {
        [SerializeField] private UnitDefinition definition;

        private readonly SkillScheduler _scheduler = new();
        private SkillBehaviour[] _skills = Array.Empty<SkillBehaviour>();

        public UnitStats Stats { get; private set; }
        public bool IsAlive => Stats != null && Stats.CurrentHealth > 0;

        public UnityEvent<UnitEntity> Died { get; } = new();
        public UnityEvent<int> HealthChanged { get; } = new();

        protected virtual void Awake()
        {
            InitializeRuntime();
        }

        public void InitializeRuntime()
        {
            if (definition == null)
            {
                Debug.LogError($"{name} is missing a UnitDefinition.");
                enabled = false;
                return;
            }

            Stats = new UnitStats(definition);
            _skills = GetComponents<SkillBehaviour>();
            for (var i = 0; i < _skills.Length; i++)
            {
                var skill = _skills[i];
                if (skill == null)
                {
                    continue;
                }

                skill.Initialize(this, _scheduler);
                _scheduler.Register(skill);
            }

            if (definition.SkillConfigs != null && definition.SkillConfigs.Count > 0)
            {
                var expected = new System.Collections.Generic.HashSet<SkillConfigBase>(definition.SkillConfigs);
                for (var i = 0; i < _skills.Length; i++)
                {
                    var skill = _skills[i];
                    if (skill?.Config != null)
                    {
                        expected.Remove(skill.Config);
                    }
                }

                foreach (var missing in expected)
                {
                    if (missing != null)
                    {
                        Debug.LogWarning($"{name} is missing a SkillBehaviour for config {missing.name}.");
                    }
                }
            }
        }

        private void Update()
        {
            _scheduler.Tick(Time.deltaTime);
        }

        public void InflictDamage(int amount)
        {
            if (!IsAlive)
            {
                return;
            }

            Stats.CurrentHealth = Mathf.Max(Stats.CurrentHealth - Mathf.Max(amount, 0), 0);
            HealthChanged.Invoke(Stats.CurrentHealth);
            if (Stats.CurrentHealth <= 0)
            {
                Died.Invoke(this);
            }
        }

        public void Heal(int amount)
        {
            if (!IsAlive)
            {
                return;
            }

            Stats.CurrentHealth = Mathf.Min(Stats.CurrentHealth + Mathf.Max(amount, 0), Stats.MaxHealth);
            HealthChanged.Invoke(Stats.CurrentHealth);
        }

        public UnitDefinition Definition => definition;
        public SkillScheduler Scheduler => _scheduler;
        public SkillBehaviour[] Skills => _skills;
    }
}
