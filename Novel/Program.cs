using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Novel.Components;
using Novel.Game;
using Novel.Data;

namespace Novel
{
    class Program
    {
        private static GameState _state;
        private static Dictionary<string, Chapter> _chapters;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Визуальная новелла";
            Console.CursorVisible = false;

            _state = new GameState();
            _chapters = ChaptersData.Build(_state);

            RunGame();
        }

        static void RunGame()
        {
            string currentChapterId = "chapter1";

            while (_chapters.ContainsKey(currentChapterId))
            {
                var chapter = _chapters[currentChapterId];

                bool chapterComplete = false;
                int stepIndex = 0;
                bool isEndingReached = false;

                while (!chapterComplete && stepIndex < chapter.Steps.Count)
                {
                    var step = chapter.Steps[stepIndex];

                    if (step.Speaker != null)
                    {
                        TypeText(step.Speaker, step.Text);
                        WaitForContinue();
                        stepIndex++;
                    }
                    else if (step.Choices.Count > 0)
                    {
                        var available = step.Choices.Where(c => c.IsAvailable(_state.Player)).ToList();

                        switch (available.Count)
                        {
                            case 0:
                                Console.WriteLine("\n[НЕТ ДОСТУПНЫХ ВАРИАНТОВ]");
                                return;
                            default:
                                int selected = ShowMenu(available);
                                var choice = available[selected];

                                var oldStats = CaptureStats(_state.Player);
                                choice.Effect?.Invoke(_state.Player);
                                ShowStatChanges(oldStats, _state.Player);

                                string nextStepId = choice.NextStepId;

                                if (string.IsNullOrEmpty(nextStepId))
                                {
                                    stepIndex++;
                                }
                                else
                                {
                                    int nextIndex = chapter.Steps.FindIndex(s => s.Id == nextStepId);
                                    stepIndex = nextIndex >= 0 ? nextIndex : stepIndex + 1;
                                }
                                break;
                        }
                    }
                    else if (step.IsEnding)
                    {
                        isEndingReached = true;
                        chapterComplete = true;
                        break;
                    }
                    else
                    {
                        stepIndex++;
                    }
                }

                if (isEndingReached)
                {
                    break;
                }

                string nextChapterId = GetNextChapter(currentChapterId);

                if (nextChapterId == "chapter8")
                {
                    nextChapterId = $"ending_{_state.Player.GetFinalEnding().ToLower()}";
                }

                currentChapterId = nextChapterId;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n[ИГРА ЗАВЕРШЕНА]");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        static Dictionary<string, int> CaptureStats(Player player)
        {
            var stats = new Dictionary<string, int>();
            foreach (var attr in player.Attrs)
            {
                stats[$"attr_{attr.Name}"] = attr.Value;
            }
            foreach (var ending in player.Endings)
            {
                stats[$"ending_{ending.Name}"] = ending.Value;
            }
            stats["HasRobot"] = player.HasRobot ? 1 : 0;
            stats["HasIPad"] = player.HasIPad ? 1 : 0;
            return stats;
        }

        static void ShowStatChanges(Dictionary<string, int> oldStats, Player player)
        {
            bool hasChanges = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[ВЫ ПОЛУЧИЛИ:]");

            foreach (var attr in player.Attrs)
            {
                string key = $"attr_{attr.Name}";
                int oldValue = oldStats.ContainsKey(key) ? oldStats[key] : 0;
                int diff = attr.Value - oldValue;

                switch (diff)
                {
                    case > 0:
                        Console.WriteLine($"  + {diff} {attr.Name}");
                        hasChanges = true;
                        break;
                    case < 0:
                        Console.WriteLine($"  - {Math.Abs(diff)} {attr.Name}");
                        hasChanges = true;
                        break;
                }
            }

            foreach (var ending in player.Endings)
            {
                string key = $"ending_{ending.Name}";
                int oldValue = oldStats.ContainsKey(key) ? oldStats[key] : 0;
                int diff = ending.Value - oldValue;

                switch (diff)
                {
                    case > 0:
                        Console.WriteLine($"  + {diff} {ending.Name} Ending");
                        hasChanges = true;
                        break;
                    case < 0:
                        Console.WriteLine($"  - {Math.Abs(diff)} {ending.Name} Ending");
                        hasChanges = true;
                        break;
                }
            }

            if (!oldStats.ContainsKey("HasRobot") || oldStats["HasRobot"] == 0 && player.HasRobot)
            {
                Console.WriteLine("  + Получен союзник: Робот");
                hasChanges = true;
            }

            if (!oldStats.ContainsKey("HasIPad") || oldStats["HasIPad"] == 0 && player.HasIPad)
            {
                Console.WriteLine("  + Получен предмет: Айпад");
                hasChanges = true;
            }

            if (!hasChanges)
            {
                Console.WriteLine("  (ничего не изменилось)");
            }

            Console.ResetColor();
            Thread.Sleep(1500);
        }

        static void TypeText(string speaker, string text)
        {
            _state.AddHistory(speaker, text);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{speaker}]: ");
            Console.ResetColor();

            switch (_state.SkipMode)
            {
                case true:
                    Console.WriteLine(text);
                    return;
                default:
                    int delay = _state.FastMode ? 10 : 40;
                    foreach (char c in text)
                    {
                        Console.Write(c);
                        Thread.Sleep(delay);
                    }
                    Console.WriteLine();
                    break;
            }
        }

        static void WaitForContinue()
        {
            switch (_state.SkipMode)
            {
                case true:
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[Нажмите любую клавишу...]");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    break;
            }
        }

        static int ShowMenu(List<Choice> choices)
        {
            int index = 0;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========== ВЫБЕРИТЕ ДЕЙСТВИЕ ==========\n");
                Console.ResetColor();

                for (int i = 0; i < choices.Count; i++)
                {
                    switch (i == index)
                    {
                        case true:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write($"> {choices[i].Text}");
                            Console.ResetColor();
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine($"  {choices[i].Text}");
                            break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n--- УПРАВЛЕНИЕ ---");
                Console.WriteLine("Стрелки - выбор | Enter - подтвердить");
                Console.WriteLine("H - история | S - статы | F - ускорение | K - пропуск");
                Console.ResetColor();

                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        index = (index - 1 + choices.Count) % choices.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        index = (index + 1) % choices.Count;
                        break;
                    case ConsoleKey.Enter:
                        return index;
                    case ConsoleKey.H:
                        _state.ShowHistory();
                        break;
                    case ConsoleKey.S:
                        _state.Player.ShowStats();
                        Console.WriteLine("Нажмите любую клавишу...");
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.F:
                        _state.ToggleFast();
                        break;
                    case ConsoleKey.K:
                        _state.ToggleSkip();
                        break;
                }
            }
        }

        static string GetNextChapter(string current)
        {
            var order = new[] { "chapter1", "chapter2", "chapter3", "chapter4", "chapter5", "chapter6", "chapter7", "chapter8" };
            int idx = Array.IndexOf(order, current);

            if (idx >= order.Length - 1)
            {
                return $"ending_{_state.Player.GetFinalEnding().ToLower()}";
            }

            return idx >= 0 ? order[idx + 1] : "ending_neutral";
        }
    }
}