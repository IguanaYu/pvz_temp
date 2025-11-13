using System.Collections.Generic;
using UnityEngine;

namespace PVZ
{
    public class SkillScheduler
    {
        private readonly List<SkillBehaviour> _skills = new();

        public void Register(SkillBehaviour skill)
        {
            if (skill == null || _skills.Contains(skill))
            {
                return;
            }

            _skills.Add(skill);
        }

        public void Unregister(SkillBehaviour skill)
        {
            if (skill == null)
            {
                return;
            }

            _skills.Remove(skill);
        }

        public void Tick(float deltaTime)
        {
            if (deltaTime <= 0f)
            {
                return;
            }

            for (var i = 0; i < _skills.Count; i++)
            {
                var skill = _skills[i];
                if (skill == null)
                {
                    continue;
                }

                skill.HandleTick(deltaTime);
            }
        }

        public IReadOnlyList<SkillBehaviour> Skills => _skills;
    }
}
