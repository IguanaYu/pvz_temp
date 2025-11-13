using NUnit.Framework;
using UnityEngine;

namespace PVZ.Tests
{
    public class UnitEntityTests
    {
        [Test]
        public void InitializeRuntime_ClonesDefinitionStats()
        {
            var definition = ScriptableObject.CreateInstance<UnitDefinition>();
            TestHelper.SetField(definition, "baseHealth", 150);
            TestHelper.SetField(definition, "baseAttack", 40);
            TestHelper.SetField(definition, "baseMoveSpeed", 1.2f);
            TestHelper.SetField(definition, "attackInterval", 1.3f);

            var movementConfig = ScriptableObject.CreateInstance<MovementSkillConfig>();
            TestHelper.SetField(movementConfig, "distancePerActivation", 0.5f);

            var gameObject = new GameObject("unit");
            var entity = gameObject.AddComponent<UnitEntity>();
            var movementSkill = gameObject.AddComponent<MovementSkill>();
            TestHelper.SetField(entity, "definition", definition);
            TestHelper.SetField(movementSkill, "config", movementConfig);

            entity.InitializeRuntime();

            Assert.NotNull(entity.Stats);
            Assert.AreEqual(150, entity.Stats.MaxHealth);
            Assert.AreEqual(40, entity.Stats.Attack);
            Assert.AreEqual(1.2f, entity.Stats.MoveSpeed);
            Assert.AreEqual(1.3f, entity.Stats.AttackInterval);
            Assert.Contains(movementSkill, entity.Skills);
        }

        [Test]
        public void InflictDamage_TriggersDeathEvent()
        {
            var definition = ScriptableObject.CreateInstance<UnitDefinition>();
            TestHelper.SetField(definition, "baseHealth", 10);

            var gameObject = new GameObject("unit");
            var entity = gameObject.AddComponent<UnitEntity>();
            TestHelper.SetField(entity, "definition", definition);
            entity.InitializeRuntime();

            UnitEntity deadEntity = null;
            entity.Died.AddListener(e => deadEntity = e);

            entity.InflictDamage(15);

            Assert.Zero(entity.Stats.CurrentHealth);
            Assert.AreSame(entity, deadEntity);
        }
    }
}
