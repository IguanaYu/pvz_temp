using NUnit.Framework;
using UnityEngine;

namespace PVZ.Tests
{
    public class SkillSchedulerTests
    {
        private class TestSkill : SkillBehaviour
        {
            public int ExecutionCount { get; private set; }

            protected override bool Perform(float deltaTime)
            {
                ExecutionCount++;
                return true;
            }
        }

        [Test]
        public void Tick_TriggersSkillsOnCooldown()
        {
            var definition = ScriptableObject.CreateInstance<UnitDefinition>();
            TestHelper.SetField(definition, "baseHealth", 5);

            var config = ScriptableObject.CreateInstance<SunProductionSkillConfig>();
            TestHelper.SetField(config, "cooldown", 0.5f);

            var go = new GameObject("unit");
            var entity = go.AddComponent<UnitEntity>();
            var skill = go.AddComponent<TestSkill>();
            TestHelper.SetField(entity, "definition", definition);
            TestHelper.SetField(skill, "config", config);

            entity.InitializeRuntime();

            entity.Scheduler.Tick(0.5f);
            entity.Scheduler.Tick(0.5f);
            entity.Scheduler.Tick(0.5f);

            Assert.GreaterOrEqual(skill.ExecutionCount, 2);
        }
    }
}
