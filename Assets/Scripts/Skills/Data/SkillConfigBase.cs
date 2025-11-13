using UnityEngine;

namespace PVZ
{
    public abstract class SkillConfigBase : ScriptableObject
    {
        [SerializeField, Min(0f)] private float cooldown = 1f;
        [SerializeField] private bool enabledByDefault = true;

        public float Cooldown => cooldown;
        public bool EnabledByDefault => enabledByDefault;
    }
}
