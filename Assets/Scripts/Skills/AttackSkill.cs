using UnityEngine;
using UnityEngine.Events;

namespace PVZ
{
    public class AttackSkill : SkillBehaviour
    {
        [SerializeField] private Transform explicitTarget;

        private AttackSkillConfig _config;

        public UnityEvent<UnitEntity, UnitEntity, int> AttackPerformed { get; } = new();

        public Transform ExplicitTarget
        {
            get => explicitTarget;
            set => explicitTarget = value;
        }

        public override void Initialize(UnitEntity owner, SkillScheduler scheduler)
        {
            base.Initialize(owner, scheduler);
            _config = Config as AttackSkillConfig;
        }

        protected override bool Perform(float deltaTime)
        {
            if (_config == null || Owner == null || !Owner.IsAlive)
            {
                return false;
            }

            var targetTransform = ResolveTarget();
            if (targetTransform == null)
            {
                return false;
            }

            var distance = Vector3.Distance(Owner.transform.position, targetTransform.position);
            if (distance > _config.Range)
            {
                return false;
            }

            var damage = Mathf.RoundToInt(Owner.Stats.Attack * _config.DamageMultiplier);
            var targetEntity = targetTransform.GetComponent<UnitEntity>();
            AttackPerformed.Invoke(Owner, targetEntity, damage);

            if (_config.ProjectilePrefab != null)
            {
                SpawnProjectile(targetTransform, targetEntity, damage);
            }
            else if (targetEntity != null)
            {
                targetEntity.InflictDamage(damage);
            }

            return true;
        }

        private Transform ResolveTarget()
        {
            if (explicitTarget != null)
            {
                return explicitTarget;
            }

            return null;
        }

        private void SpawnProjectile(Transform targetTransform, UnitEntity targetEntity, int damage)
        {
            var instance = Instantiate(_config.ProjectilePrefab, Owner.transform.position, Quaternion.identity);
            if (instance.TryGetComponent(out Projectile projectile))
            {
                projectile.Initialize(Owner, targetEntity, damage, _config.ProjectileSpeed, targetTransform.position);
            }
        }
    }
}
