using System;

namespace VisualNovel
{
    public class PlayerStats
    {
        public int Strength { get; private set; }
        public int Intelligence { get; private set; }

        public int GoodEndingPoints { get; private set; }
        public int BadEndingPoints { get; private set; }

        public PlayerStats(int strength, int intelligence)
        {
            Strength = Math.Max(0, strength);
            Intelligence = Math.Max(0, intelligence);
        }

        public void AddStrength(int value)
        {
            Strength = Math.Max(0, Strength + value);
        }

        public void AddIntelligence(int value)
        {
            Intelligence = Math.Max(0, Intelligence + value);
        }

        public void AddGoodPoint()
        {
            GoodEndingPoints++;
        }

        public void AddBadPoint()
        {
            BadEndingPoints++;
        }

        public void ShowStats()
        {
            Console.WriteLine("\n===== СТАТЫ =====");
            Console.WriteLine($"Сила: {Strength}");
            Console.WriteLine($"Интеллект: {Intelligence}");
            Console.WriteLine($"Good Ending: {GoodEndingPoints}");
            Console.WriteLine($"Bad Ending: {BadEndingPoints}");
            Console.WriteLine("=================\n");
        }
    }

    public interface IHackMethod
    {
        void Execute(PlayerStats player);
    }

    public class StrengthHack : IHackMethod
    {
        public void Execute(PlayerStats player)
        {
            Console.WriteLine("\nВы пытаетесь открыть дверь СИЛОЙ...\n");

            if (player.Strength == 2)
            {
                Console.WriteLine("Дверь открывается! Внутри — оружие, энергетики и Blu-ray коллекция \"Евангелиона\".");
                Console.WriteLine("Игрок: Пистолет... и фигурка Аски. Человечество действительно погибло.");

                player.AddStrength(1);
                player.AddGoodPoint();
            }
            else
            {
                Console.WriteLine("Система блокируется. Из динамиков начинает орать \"AMOGUS\".");
                Console.WriteLine("Вам приходится уйти, пока стены не начали воспроизводить брейнрот мемы.");

                player.AddIntelligence(-1);
                player.AddBadPoint();
            }
        }
    }

    public class IntelligenceHack : IHackMethod
    {
        public void Execute(PlayerStats player)
        {
            Console.WriteLine("\nВы пытаетесь взломать дверь ИНТЕЛЛЕКТОМ...\n");

            if (player.Intelligence >= 2)
            {
                Console.WriteLine("Дверь открывается! Внутри — оружие, энергетики и Blu-ray коллекция \"Евангелиона\".");
                Console.WriteLine("Игрок: Пистолет... и фигурка Аски. Человечество действительно погибло.");

                player.AddIntelligence(1);
                player.AddGoodPoint();
            }
            else
            {
                Console.WriteLine("Система блокируется. Из динамиков начинает орать \"AMOGUS\".");
                Console.WriteLine("Вам приходится уйти, пока стены не начали воспроизводить брейнрот мемы.");

                player.AddIntelligence(-1);
                player.AddBadPoint();
            }
        }
    }

    public class HackingMiniGame
    {
        private readonly PlayerStats _player;

        public HackingMiniGame(PlayerStats player)
        {
            _player = player;
        }

        public void Start()
        {
            Console.WriteLine("=== МИНИ-ИГРА: ВЗЛОМ ЗАМКА ===");
            Console.WriteLine("1. Открыть силой");
            Console.WriteLine("2. Взломать интеллектом");

            string choice = Console.ReadLine();

            IHackMethod method = null;

            switch (choice)
            {
                case "1":
                    method = new StrengthHack();
                    break;

                case "2":
                    method = new IntelligenceHack();
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    return;
            }

            method.Execute(_player);

            _player.ShowStats();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PlayerStats player = new PlayerStats(
                strength: 2,
                intelligence: 1
            );

            HackingMiniGame game = new HackingMiniGame(player);

            game.Start();
        }
    }
}