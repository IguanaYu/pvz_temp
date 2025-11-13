using UnityEngine;
using UnityEngine.Events;

namespace PVZ
{
    public class SunProductionSkill : SkillBehaviour
    {
        private SunProductionSkillConfig _config;

        public UnityEvent<int> SunProduced { get; } = new();

        public override void Initialize(UnitEntity owner, SkillScheduler scheduler)
        {
            base.Initialize(owner, scheduler);
            _config = Config as SunProductionSkillConfig;
        }

        protected override bool Perform(float deltaTime)
        {
            if (_config == null)
            {
                return false;
            }

            SunProduced.Invoke(_config.SunAmount);
            return true;
        }
    }
}
