//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        static void Store(List<Item> items, Character player)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G \n");

            Console.WriteLine("[아이템 목록]");
            foreach (Item item in items)
            {
                Console.Write("- ");
                item.ShowItem();
                if (!item.sold) Console.WriteLine($" | {item.price} G");
                else Console.WriteLine(" | 구매 완료");
            }

            WriteColoredConsole("\n1", ConsoleColor.Red);
            Console.WriteLine(". 아이템 구매");
            WriteColoredConsole("2", ConsoleColor.Red);
            Console.WriteLine(". 아이템 판매");
            WriteColoredConsole("0", ConsoleColor.Red);
            Console.WriteLine(". 나가기");

            int input = CheckInput(0, 2);

            if (input == 1)
            {
                BuyMenu(items, player);
            }
            else if (input == 2)
            {
                SellMenu(items, player);
            }
            else
            {
                MainMenu(player);
            }
        }

        static void BuyMenu(List<Item> items, Character player)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G \n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("- ");
                WriteColoredConsole($"{i + 1}", ConsoleColor.Red);
                Console.Write(". ");
                items[i].ShowItem();
                if (!items[i].sold) Console.WriteLine($" | {items[i].price} G");
                else Console.WriteLine(" | 구매 완료");
            }
            WriteColoredConsole("\n0", ConsoleColor.Red);
            Console.WriteLine(". 나가기");

            int input = CheckInput(0, items.Count);

            if (input == 0)
            {
                Store(items, player);
            }
            else
            {
                if (items[input - 1].sold)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Thread.Sleep(1000);
                }
                else
                {
                    if (player.gold >= items[input - 1].price)
                    {
                        player.Buy(items[input - 1]);
                        Console.WriteLine("구매를 완료했습니다.");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        Thread.Sleep(1000);
                    }
                }
                BuyMenu(items, player);
            }
        }


        static void SellMenu(List<Item> items, Character player)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요하지 않은 아이템을 판매할 수 있습니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G \n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < player.items.Count; i++)
            {
                Console.Write("- ");
                WriteColoredConsole($"{i + 1}", ConsoleColor.Red);
                Console.Write(". ");
                player.items[i].ShowItem();
                Console.WriteLine($" | 판매 가격: {player.items[i].price * 85 / 100} G");
            }
            WriteColoredConsole("0", ConsoleColor.Red);
            Console.WriteLine(". 나가기");

            int input = CheckInput(0, player.items.Count);

            if (input == 0)
            {
                Store(items, player);
            }
            else
            {
                if (player.items[input - 1].sold)
                {
                    player.Sell(player.items[input - 1]);
                    Console.WriteLine("판매가 완료되었습니다.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("구매하지 않은 아이템입니다.");
                    Thread.Sleep(1000);
                }
                SellMenu(items, player);
            }
        }
    }
}
