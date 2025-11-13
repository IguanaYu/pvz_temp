using System;

namespace PVZ
{
    [Flags]
    public enum UnitTag
    {
        None = 0,
        Plant = 1 << 0,
        Zombie = 1 << 1
    }
}
