using UnityEngine;

namespace PVZ
{
    [CreateAssetMenu(fileName = "MovementSkill", menuName = "PVZ/Skills/Movement", order = 0)]
    public class MovementSkillConfig : SkillConfigBase
    {
        [SerializeField] private float distancePerActivation = 0.25f;
        [SerializeField] private Vector3 direction = Vector3.left;

        public float DistancePerActivation => distancePerActivation;
        public Vector3 Direction => direction.normalized;
    }
}
