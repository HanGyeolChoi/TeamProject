//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        static int potionHp = 30;
        static List<int> potionList = new List<int>(3); // 포션 리스트 초기화;

        public class Heal
        {
            public void Rest(Character player)
            {
                ConsoleUI.ShowHealMenu(player);

                int input = CheckInput(0, 2);

                // 풀피인데 회복하려고 시도하는 경우
                if ((input == 1 || input == 2) && player.health >= player.maxHP && player.mp >= player.maxMP)
                {
                    WriteLineColoredConsole("체력과 마나가 충분합니다. 회복할 필요가 없습니다.", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    MainMenu(player);
                }
                else
                {
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
                                player.mp += 10;
                                if (player.health >= player.maxHP) player.health = player.maxHP;
                                if (player.mp >= player.maxMP) player.mp = player.maxMP;
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
            }
            public bool GetPotion()
            {
                // 10%의 확률로 포션을 얻음
                Random rand = new Random();
                int randNum = rand.Next(0, 100);
                if (randNum < 10)
                {
                    potionList.Add(potionHp);
                    return true;
                }
                return false;
            }
            public void UsePotion(Character player)
            {
                player.health += potionHp;
                if (player.health >= player.maxHP)
                    player.health = player.maxHP;
                potionList.Remove(potionHp);
                WriteLineColoredConsole("회복을 완료했습니다.", ConsoleColor.Blue);
            }
        }
    }
}
