using System.Collections.Generic;
using UnityEngine;

namespace PVZ
{
    [CreateAssetMenu(fileName = "UnitDefinition", menuName = "PVZ/Unit Definition", order = 0)]
    public class UnitDefinition : ScriptableObject
    {
        [SerializeField] private string displayName = "New Unit";
        [SerializeField] private UnitTag tag = UnitTag.Plant;
        [SerializeField] private int baseHealth = 100;
        [SerializeField] private int baseAttack = 20;
        [SerializeField] private float baseMoveSpeed = 1f;
        [SerializeField] private float attackInterval = 1.5f;
        [SerializeField] private List<SkillConfigBase> skillConfigs = new();

        public string DisplayName => displayName;
        public UnitTag Tag => tag;
        public int BaseHealth => baseHealth;
        public int BaseAttack => baseAttack;
        public float BaseMoveSpeed => baseMoveSpeed;
        public float AttackInterval => attackInterval;
        public IReadOnlyList<SkillConfigBase> SkillConfigs => skillConfigs;
    }
}
