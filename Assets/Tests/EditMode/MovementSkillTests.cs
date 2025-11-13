using NUnit.Framework;
using UnityEngine;

namespace PVZ.Tests
{
    public class MovementSkillTests
    {
        [Test]
        public void Perform_MovesOwnerByConfiguredAmount()
        {
            var definition = ScriptableObject.CreateInstance<UnitDefinition>();
            TestHelper.SetField(definition, "baseMoveSpeed", 2f);

            var config = ScriptableObject.CreateInstance<MovementSkillConfig>();
            TestHelper.SetField(config, "distancePerActivation", 0.5f);
            TestHelper.SetField(config, "direction", Vector3.right);

            var go = new GameObject("unit");
            var entity = go.AddComponent<UnitEntity>();
            var skill = go.AddComponent<MovementSkill>();
            TestHelper.SetField(entity, "definition", definition);
            TestHelper.SetField(skill, "config", config);

            entity.InitializeRuntime();

            var initialPosition = go.transform.position;
            entity.Scheduler.Tick(1f);

            var expected = initialPosition + Vector3.right * 1f;
            Assert.AreEqual(expected, go.transform.position);
            Assert.AreEqual(Vector3.right * 1f, skill.LastDisplacement);
        }
    }
}
