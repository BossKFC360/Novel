using System;
using System.Collections.Generic;
using Novel.Components;
using Novel.Game;
using Novel.Battle;

namespace Novel.Data
{
    public static class ChaptersData
    {
        public static Dictionary<string, Chapter> Build(GameState state)
        {
            var chapters = new Dictionary<string, Chapter>();
            var p = state.Player;

            var c1 = new Chapter("chapter1", "ГЛАВА 1: ПРОБУЖДЕНИЕ");
            c1.AddDialog("c1_d1", "Рассказчик", "Вы просыпаетесь внутри криокапсулы. Тело онемело, мысли путаются. На вас скафандр с логотипом \"IT TOP\".");
            c1.AddDialog("c1_d2", "Рассказчик", "Капсула открывается с шипением. Сквозь белый свет проходит силуэт Шрека в дыме вейпа.");
            c1.AddDialog("c1_d3", "Рассказчик", "Вокруг — пустыня. Песок засыпал руины Макдональдса и гигантской статуи Гигачада.");
            c1.AddDialog("c1_d4", "Игрок", "Что произошло? Почему в небе гигантское лицо Алексея Фомина?");
            c1.AddDialog("c1_d5", "Рассказчик", "Память возвращается фрагментами. Зачёт. Билеты. Бессонная ночь. Вы согласились на эксперимент, потому что Алексей пообещал автомат по Шарпам.");

            var c1_choices = new List<Choice>
            {
                new Choice("[1] Взять катану", (pl) => { pl.GetAttr("Сила").Add(1); }, nextStepId: "c1_end"),
                new Choice("[2] Выпить энергетик", (pl) => { pl.GetAttr("Рассудок").Add(1); pl.GetAttr("Интеллект").Sub(1); }, nextStepId: "c1_end"),
                new Choice("[3] Взять Айпад", (pl) => { pl.GetAttr("Харизма").Add(1); pl.GetEnding("Secret").Add(1); pl.HasIPad = true; }, nextStepId: "c1_end")
            };
            c1.AddChoice("c1_choice", c1_choices);
            c1.AddDialog("c1_end", "Рассказчик", "Вы готовы идти дальше...");

            var c2 = new Chapter("chapter2", "ГЛАВА 2: ПУСТЫНЯ");
            c2.AddDialog("c2_d1", "Рассказчик", "Датчик шлема сообщает: кислород — нормальный.");
            c2.AddDialog("c2_d2", "Рассказчик", "Воздух пригоден для дыхания. Но стоит ли снимать шлем, если где-то рядом может быть ТунТунСахур?");

            var c2_first_choice = new List<Choice>
            {
                new Choice("[1] Снять шлем", (pl) => {
                    pl.GetAttr("Сила").Add(1);
                    pl.GetAttr("Смелость").Add(1);
                    pl.GetEnding("Good").Add(1);
                }, nextStepId: "PATH1_START"),

                new Choice("[2] Оставить шлем", (pl) => {
                    pl.GetAttr("Интеллект").Add(1);
                    pl.GetAttr("Рассудок").Add(1);
                }, nextStepId: "PATH2_START")
            };
            c2.AddChoice("c2_first_choice", c2_first_choice);

            c2.AddDialog("PATH1_START", "Рассказчик", "Вы снимаете шлем. Из динамиков где-то вдали начинает играть \"Can You Feel My Heart\".");
            c2.AddDialog("PATH1_2", "Игрок", "*глубокий вдох* Почему воздух пахнет энергетиком и горелой пластмассой?");
            c2.AddDialog("PATH1_3", "Рассказчик", "Вы кашляете, а потом замечаете в песке идеально сохранившийся спиннер.");
            c2.AddDialog("PATH1_4", "Рассказчик", "Теперь вы чувствуете ветер, запах пыли и присутствие Райана Гослинга где-то неподалеку.");
            c2.AddDialog("PATH1_5", "Рассказчик", "Вдалеке вы замечаете разрушенный рекламный щит с надписью: \"РАБОТА ВПН\".");
            c2.AddDialog("PATH1_6", "Рассказчик", "За щитом скрывается USB-флешка с надписью \"VPN\".");
            c2.AddDialog("PATH1_7", "Игрок", "Слава Богу, сюда не добрались РКН.");

            var c2_path1_choices = new List<Choice>
            {
                new Choice("[1] Подключить флешку к браслету", (pl) => { pl.GetAttr("Интеллект").Add(1); pl.GetEnding("Secret").Add(1); }, nextStepId: "PATH1_SUCCESS"),
                new Choice("[2] Выбросить флешку", (pl) => { pl.GetAttr("Рассудок").Add(1); }, nextStepId: "PATH1_THROW"),
                new Choice("[3] Игнорировать", (pl) => { pl.GetEnding("Neutral").Add(1); }, nextStepId: "PATH1_IGNORE")
            };
            c2.AddChoice("c2_path1_choices", c2_path1_choices);

            c2.AddDialog("PATH1_SUCCESS", "Рассказчик", "Перед глазами появляется интерфейс Windows XP.");
            c2.AddDialog("PATH1_SUCCESS2", "Система", "Установлен навык: Иноагент.");
            c2.AddDialog("PATH1_SUCCESS3", "Рассказчик", "Вы продолжаете путь по пустыне...");

            c2.AddDialog("PATH1_THROW", "Рассказчик", "Флешка начинает орать \"AUUUUGHHH\" и взрывается.");
            c2.AddDialog("PATH1_THROW2", "Рассказчик", "Вы отходите подальше, чтобы не пострадать.");
            c2.AddDialog("PATH1_THROW3", "Рассказчик", "Вы продолжаете путь по пустыне...");

            c2.AddDialog("PATH1_IGNORE", "Рассказчик", "Вы решаете не трогать флешку и идёте дальше.");
            c2.AddDialog("PATH1_IGNORE2", "Рассказчик", "Кто знает, что могло быть на этом носителе...");
            c2.AddDialog("PATH1_IGNORE3", "Рассказчик", "Вы продолжаете путь по пустыне...");

            c2.AddDialog("PATH2_START", "Рассказчик", "Вы решаете не рисковать. В этом мире любой микроб может оказаться подписчиком Влада А4.");
            c2.AddDialog("PATH2_2", "Рассказчик", "Шлем остается на месте. Визор автоматически включает Subway Surfers, чтобы вы не заскучали.");
            c2.AddDialog("PATH2_3", "Рассказчик", "Вдалеке вы замечаете разрушенный рекламный щит с надписью: \"РАБОТА ВПН\".");
            c2.AddDialog("PATH2_4", "Рассказчик", "За щитом скрывается USB-флешка с надписью \"VPN\".");
            c2.AddDialog("PATH2_5", "Игрок", "Слава Богу, сюда не добрались РКН.");

            var c2_path2_choices = new List<Choice>
            {
                new Choice("[1] Подключить флешку к браслету", (pl) => { pl.GetAttr("Интеллект").Add(1); pl.GetEnding("Secret").Add(1); }, nextStepId: "PATH2_SUCCESS"),
                new Choice("[2] Выбросить флешку", (pl) => { pl.GetAttr("Рассудок").Add(1); }, nextStepId: "PATH2_THROW"),
                new Choice("[3] Игнорировать", (pl) => { pl.GetEnding("Neutral").Add(1); }, nextStepId: "PATH2_IGNORE")
            };
            c2.AddChoice("c2_path2_choices", c2_path2_choices);

            c2.AddDialog("PATH2_SUCCESS", "Рассказчик", "Перед глазами появляется интерфейс Windows XP.");
            c2.AddDialog("PATH2_SUCCESS2", "Система", "Установлен навык: Иноагент.");
            c2.AddDialog("PATH2_SUCCESS3", "Рассказчик", "Вы продолжаете путь по пустыне...");

            c2.AddDialog("PATH2_THROW", "Рассказчик", "Флешка начинает орать \"AUUUUGHHH\" и взрывается.");
            c2.AddDialog("PATH2_THROW2", "Рассказчик", "Вы отходите подальше, чтобы не пострадать.");
            c2.AddDialog("PATH2_THROW3", "Рассказчик", "Вы продолжаете путь по пустыне...");

            c2.AddDialog("PATH2_IGNORE", "Рассказчик", "Вы решаете не трогать флешку и идёте дальше.");
            c2.AddDialog("PATH2_IGNORE2", "Рассказчик", "Кто знает, что могло быть на этом носителе...");
            c2.AddDialog("PATH2_IGNORE3", "Рассказчик", "Вы продолжаете путь по пустыне...");

            var c3 = new Chapter("chapter3", "ГЛАВА 3: ВСТРЕЧА С РОБОТОМ");
            c3.AddDialog("c3_d1", "Рассказчик", "Вы обыскиваете окрестности и находите наполовину закопанного робота с лицом Навального и телом из деталей Xbox Series X.");
            c3.AddDialog("c3_d2", "Рассказчик", "Рядом лежит планшет Samsung Galaxy S42 Ultra с картой памяти.");
            c3.AddDialog("c3_d3", "Робо-Навальный", "Здравствуйте! Я Робо-Навальный версии 6.7. Мой процессор работает на донатах и ненависти к стульям.");
            c3.AddDialog("c3_d4", "Игрок", "Ты... функционируешь? После всего этого?");
            c3.AddDialog("c3_d5", "Робо-Навальный", "Да. После апокалипсиса выжили только тараканы, Nokia 3310 и я.");

            var c3_choices1 = new List<Choice>
            {
                new Choice("[1] Взять робота с собой", (pl) => {
                    pl.GetAttr("Харизма").Add(1);
                    pl.GetEnding("Good").Add(1);
                    pl.GetEnding("Secret").Add(1);
                    pl.HasRobot = true;
                }, nextStepId: "c3_take_robot"),
                new Choice("[2] Оставить робота", (pl) => {
                    pl.GetAttr("Рассудок").Sub(1);
                    pl.GetEnding("Bad").Add(1);
                }, nextStepId: "c3_leave_robot")
            };
            c3.AddChoice("c3_choice1", c3_choices1);

            c3.AddDialog("c3_take_robot", "Игрок", "Ладно, Робо-Навальный. Только без политики, пж.");
            c3.AddDialog("c3_take_robot2", "Робо-Навальный", "Не обещаю. Также у меня встроен VPN и 40 терабайт компромата на... человечество.");
            c3.AddDialog("c3_take_robot3", "Рассказчик", "Робот довольно пищит и включает phonk-ремикс гимна России.");
            c3.AddDialog("c3_take_robot4", "Рассказчик", "Вы впервые за долгое время ощущаете, что одиночество отступает.");
            c3.AddDialog("c3_take_robot5", "Робо-Навальный", "Кстати. Перед войной люди разделились на два лагеря.");
            c3.AddDialog("c3_take_robot6", "Игрок", "Какие еще лагеря?");
            c3.AddDialog("c3_take_robot7", "Робо-Навальный", "Те, кто смотрел аниме с субтитрами… и те, кто ел пиццу с ананасами.");

            var c3_take_choices = new List<Choice>
            {
                new Choice("[1] Поддержать анимешников", (pl) => { pl.GetAttr("Интеллект").Add(1); }, nextStepId: "c3_end"),
                new Choice("[2] Поддержать ананасовую пиццу", (pl) => { pl.GetAttr("Харизма").Add(1); }, nextStepId: "c3_end"),
                new Choice("[3] Сказать \"оба лагеря безумны\"", (pl) => { pl.GetAttr("Рассудок").Add(1); }, nextStepId: "c3_end")
            };
            c3.AddChoice("c3_take_choice", c3_take_choices);

            c3.AddDialog("c3_leave_robot", "Игрок", "Нет. После Detroit: Become Human я вам не доверяю.");
            c3.AddDialog("c3_leave_robot2", "Робо-Навальный", "Понимаю. Значит, снова смотреть мемы одному…");
            c3.AddDialog("c3_leave_robot3", "Рассказчик", "Экран робота медленно тухнет. Последнее, что вы слышите — грустный тромбон.");
            c3.AddDialog("c3_end", "Рассказчик", "Вы идёте дальше...");

            var c4 = new Chapter("chapter4", "ГЛАВА 4: НАУЧНЫЙ ЦЕНТР");
            c4.AddDialog("c4_d1", "Рассказчик", "Через несколько часов пути вы находите разрушенный научный центр \"Black Mesa Disneyland Edition\".");
            c4.AddDialog("c4_d2", "Рассказчик", "Внутри — массивная дверь с экраном, на котором бесконечно крутится \"Bad Apple\".");
            c4.AddDialog("c4_d3", "Рассказчик", "Вам нужно взломать замок.");

            c4.AddMiniGame("c4_minigame", (pl) => {
                Console.WriteLine("\n=========================================");
                Console.WriteLine("         МИНИ-ИГРА: ВЗЛОМ ЗАМКА");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Открыть силой");
                Console.WriteLine("2. Взломать интеллектом");
                Console.Write("Выберите способ: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nВы пытаетесь открыть дверь СИЛОЙ...");
                        if (pl.GetAttr("Сила").Value >= 2)
                        {
                            Console.WriteLine("Дверь открывается! Внутри — оружие, энергетики и Blu-ray коллекция \"Евангелиона\".");
                            Console.WriteLine("Игрок: Пистолет... и фигурка Аски. Человечество действительно погибло.");
                            pl.GetAttr("Сила").Add(1);
                            pl.GetEnding("Good").Add(1);
                        }
                        else
                        {
                            Console.WriteLine("Система блокируется. Из динамиков начинает орать \"AMOGUS\".");
                            Console.WriteLine("Вам приходится уйти, пока стены не начали воспроизводить брейнрот мемы.");
                            pl.GetAttr("Интеллект").Sub(1);
                            pl.GetEnding("Bad").Add(1);
                        }
                        break;
                    case "2":
                        Console.WriteLine("\nВы пытаетесь взломать дверь ИНТЕЛЛЕКТОМ...");
                        if (pl.GetAttr("Интеллект").Value >= 2)
                        {
                            Console.WriteLine("Дверь открывается! Внутри — оружие, энергетики и Blu-ray коллекция \"Евангелиона\".");
                            Console.WriteLine("Игрок: Пистолет... и фигурка Аски. Человечество действительно погибло.");
                            pl.GetAttr("Интеллект").Add(1);
                            pl.GetEnding("Good").Add(1);
                        }
                        else
                        {
                            Console.WriteLine("Система блокируется. Из динамиков начинает орать \"AMOGUS\".");
                            Console.WriteLine("Вам приходится уйти, пока стены не начали воспроизводить брейнрот мемы.");
                            pl.GetAttr("Интеллект").Sub(1);
                            pl.GetEnding("Bad").Add(1);
                        }
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Дверь остаётся закрытой.");
                        break;
                }
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey(true);
            });

