using UnityEngine;

namespace PVZ
{
    [CreateAssetMenu(fileName = "AttackSkill", menuName = "PVZ/Skills/Attack", order = 1)]
    public class AttackSkillConfig : SkillConfigBase
    {
        [SerializeField] private float range = 5f;
        [SerializeField] private float damageMultiplier = 1f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 5f;

        public float Range => range;
        public float DamageMultiplier => damageMultiplier;
        public GameObject ProjectilePrefab => projectilePrefab;
        public float ProjectileSpeed => projectileSpeed;
    }
}
