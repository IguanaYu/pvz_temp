using NUnit.Framework;
using UnityEngine;

namespace PVZ.Tests
{
    public class SunProductionSkillTests
    {
        [Test]
        public void Perform_RaisesSunProducedEvent()
        {
            var definition = ScriptableObject.CreateInstance<UnitDefinition>();
            TestHelper.SetField(definition, "baseHealth", 10);

            var config = ScriptableObject.CreateInstance<SunProductionSkillConfig>();
            TestHelper.SetField(config, "sunAmount", 50);
            TestHelper.SetField(config, "cooldown", 0.25f);

            var go = new GameObject("sunflower");
            var entity = go.AddComponent<UnitEntity>();
            var skill = go.AddComponent<SunProductionSkill>();
            TestHelper.SetField(entity, "definition", definition);
            TestHelper.SetField(skill, "config", config);

            entity.InitializeRuntime();

            var produced = 0;
            skill.SunProduced.AddListener(amount => produced += amount);

            entity.Scheduler.Tick(0.25f);
            entity.Scheduler.Tick(0.25f);

            Assert.AreEqual(100, produced);
        }
    }
}
