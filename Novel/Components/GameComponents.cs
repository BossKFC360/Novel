using System;
using System.Collections.Generic;
using System.Linq;

namespace Novel.Components
{
    public abstract class Point
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Point(string name) { Name = name; Value = 0; }
        public void Add(int amount) => Value += amount;
        public void Sub(int amount) => Value -= amount;
    }

    public class Attr : Point { public Attr(string name) : base(name) { } }
    public class Ending : Point { public Ending(string name) : base(name) { } }

    public class Player
    {
        public List<Attr> Attrs { get; set; } = new();
        public List<Ending> Endings { get; set; } = new();
        public bool HasRobot { get; set; }
        public bool HasIPad { get; set; }

        public Player()
        {
            Attrs.AddRange(new[] { new Attr("Сила"), new Attr("Интеллект"), new Attr("Харизма"), new Attr("Рассудок"), new Attr("Смелость") });
            Endings.AddRange(new[] { new Ending("Good"), new Ending("Neutral"), new Ending("Bad"), new Ending("Secret") });
        }

        public Attr GetAttr(string name) => Attrs.First(a => a.Name == name);
        public Ending GetEnding(string name) => Endings.First(e => e.Name == name);

        public string GetFinalEnding()
        {
            int g = GetEnding("Good").Value;
            int n = GetEnding("Neutral").Value;
            int b = GetEnding("Bad").Value;
            int s = GetEnding("Secret").Value;

            if (s > 0 && s >= g && s >= n && s >= b) return "Secret";
            if (g >= n && g >= b) return "Good";
            if (n >= b) return "Neutral";
            return "Bad";
        }

        public void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n========== СТАТИСТИКА ==========");
            foreach (var a in Attrs) Console.WriteLine($"{a.Name}: {a.Value}");
            Console.WriteLine("--- ОЧКИ КОНЦОВОК ---");
            foreach (var e in Endings) Console.WriteLine($"{e.Name}: {e.Value}");
            Console.WriteLine("==================================\n");
            Console.ResetColor();
        }
    }

    public class Choice
    {
        public string Text { get; set; }
        public Action<Player> Effect { get; set; }
        public Func<Player, bool> Condition { get; set; }
        public string NextStepId { get; set; }

        public Choice(string text, Action<Player> effect, Func<Player, bool> condition = null, string nextStepId = null)
        {
            Text = text;
            Effect = effect ?? ((p) => { });
            Condition = condition ?? ((p) => true);
            NextStepId = nextStepId;
        }

        public bool IsAvailable(Player player) => Condition(player);
    }

    public class Step
    {
        public string Id { get; set; }
        public string Speaker { get; set; }
        public string Text { get; set; }
        public List<Choice> Choices { get; set; } = new();
        public Action<Player> MiniGame { get; set; }
        public bool IsEnding { get; set; }

        public Step(string id, string speaker, string text) { Id = id; Speaker = speaker; Text = text; }
        public Step(string id, List<Choice> choices) { Id = id; Choices = choices; }
        public Step(string id, Action<Player> miniGame) { Id = id; MiniGame = miniGame; }
        public Step(string id, bool isEnding) { Id = id; IsEnding = isEnding; }
    }

    public class Chapter
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<Step> Steps { get; set; } = new();

        public Chapter(string id, string title) { Id = id; Title = title; }

        public void AddDialog(string stepId, string speaker, string text) => Steps.Add(new Step(stepId, speaker, text));
        public void AddChoice(string stepId, List<Choice> choices) => Steps.Add(new Step(stepId, choices));
        public void AddMiniGame(string stepId, Action<Player> miniGame) => Steps.Add(new Step(stepId, miniGame));
        public void AddEnding(string stepId) => Steps.Add(new Step(stepId, true));
    }
}