            c4.AddDialog("c4_after_hack", "Рассказчик", "Вы проходите дальше в лабораторию.");
            c4.AddDialog("c4_capsule", "Рассказчик", "В лаборатории вы находите криокапсулу с надписью \"DO NOT OPEN\".");

            var c4_choices = new List<Choice>
            {
                new Choice("[1] Открыть капсулу", (pl) => {
                    pl.GetEnding("Bad").Add(1);
                    pl.GetAttr("Рассудок").Add(1);
                }, nextStepId: "c4_open"),
                new Choice("[2] Не трогать", (pl) => {
                    pl.GetEnding("Secret").Add(1);
                }, nextStepId: "c4_ignore")
            };
            c4.AddChoice("c4_choice", c4_choices);

            c4.AddDialog("c4_open", "Рассказчик", "Из капсулы вываливается человек в зеленой маске и моментально убегает со скоростью света.");
            c4.AddDialog("c4_open2", "Игрок", "Это был… Dream?");
            if (p.HasRobot) c4.AddDialog("c4_open3", "Робо-Навальный", "Хуже. Это был его фанат.");
            else c4.AddDialog("c4_open3", "Рассказчик", "Вы так и не поняли, кто это был.");
            c4.AddDialog("c4_open4", "Рассказчик", "Вы покидаете центр и идёте дальше...");

