namespace PVZ
{
    public class UnitStats
    {
        public UnitStats(UnitDefinition definition)
        {
            Definition = definition;
            MaxHealth = definition.BaseHealth;
            Attack = definition.BaseAttack;
            MoveSpeed = definition.BaseMoveSpeed;
            AttackInterval = definition.AttackInterval;
            CurrentHealth = MaxHealth;
        }

        public UnitDefinition Definition { get; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public float MoveSpeed { get; set; }
        public float AttackInterval { get; set; }
        public int CurrentHealth { get; set; }
    }
}
