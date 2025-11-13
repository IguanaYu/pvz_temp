using UnityEngine;

namespace PVZ
{
    public class MovementSkill : SkillBehaviour
    {
        private MovementSkillConfig _movementConfig;

        public Vector3 LastDisplacement { get; private set; }

        public override void Initialize(UnitEntity owner, SkillScheduler scheduler)
        {
            base.Initialize(owner, scheduler);
            _movementConfig = Config as MovementSkillConfig;
        }

        protected override bool Perform(float deltaTime)
        {
            if (_movementConfig == null || Owner == null || !Owner.IsAlive)
            {
                return false;
            }

            var stats = Owner.Stats;
            var displacement = _movementConfig.Direction * _movementConfig.DistancePerActivation * Mathf.Max(stats.MoveSpeed, 0f);
            Owner.transform.position += displacement;
            LastDisplacement = displacement;
            return true;
        }
    }
}