            c4.AddDialog("c4_ignore", "Рассказчик", "Изнутри капсулы кто-то тихо шепчет: \"спидран…\"");
            c4.AddDialog("c4_ignore2", "Рассказчик", "Вы покидаете центр и идёте дальше...");

            var c5 = new Chapter("chapter5", "ГЛАВА 5: СУПЕРМАРКЕТ");
            c5.AddDialog("c5_d1", "Рассказчик", "Вы находите заброшенный супермаркет \"Пятёрочка X Cyberpunk 2077\".");
            c5.AddDialog("c5_d2", "Игрок", "Наконец-то. Если тут есть дошик — человечество еще можно спасти.");
            c5.AddDialog("c5_d3", "Рассказчик", "Среди полок лежат консервы, бутылка ПСЫЖа и пачка сухариков \"3 КОРОЧКИ\".");
            c5.AddDialog("c5_d4", "Рассказчик", "Солнце медленно тонет в песке. Где-то вдалеке слышится \"ГОЙДА\".");
            c5.AddDialog("c5_d5", "Рассказчик", "В отделе электроники все еще работает один телевизор.");

            var c5_choices = new List<Choice>
            {
                new Choice("[1] Смотреть Skibidi Toilet", (pl) => {
                    pl.GetAttr("Рассудок").Sub(1);
                    pl.GetEnding("Bad").Add(1);
                }, nextStepId: "c5_skibidi"),
                new Choice("[2] Смотреть Стинта", (pl) => {
                    pl.GetAttr("Харизма").Add(1);
                }, nextStepId: "c5_stint"),
                new Choice("[3] Смотреть документалку", (pl) => {
                    pl.GetAttr("Интеллект").Add(1);
                    pl.GetEnding("Secret").Add(1);
                }, nextStepId: "c5_doc"),
                new Choice("[4] Разбить телевизор", (pl) => {
                    pl.GetAttr("Сила").Add(1);
                }, nextStepId: "c5_break")
            };
            c5.AddChoice("c5_choice", c5_choices);

