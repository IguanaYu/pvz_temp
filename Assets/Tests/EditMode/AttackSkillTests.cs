using NUnit.Framework;
using UnityEngine;

namespace PVZ.Tests
{
    public class AttackSkillTests
    {
        [Test]
        public void Perform_DealsDamageToTargetInRange()
        {
            var attackerDef = ScriptableObject.CreateInstance<UnitDefinition>();
            TestHelper.SetField(attackerDef, "baseAttack", 20);
            TestHelper.SetField(attackerDef, "attackInterval", 0.5f);

            var targetDef = ScriptableObject.CreateInstance<UnitDefinition>();
            TestHelper.SetField(targetDef, "baseHealth", 50);

            var attackConfig = ScriptableObject.CreateInstance<AttackSkillConfig>();
            TestHelper.SetField(attackConfig, "range", 10f);
            TestHelper.SetField(attackConfig, "damageMultiplier", 1.5f);
            TestHelper.SetField(attackConfig, "cooldown", 0.5f);

            var attackerGo = new GameObject("attacker");
            var attacker = attackerGo.AddComponent<UnitEntity>();
            var attackSkill = attackerGo.AddComponent<AttackSkill>();
            TestHelper.SetField(attacker, "definition", attackerDef);
            TestHelper.SetField(attackSkill, "config", attackConfig);

            var targetGo = new GameObject("target");
            targetGo.transform.position = attackerGo.transform.position + Vector3.right * 2f;
            var target = targetGo.AddComponent<UnitEntity>();
            TestHelper.SetField(target, "definition", targetDef);

            attacker.InitializeRuntime();
            target.InitializeRuntime();

            attackSkill.ExplicitTarget = targetGo.transform;

            attacker.Scheduler.Tick(0.5f);

            var expectedDamage = Mathf.RoundToInt(attacker.Stats.Attack * 1.5f);
            Assert.AreEqual(targetDef.BaseHealth - expectedDamage, target.Stats.CurrentHealth);
        }
    }
}
