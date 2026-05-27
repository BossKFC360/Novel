using System;

namespace Novel.Battle
{
    public class BattleSystem
    {
        private BattlePlayer player;
        private Enemy enemy;
        private Random random = new Random();

        public BattleSystem(BattlePlayer player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public bool StartBattle()
        {
            Console.WriteLine($"\n=========================================");
            Console.WriteLine($"         СРАЖЕНИЕ: {enemy.Name}");
            Console.WriteLine($"=========================================\n");

            while (player.IsAlive() && enemy.IsAlive())
            {
                Console.WriteLine($"Игрок HP: {player.HP} | Сила: {player.Strength} | Интеллект: {player.Intelligence}");
                Console.WriteLine($"{enemy.Name} HP: {enemy.HP}");
                Console.WriteLine();

                Console.WriteLine("1. Атаковать");
                Console.WriteLine("2. Мем-атака");
                Console.WriteLine("3. Защита");
                Console.Write("Выбор: ");

                string input = Console.ReadLine() ?? "";

                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        Attack();
                        break;
                    case "2":
                        MemeAttack();
                        break;
                    case "3":
                        Defend();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        continue;
                }

                if (!enemy.IsAlive())
                    break;

                EnemyTurn();
            }

            Console.WriteLine();
            if (player.IsAlive())
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("         ПОБЕДА!");
                Console.WriteLine("=========================================");
                return true;
            }
            else
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("         ПОРАЖЕНИЕ...");
                Console.WriteLine("=========================================");
                return false;
            }
        }

        private void Attack()
        {
            int damage = 3 + player.Strength + random.Next(0, 3);
            enemy.HP -= damage;
            Console.WriteLine($"Вы нанесли {damage} урона.");
        }

        private void MemeAttack()
        {
            int damage = 2 + player.Intelligence + random.Next(0, 4);

            if (player.HasRobot)
            {
                damage += 3;
                Console.WriteLine("Робо-Навальный усилил мем!");
            }

            enemy.HP -= damage;
            player.Sanity -= 1;
            Console.WriteLine($"Мем нанёс {damage} урона. Разум -1.");
        }

        private void Defend()
        {
            Console.WriteLine("Вы защищаетесь (урон уменьшен вдвое).");
            int damage = (enemy.Damage + random.Next(0, 3)) / 2;
            player.HP -= damage;
            Console.WriteLine($"Вы получили {damage} урона.");
        }

        private void EnemyTurn()
        {
            int damage = enemy.Damage + random.Next(0, 3);
            player.HP -= damage;
            Console.WriteLine($"{enemy.Name} атакует и наносит {damage} урона.");
        }
    }
}