            c5.AddDialog("c5_skibidi", "Рассказчик", "Вы смотрите на бессмысленные движения головой. Ваш рассудок медленно угасает.");
            c5.AddDialog("c5_skibidi2", "Рассказчик", "Теперь этот звук будет преследовать вас в кошмарах.");
            c5.AddDialog("c5_skibidi3", "Рассказчик", "Вы набираете припасы и уходите из магазина...");

            c5.AddDialog("c5_stint", "Рассказчик", "Вы смотрите интервью Стинта. Его харизма заряжает вас энергией.");
            c5.AddDialog("c5_stint2", "Рассказчик", "Вы чувствуете прилив уверенности.");
            c5.AddDialog("c5_stint3", "Рассказчик", "Вы набираете припасы и уходите из магазина...");

            c5.AddDialog("c5_doc", "Рассказчик", "Вы смотрите документальный фильм о древних цивилизациях.");
            c5.AddDialog("c5_doc2", "Рассказчик", "Ваши знания пополняются. Кто знает, пригодится ли это?");
            c5.AddDialog("c5_doc3", "Рассказчик", "Вы набираете припасы и уходите из магазина...");

            c5.AddDialog("c5_break", "Рассказчик", "Вы с силой бьете по телевизору. Он разлетается на куски.");
            c5.AddDialog("c5_break2", "Рассказчик", "Тишина. Наконец-то благословенная тишина.");
            c5.AddDialog("c5_break3", "Рассказчик", "Вы набираете припасы и уходите из магазина...");

