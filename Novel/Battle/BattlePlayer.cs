namespace Novel.Battle
{
    public class BattlePlayer
    {
        public int HP { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Sanity { get; set; }
        public bool HasRobot { get; set; }

        public BattlePlayer()
        {
            HP = 30;
            Strength = 0;
            Intelligence = 0;
            Sanity = 0;
            HasRobot = false;
        }

        public bool IsAlive()
        {
            return HP > 0;
        }
    }
}