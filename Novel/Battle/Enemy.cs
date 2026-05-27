namespace Novel.Battle
{
    public class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }

        public Enemy(string name, int hp, int damage)
        {
            Name = name;
            HP = hp;
            Damage = damage;
        }

        public bool IsAlive()
        {
            return HP > 0;
        }
    }
}