            var c6 = new Chapter("chapter6", "ГЛАВА 6: НАПАДЕНИЕ");
            c6.AddDialog("c6_d1", "Рассказчик", "Ночь опускается на пустыню. Освещение магазина начинает мигать как во ФНАФе.");
            c6.AddDialog("c6_d2", "Рассказчик", "Из темноты появляются длинные черные фигуры. Они выглядят как смесь Титанов и Слендермена.");
            c6.AddDialog("c6_d3", "Игрок", "Почему у одного из них лицо Джима Керри?!");

            var c6_choices = new List<Choice>
            {
                new Choice("[1] Спрятаться", (pl) => {
                    pl.GetAttr("Рассудок").Add(1);
                    pl.GetAttr("Интеллект").Add(1);
                    pl.GetEnding("Neutral").Add(1);
                }, nextStepId: "c6_hide"),
                new Choice("[2] Атаковать", (pl) => {
                    pl.GetAttr("Сила").Add(1);
                    pl.GetEnding("Good").Add(1);
                }, nextStepId: "c6_battle_start")
            };

            if (p.HasRobot)
            {
                c6_choices.Add(new Choice("[3] Попробовать договориться (робот)", (pl) => {
                    pl.GetEnding("Good").Add(1);
                }, nextStepId: "c6_robot_start"));
            }

