using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Novel.Components;

namespace Novel.Game
{
    public class GameState
    {
        public Player Player { get; set; }
        public List<(string speaker, string text)> History { get; set; } = new();
        public bool FastMode { get; set; }
        public bool SkipMode { get; set; }

        public GameState()
        {
            Player = new Player();
        }

        public void AddHistory(string speaker, string text)
        {
            History.Add((speaker, text));
        }

        public void ShowHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("========== ИСТОРИЯ ==========\n");
            foreach (var h in History.TakeLast(30))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"[{h.speaker}]: ");
                Console.ResetColor();
                Console.WriteLine(h.text);
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n==============================");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public void ToggleFast()
        {
            FastMode = !FastMode;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(FastMode ? "[УСКОРЕНИЕ ВКЛЮЧЕНО]" : "[УСКОРЕНИЕ ВЫКЛЮЧЕНО]");
            Console.ResetColor();
            Thread.Sleep(300);
        }

        public void ToggleSkip()
        {
            SkipMode = !SkipMode;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(SkipMode ? "[ПРОПУСК ВКЛЮЧЕН]" : "[ПРОПУСК ВЫКЛЮЧЕН]");
            Console.ResetColor();
            Thread.Sleep(300);
        }
    }
}