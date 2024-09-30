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
            Console.WriteLine($"500 G 를 내어 휴식하면 체력을 회복할 수 있습니다. 보유 골드: {player.gold} G");
            Console.WriteLine($"포션을 사용하면 체력을 {potionHp} 회복할 수 있습니다. (남은 포션 : {potionList.Count} )");
            Console.WriteLine($"현재 체력: {player.health} / 100\n");

            Console.WriteLine("1. 휴식 하기");
            Console.WriteLine("2. 포션 사용하기");
            Console.WriteLine("0. 나가기");

            int input = CheckInput(0, 2);

            // 풀피인데 회복하려고 시도하는 경우
            if ((input == 1 || input == 2) && player.health >= 100)
            {
                WriteColoredConsole("체력이 충분합니다. 회복할 필요가 없습니다.", ConsoleColor.Red);
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
                        if (player.health >= 100) player.health = 100;
                        WriteColoredConsole("휴식을 완료했습니다.", ConsoleColor.Blue);
                        Thread.Sleep(1000);
                        Rest(player);
                    }
                    else
                    {
                        WriteColoredConsole("Gold 가 부족합니다.", ConsoleColor.Red);
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
                        WriteColoredConsole("포션이 부족합니다.", ConsoleColor.Red);
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
            if (player.health >= 100)
                player.health = 100;
            potionList.Remove(potionHp);
            WriteColoredConsole("회복을 완료했습니다.", ConsoleColor.Blue);
        }
    }
}