            c6.AddChoice("c6_choice", c6_choices);

            c6.AddDialog("c6_hide", "Рассказчик", "Вы прячетесь за холодильником с ПСЫЖем. Существа проходят мимо, не заметив вас.");
            c6.AddDialog("c6_hide2", "Рассказчик", "Вы выжили и продолжаете путь...");

            c6.AddDialog("c6_robot_start", "Робо-Навальный", "Есть идея. Мы можем отвлечь существ мемами.");

            var c6_robot_choices = new List<Choice>
            {
                new Choice("[1] Включить котиков", (pl) => {
                    pl.GetEnding("Good").Add(1);
                }, nextStepId: "c6_robot_cats"),
                new Choice("[2] Включить брейнроты", (pl) => {
                    pl.GetEnding("Bad").Add(1);
                }, nextStepId: "c6_robot_brainrot")
            };
            c6.AddChoice("c6_robot_choice", c6_robot_choices);

            c6.AddDialog("c6_robot_cats", "Рассказчик", "Вы включаете видео с милыми котиками. Существа умиляются и уходят.");
            c6.AddDialog("c6_robot_cats2", "Рассказчик", "Вы выжили и продолжаете путь...");

            c6.AddDialog("c6_robot_brainrot", "Рассказчик", "Вы включаете бессмысленные брейнроты. Существа раздраженно уходят.");
            c6.AddDialog("c6_robot_brainrot2", "Рассказчик", "Вы выжили и продолжаете путь...");

            c6.AddMiniGame("c6_battle_start", (pl) => {
                var battlePlayer = new BattlePlayer
                {
                    HP = 30 + pl.GetAttr("Сила").Value,
                    Strength = pl.GetAttr("Сила").Value,
                    Intelligence = pl.GetAttr("Интеллект").Value,
                    Sanity = pl.GetAttr("Рассудок").Value,
                    HasRobot = pl.HasRobot
                };

                var enemy = new Enemy("Brainrot Monster", 25, 4);
                var battle = new BattleSystem(battlePlayer, enemy);

                bool victory = battle.StartBattle();

                if (victory)
                {
                    Console.WriteLine("\nВы победили монстра!");
                    pl.GetAttr("Сила").Add(2);
                    pl.GetEnding("Good").Add(3);
                    Console.WriteLine("+2 Силы | +3 Good Ending");
                }
                else
                {
                    Console.WriteLine("\nВы проиграли сражение...");
                    pl.GetAttr("Рассудок").Sub(3);
                    pl.GetEnding("Bad").Add(5);
                    Console.WriteLine("-3 Рассудка | +5 Bad Ending");
                    Console.WriteLine("Игра окончена!");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                }
            });

