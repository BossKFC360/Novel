using System;
using System.Collections.Generic;
using Novel.Components;
using Novel.Game;

namespace Novel.Data
{
    public static class ChaptersData
    {
        public static Dictionary<string, Chapter> Build(GameState state)
        {
            var chapters = new Dictionary<string, Chapter>();
            var p = state.Player;

            var c1 = new Chapter("chapter1", "ГЛАВА 1: ПРОБУЖДЕНИЕ");

            c1.AddDialog("c1_d1", "Рассказчик", "Вы просыпаетесь внутри криокапсулы. Тело онемело.");
            c1.AddDialog("c1_d2", "Рассказчик", "Капсула открывается с шипением. Вокруг пустыня и руины.");
            c1.AddDialog("c1_d3", "Игрок", "Что произошло? Где я?");

            var c1_choices = new List<Choice>
            {
                new Choice("[1] Взять катану", (pl) => { pl.GetAttr("Сила").Add(5); }, nextStepId: "c1_next"),
                new Choice("[2] Выпить энергетик", (pl) => { pl.GetAttr("Рассудок").Add(5); pl.GetAttr("Интеллект").Sub(2); }, nextStepId: "c1_next"),
                new Choice("[3] Взять Айпад", (pl) => { pl.GetAttr("Харизма").Add(5); pl.GetEnding("Secret").Add(3); pl.HasIPad = true; }, nextStepId: "c1_next")
            };
            c1.AddChoice("c1_choice", c1_choices);
            c1.AddDialog("c1_next", "Рассказчик", "Вы готовы идти дальше...");

            var c2 = new Chapter("chapter2", "ГЛАВА 2: ПУСТЫНЯ");

            c2.AddDialog("c2_d1", "Рассказчик", "Датчик шлема показывает: кислород в норме.");
            c2.AddDialog("c2_d2", "Рассказчик", "Воздух пригоден для дыхания. Стоит ли снимать шлем?");

            var c2_choices = new List<Choice>
            {
                new Choice("[1] Снять шлем", (pl) => { pl.GetAttr("Сила").Add(3); pl.GetAttr("Смелость").Add(5); pl.GetEnding("Good").Add(5); }, nextStepId: "c2_found_usb"),
                new Choice("[2] Оставить шлем", (pl) => { pl.GetAttr("Интеллект").Add(3); pl.GetAttr("Рассудок").Add(5); }, nextStepId: "c2_found_usb")
            };
            c2.AddChoice("c2_choice1", c2_choices);

            c2.AddDialog("c2_found_usb", "Рассказчик", "Вы находите USB-флешку с надписью VPN.");

            var c2_choices2 = new List<Choice>
            {
                new Choice("[3] Подключить флешку", (pl) => { pl.GetAttr("Интеллект").Add(5); pl.GetEnding("Secret").Add(3); }, nextStepId: "c2_end"),
                new Choice("[4] Выбросить флешку", (pl) => { pl.GetAttr("Рассудок").Add(3); }, nextStepId: "c2_end"),
                new Choice("[5] Игнорировать", (pl) => { }, nextStepId: "c2_end")
            };
            c2.AddChoice("c2_choice2", c2_choices2);
            c2.AddDialog("c2_end", "Рассказчик", "Вы продолжаете путь по пустыне...");

            var c3 = new Chapter("chapter3", "ГЛАВА 3: ВСТРЕЧА С РОБОТОМ");

            c3.AddDialog("c3_d1", "Рассказчик", "Вы находите наполовину закопанного робота.");
            c3.AddDialog("c3_d2", "Робо-Навальный", "Здравствуйте! Я Робо-Навальный. Возьмите меня с собой!");

            var c3_choices = new List<Choice>
            {
                new Choice("[1] Взять робота", (pl) => { pl.GetAttr("Харизма").Add(5); pl.GetEnding("Good").Add(5); pl.GetEnding("Secret").Add(3); pl.HasRobot = true; }, nextStepId: "c3_has_robot"),
                new Choice("[2] Оставить робота", (pl) => { pl.GetAttr("Рассудок").Sub(5); pl.GetEnding("Bad").Add(5); }, nextStepId: "c3_end")
            };
            c3.AddChoice("c3_choice1", c3_choices);

            c3.AddDialog("c3_has_robot", "Робо-Навальный", "Спасибо! Кстати... Аниме с субтитрами или пицца с ананасами?");

            var c3_choices2 = new List<Choice>
            {
                new Choice("[3] Аниме с субтитрами", (pl) => { pl.GetAttr("Интеллект").Add(5); }, nextStepId: "c3_end"),
                new Choice("[4] Пицца с ананасами", (pl) => { pl.GetAttr("Харизма").Add(5); }, nextStepId: "c3_end"),
                new Choice("[5] Оба лагеря безумны", (pl) => { pl.GetAttr("Рассудок").Add(5); }, nextStepId: "c3_end")
            };
            c3.AddChoice("c3_choice2", c3_choices2);
            c3.AddDialog("c3_end", "Рассказчик", "Вы идёте дальше...");

            var c4 = new Chapter("chapter4", "ГЛАВА 4: НАУЧНЫЙ ЦЕНТР");

            c4.AddDialog("c4_d1", "Рассказчик", "Вы доходите до разрушенного научного центра.");
            c4.AddDialog("c4_d2", "Рассказчик", "Внутри лаборатории вы видите криокапсулу.");
            c4.AddDialog("c4_d3", "Рассказчик", "На ней надпись: DO NOT OPEN");

            var c4_choices = new List<Choice>
            {
                new Choice("[1] Открыть капсулу", (pl) => { pl.GetEnding("Bad").Add(5); pl.GetAttr("Рассудок").Add(3); }, nextStepId: "c4_end"),
                new Choice("[2] Не трогать", (pl) => { pl.GetEnding("Secret").Add(3); }, nextStepId: "c4_end")
            };
            c4.AddChoice("c4_choice", c4_choices);
            c4.AddDialog("c4_end", "Рассказчик", "Вы покидаете центр и идёте дальше...");

            var c5 = new Chapter("chapter5", "ГЛАВА 5: СУПЕРМАРКЕТ");

            c5.AddDialog("c5_d1", "Рассказчик", "Вы находите заброшенный супермаркет.");
            c5.AddDialog("c5_d2", "Рассказчик", "В отделе электроники работает старый телевизор.");

            var c5_choices = new List<Choice>
            {
                new Choice("[1] Смотреть Skibidi Toilet", (pl) => { pl.GetAttr("Рассудок").Sub(5); pl.GetEnding("Bad").Add(3); }, nextStepId: "c5_end"),
                new Choice("[2] Смотреть Стинта", (pl) => { pl.GetAttr("Харизма").Add(5); }, nextStepId: "c5_end"),
                new Choice("[3] Смотреть документалку", (pl) => { pl.GetAttr("Интеллект").Add(5); pl.GetEnding("Secret").Add(3); }, nextStepId: "c5_end"),
                new Choice("[4] Разбить телевизор", (pl) => { pl.GetAttr("Сила").Add(5); }, nextStepId: "c5_end")
            };
            c5.AddChoice("c5_choice", c5_choices);
            c5.AddDialog("c5_end", "Рассказчик", "Вы набираете припасы и уходите...");

            var c6 = new Chapter("chapter6", "ГЛАВА 6: НАПАДЕНИЕ");

            c6.AddDialog("c6_d1", "Рассказчик", "Ночь опускается. Вокруг начинают мелькать тени.");
            c6.AddDialog("c6_d2", "Рассказчик", "Из темноты появляются монстры!");

            var c6_choices = new List<Choice>
            {
                new Choice("[1] Спрятаться", (pl) => { pl.GetAttr("Рассудок").Add(5); pl.GetAttr("Интеллект").Add(3); pl.GetEnding("Neutral").Add(5); }, nextStepId: "c6_end"),
                new Choice("[2] Атаковать", (pl) => { pl.GetAttr("Сила").Add(5); pl.GetEnding("Good").Add(5); }, nextStepId: "c6_end")
            };

            if (state.Player.HasRobot)
            {
                c6_choices.Add(new Choice("[3] Попросить робота помочь", (pl) => { pl.GetEnding("Good").Add(10); }, nextStepId: "c6_end"));
            }

            c6.AddChoice("c6_choice", c6_choices);
            c6.AddDialog("c6_end", "Рассказчик", "Вы выжили и идёте дальше...");

            var c7 = new Chapter("chapter7", "ГЛАВА 7: ПУТЬ К АЭРОПОРТУ");

            c7.AddDialog("c7_d1", "Рассказчик", "На следующий день вы добираетесь до аэропорта.");
            c7.AddDialog("c7_d2", "Рассказчик", "На стене разрушенного метро вы видите граффити.");
            c7.AddDialog("c7_d3", "Рассказчик", "Оставьте свой след в истории...");

            var c7_choices = new List<Choice>
            {
                new Choice("[1] Написать ЦОЙ ЖИВ", (pl) => { }, nextStepId: "c7_end"),
                new Choice("[2] Написать мат", (pl) => { }, nextStepId: "c7_end"),
                new Choice("[3] Нарисовать амогуса", (pl) => { }, nextStepId: "c7_end")
            };
            c7.AddChoice("c7_choice", c7_choices);
            c7.AddDialog("c7_end", "Рассказчик", "Вы идёте к самолёту...");

            var c8 = new Chapter("chapter8", "ГЛАВА 8: АЭРОПОРТ");

            c8.AddDialog("c8_d1", "Рассказчик", "В ангаре вы находите старый самолёт.");

            int strength = state.Player.GetAttr("Сила").Value;
            int intellect = state.Player.GetAttr("Интеллект").Value;

            string fuelMessage = strength >= 7 ? "С силой открываете резервуар! Топливо найдено." :
                                 intellect >= 7 ? "По старым схемам находите топливо!" :
                                 state.Player.HasRobot ? "Робот помог найти топливо!" :
                                 "Чудом находите немного топлива.";

            c8.AddDialog("c8_d2", "Рассказчик", fuelMessage);
            c8.AddDialog("c8_d3", "Рассказчик", "Самолет готов к взлету...");

            var endGood = new Chapter("ending_good", "ХОРОШАЯ КОНЦОВКА");
            endGood.AddDialog("eg1", "Рассказчик", "Вы взлетаете и находите зелёный остров с выжившими!");
            endGood.AddDialog("eg2", "Рассказчик", "Вас встречают: Гатс, CJ, Курт Кобейн и другие.");
            if (state.Player.HasRobot) endGood.AddDialog("eg3", "Робо-Навальный", "Дружба - это сила!");
            endGood.AddDialog("eg4", "Рассказчик", "История только начинается...");
            endGood.AddEnding("eg_end");

            var endNeutral = new Chapter("ending_neutral", "НЕЙТРАЛЬНАЯ КОНЦОВКА");
            endNeutral.AddDialog("en1", "Рассказчик", "Вы находите убежище. Оно пустое.");
            endNeutral.AddDialog("en2", "Рассказчик", "Живёте один, смотрите старый телевизор.");
            endNeutral.AddDialog("en3", "Рассказчик", "Иногда вам кажется, что где-то играет SKIBIDI...");
            endNeutral.AddEnding("en_end");

            var endBad = new Chapter("ending_bad", "ПЛОХАЯ КОНЦОВКА");
            endBad.AddDialog("eb1", "Рассказчик", "Силы покидают вас. Песок засыпает следы.");
            endBad.AddDialog("eb2", "Рассказчик", "Небо становится красным. Вдалеке приближается тень.");
            endBad.AddDialog("eb3", "Рассказчик", "Последнее, что вы слышите: bruh...");
            endBad.AddEnding("eb_end");

            var endSecret = new Chapter("ending_secret", "СЕКРЕТНАЯ КОНЦОВКА");
            endSecret.AddDialog("es1", "Рассказчик", "Вы находите секретный бункер NASA!");
            endSecret.AddDialog("es2", "Рассказчик", "Внутри - машина времени.");
            endSecret.AddDialog("es3", "Рассказчик", "На экране: RETURN TO 2020?");
            endSecret.AddDialog("es4", "Рассказчик", "Вы возвращаетесь в прошлое...");
            endSecret.AddDialog("es5", "Рассказчик", "За окном 2020 год. На столе - экзаменационные билеты.");
            endSecret.AddDialog("es6", "Рассказчик", "Теперь вы знаете, что ждёт человечество...");
            endSecret.AddDialog("es7", "Рассказчик", "КОНЕЦ? ИЛИ НАЧАЛО?");
            endSecret.AddEnding("es_end");

            chapters["chapter1"] = c1;
            chapters["chapter2"] = c2;
            chapters["chapter3"] = c3;
            chapters["chapter4"] = c4;
            chapters["chapter5"] = c5;
            chapters["chapter6"] = c6;
            chapters["chapter7"] = c7;
            chapters["chapter8"] = c8;
            chapters["ending_good"] = endGood;
            chapters["ending_neutral"] = endNeutral;
            chapters["ending_bad"] = endBad;
            chapters["ending_secret"] = endSecret;

            return chapters;
        }
    }
}