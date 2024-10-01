//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        static void Rest(Character player)
        {
            Console.Clear();
            Console.WriteLine("체력 회복하기");
            WriteColoredConsole("500", ConsoleColor.Red);
            Console.Write($" G 를 내어 휴식하면 체력을 40 회복할 수 있습니다. 보유 골드: ");
            WriteColoredConsole($"{player.gold}", ConsoleColor.Red);
            Console.WriteLine(" G");
            Console.WriteLine($"포션을 사용하면 체력을 {potionHp} 회복할 수 있습니다. (남은 포션 : {potionList.Count} )");
            Console.Write($"현재 체력: ");
            WriteColoredConsole($"{player.health}", ConsoleColor.Red);
            Console.WriteLine($" / {player.maxHP}\n");

            WriteColoredConsole("1", ConsoleColor.Red);
            Console.WriteLine(". 휴식 하기");
            WriteColoredConsole("2", ConsoleColor.Red);
            Console.WriteLine(". 포션 사용하기");
            Console.WriteLine();
            WriteColoredConsole("0", ConsoleColor.Red);
            Console.WriteLine(". 나가기");

            int input = CheckInput(0, 2);

            // 풀피인데 회복하려고 시도하는 경우
            if ((input == 1 || input == 2) && player.health >= player.maxHP)
            {
                WriteLineColoredConsole("체력이 충분합니다. 회복할 필요가 없습니다.", ConsoleColor.Red);
                Thread.Sleep(1000);
                Rest(player);
            }

            switch (input)
            {
                case 0: // 나가기
                    MainMenu(player);
                    break;
                case 1: // 휴식하기
                    if (player.gold >= 500)
                    {
                        player.gold -= 500;
                        player.health += 40;
                        if (player.health >= player.maxHP) player.health = player.maxHP;
                        WriteLineColoredConsole("휴식을 완료했습니다.", ConsoleColor.Blue);
                        Thread.Sleep(1000);
                        Rest(player);
                    }
                    else
                    {
                        WriteLineColoredConsole("Gold 가 부족합니다.", ConsoleColor.Red);
                        Thread.Sleep(1000);
                        Rest(player);
                    }
                    break;
                case 2: // 포션 사용하기
                    if (potionList.Count > 0)
                    {
                        UsePotion(player);
                        Thread.Sleep(1000);
                        Rest(player);
                    }
                    else
                    {
                        WriteLineColoredConsole("포션이 부족합니다.", ConsoleColor.Red);
                        Thread.Sleep(1000);
                        Rest(player);
                    }
                    break;
            }
        }
        static void GetPotion()
        {
            // 10%의 확률로 포션을 얻음
            Random rand = new Random();
            int randNum = rand.Next(0, 100);
            if (randNum < 10)
            {
                potionList.Add(potionHp);
            }
        }
        static void UsePotion(Character player)
        {
            player.health += potionHp;
            if (player.health >= player.maxHP)
                player.health = player.maxHP;
            potionList.Remove(potionHp);
            WriteLineColoredConsole("회복을 완료했습니다.", ConsoleColor.Blue);
        }
    }
}