            c6.AddDialog("c6_attack_end", "Рассказчик", "Вы выжили и продолжаете путь...");

            var c7 = new Chapter("chapter7", "ГЛАВА 7: ПУТЬ К АЭРОПОРТУ");
            c7.AddDialog("c7_d1", "Рассказчик", "На следующий день планшет активируется сам. На карте мигает точка с подписью: \"LAST HUMAN DLC\".");
            c7.AddDialog("c7_d2", "Рассказчик", "Путь лежит через разрушенный город, где билборды с лицом Моргенштерна все еще светятся в темноте.");
            c7.AddDialog("c7_d3", "Рассказчик", "Ваш рассудок ухудшается. Вы начинаете слышать голос Смешариков, читающих Ницше.");
            c7.AddDialog("c7_d4", "Игрок", "Это просто усталость... или я уже в эдите?");
            c7.AddDialog("c7_d5", "Рассказчик", "На стене разрушенного метро написано: \"ТОРТ ЭТО ЛОЖЬ\".");
            c7.AddDialog("c7_d6", "Рассказчик", "Может, оставите свой след в истории?");

            var c7_choices = new List<Choice>
            {
                new Choice("[1] Написать ЦОЙ ЖИВ", (pl) => { }, nextStepId: "c7_tsoy"),
                new Choice("[2] Написать мат", (pl) => { }, nextStepId: "c7_mat"),
                new Choice("[3] Нарисовать амогуса", (pl) => { }, nextStepId: "c7_amogus")
            };
            c7.AddChoice("c7_choice", c7_choices);

            c7.AddDialog("c7_tsoy", "Рассказчик", "Вы пишете 'ЦОЙ ЖИВ' на стене. Закончив, чувствуете странное умиротворение.");
            c7.AddDialog("c7_tsoy2", "Рассказчик", "Вы идёте к аэропорту...");

            c7.AddDialog("c7_mat", "Рассказчик", "Вы пишете трехэтажный мат. Становится немного легче на душе.");
            c7.AddDialog("c7_mat2", "Рассказчик", "Вы идёте к аэропорту...");

            c7.AddDialog("c7_amogus", "Рассказчик", "Вы рисуете красного амогуса. Кто-то явно оценит эту отсылку через тысячу лет.");
            c7.AddDialog("c7_amogus2", "Рассказчик", "Вы идёте к аэропорту...");

            var c8 = new Chapter("chapter8", "ГЛАВА 8: АЭРОПОРТ");
            c8.AddDialog("c8_d1", "Рассказчик", "Вы добираетесь до гигантского аэропорта. На башне управления огромными буквами написано: \"WE LIVE WE LOVE WE LIE\".");
            c8.AddDialog("c8_d2", "Игрок", "Самолет... пожалуйста, пусть это не отсылка на 9/11.");
            c8.AddDialog("c8_d3", "Рассказчик", "В одном из ангаров стоит старый лайнер с аниме-наклейками и надписью \"Evangelion\".");

            int strength = p.GetAttr("Сила").Value;
            int intellect = p.GetAttr("Интеллект").Value;

