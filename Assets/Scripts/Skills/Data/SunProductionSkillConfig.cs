using UnityEngine;

namespace PVZ
{
    [CreateAssetMenu(fileName = "SunProductionSkill", menuName = "PVZ/Skills/Sun Production", order = 2)]
    public class SunProductionSkillConfig : SkillConfigBase
    {
        [SerializeField] private int sunAmount = 25;

        public int SunAmount => sunAmount;
    }
}