            if (strength >= 7)
            {
                c8.AddDialog("c8_fuel", "Рассказчик", "Вы вручную открываете резервуар под музыку Doom!");
                c8.AddDialog("c8_fuel2", "Рассказчик", "Топливо найдено! Ваша сила спасла ситуацию.");
            }
            else if (intellect >= 7)
            {
                c8.AddDialog("c8_fuel", "Рассказчик", "Вы находите топливо по старым схемам NASA и Reddit-гайдам!");
                c8.AddDialog("c8_fuel2", "Рассказчик", "Ваш интеллект помог разобраться в сложной системе.");
            }
            else if (p.HasRobot)
            {
                c8.AddDialog("c8_fuel", "Рассказчик", "Робо-Навальный убеждает дрона MrBeast помочь вам!");
                c8.AddDialog("c8_fuel2", "Робо-Навальный", "У меня 40 терабайт компромата, пришлось немного использовать...");
            }
            else
            {
                c8.AddDialog("c8_fuel", "Рассказчик", "Чудом вы находите немного топлива в старой бочке.");
                c8.AddDialog("c8_fuel2", "Рассказчик", "Этого должно хватить, чтобы взлететь.");
            }
            c8.AddDialog("c8_d4", "Рассказчик", "Самолет готов к взлету...");

            var endGood = new Chapter("ending_good", "ХОРОШАЯ КОНЦОВКА");
            endGood.AddDialog("eg1", "Рассказчик", "Самолет взлетает. Сквозь облака вы видите зеленый остров и гигантскую надпись \"WELCOME BACK\".");
            endGood.AddDialog("eg2", "Рассказчик", "Вас встречают выжившие: Гатс, CJ из GTA San Andreas, Курт Кобейн и человек в костюме Among Us.");
            endGood.AddDialog("eg3", "Рассказчик", "Кто-то включает музыку.");
            if (p.HasRobot) endGood.AddDialog("eg4", "Робо-Навальный", "maybe the real sigma was the friends we made along the way");
            endGood.AddDialog("eg5", "Рассказчик", "История только начинается...");
            endGood.AddEnding("eg_end");

            var endNeutral = new Chapter("ending_neutral", "НЕЙТРАЛЬНАЯ КОНЦОВКА");
            endNeutral.AddDialog("en1", "Рассказчик", "Вы находите убежище. Оно пустое. Только старый телевизор бесконечно показывает Shrek Forever After.");
            endNeutral.AddDialog("en2", "Рассказчик", "Вы живете один среди руин, питаясь консервами и скачанными мемами.");
            endNeutral.AddDialog("en3", "Рассказчик", "Иногда ночью вам кажется, что где-то рядом звучит SKIBIDI.");
            endNeutral.AddEnding("en_end");

            var endBad = new Chapter("ending_bad", "ПЛОХАЯ КОНЦОВКА");
            endBad.AddDialog("eb1", "Рассказчик", "Силы покидают вас. Песок засыпает следы, а небо становится красным как превью к creepypasta.");
            endBad.AddDialog("eb2", "Рассказчик", "Вы падаете на колени. Вдалеке медленно приближается Big Pibbles.");
            endBad.AddDialog("eb3", "Рассказчик", "Последнее, что вы слышите: \"bruh\".");
            endBad.AddEnding("eb_end");

            var endSecret = new Chapter("ending_secret", "СЕКРЕТНАЯ КОНЦОВКА");
            endSecret.AddDialog("es1", "Рассказчик", "Вы находите скрытый бункер NASA. На двери надпись: \"BACKROOMS\".");
            endSecret.AddDialog("es2", "Рассказчик", "Внутри — машина времени, собранная Тони Старком из металлолома.");
            endSecret.AddDialog("es3", "Рассказчик", "На экране появляется сообщение: \"RETURN TO 2020? Y/N\"");
            endSecret.AddDialog("es4", "Рассказчик", "Камера медленно приближается к вашему лицу под музыку \"Memory Reboot\".");
            endSecret.AddDialog("es5", "Рассказчик", "Вы нажимаете Y. Мир вокруг искажается...");
            endSecret.AddDialog("es6", "Рассказчик", "Вы просыпаетесь в своей комнате. За окном 2020 год.");
            endSecret.AddDialog("es7", "Рассказчик", "На столе — экзаменационные билеты. Но теперь вы знаете, что вас ждет впереди...");
            endSecret.AddDialog("es8", "Рассказчик", "КОНЕЦ? ИЛИ НАЧАЛО?");
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