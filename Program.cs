using System.ComponentModel;
using System.Numerics;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;


namespace TextRPG_project
{
    internal partial class Program
    {
        static List<Item> itemList = new List<Item>(); // 아이템 리스트 초기화;
        public enum DungeonDiff { 쉬운 = 5, 일반 = 11, 어려운 = 17 }
        static string Start()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 선택해주세요.\n");

            string name = Console.ReadLine();

            Console.WriteLine($"\n설정하신 이름은 {name}입니다.\n");
            Console.WriteLine("1. 저장\n2. 취소");

            int input = CheckInput(1, 2);

            if (input == 1)
            {
                return name;
            }
            else if (input == 2)
            {
                return Start();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
                return Start();
            }
        }

        static int SelectClass()
        {

            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 직업을 선택해주세요.\n");
            Console.WriteLine("1. 전사\n2. 도적\n");
            int class_type = CheckInput(1, 2);

            if (class_type == 1) Console.WriteLine("\n고른 직업은 전사입니다.\n");
            else if (class_type == 2) Console.WriteLine("\n고른 직업은 도적 입니다.\n");

            Console.WriteLine("1. 저장\n2. 취소");

            int input = CheckInput(1, 2);

            if (input == 1)
            {
                return class_type;
            }
            else
            {
                return SelectClass();
            }

        }
        static void MainMenu(Character player)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전에 들어가기 전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 휴식하기");
            Console.WriteLine("5. 던전 입장");
            Console.WriteLine("0. 종료");

            int input = CheckInput(0, 5);

            switch (input)
            {
                case 1:
                    player.ShowStats();//상태창 보기
                    break;
                case 2:
                    player.ShowInventory();//인벤토리 보기
                    break;
                case 3:
                    Store(itemList, player);//상점 보기
                    break;
                case 4:
                    Rest(player);
                    break;
                case 5:
                    DungeonMenu(player);
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    MainMenu(player);
                    break;

            }

        }

        static void Rest(Character player)
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. 보유 골드: {player.gold} G");
            Console.WriteLine($"현재 체력: {player.health} / 100\n");

            Console.WriteLine("1. 휴식 하기");
            Console.WriteLine("0. 나가기");

            int input = CheckInput(0, 1);

            if (input == 1)
            {
                if (player.gold >= 500)
                {
                    player.gold -= 500;
                    player.health += 40;
                    if (player.health >= 100) player.health = 100;
                    Console.WriteLine("휴식을 완료했습니다.");
                    Thread.Sleep(1000);
                    MainMenu(player);
                }
                else
                {
                    Console.WriteLine("Gold 가 부족합니다.");
                    Thread.Sleep(1000);
                    MainMenu(player);
                }
            }
            else
            {
                MainMenu(player);
            }
        }


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

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

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
                Console.Write($"- {i + 1} ");
                items[i].ShowItem();
                if (!items[i].sold) Console.WriteLine($" | {items[i].price} G");
                else Console.WriteLine(" | 구매 완료");
            }

            Console.WriteLine("\n0. 나가기");

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
                    if (player.gold > items[input - 1].price)
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
                Console.Write($"- {i + 1} ");
                player.items[i].ShowItem();
                Console.WriteLine($" | 판매 가격: {player.items[i].price * 85 / 100} G");
            }

            Console.WriteLine("0. 나가기");

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


        static void DungeonMenu(Character player)
        {
            Console.Clear();
            int diff1 = 5;
            int diff2 = 11;
            int diff3 = 17;
            Console.WriteLine("던전 입장");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine($"현재 체력\t: {player.health}");
            Console.WriteLine($"현재 방어력\t: {player.defence}\n");

            Console.WriteLine($"1. 쉬운 던전\t\t| 방어력 {diff1} 이상 권장");
            Console.WriteLine($"2. 일반 던전\t\t| 방어력 {diff2} 이상 권장");
            Console.WriteLine($"3. 어려운 던전\t\t| 방어력 {diff3} 이상 권장");
            Console.WriteLine("0. 나가기");

            int input = CheckInput(0, 3);
            switch (input)
            {
                case 1:
                    EnterDungeon(player, diff1);
                    break;
                case 2:
                    EnterDungeon(player, diff2);
                    break;
                case 3:
                    EnterDungeon(player, diff3);
                    break;
                case 0:
                    MainMenu(player);
                    break;
                default:
                    Console.WriteLine("Error 발생");
                    break;

            }


        }

        static void EnterDungeon(Character player, int diff)
        {
            int[] dungeonGold = { 1000, 1700, 2500 };
            Random rand = new Random();
            int decreasedHealth;
            int result;
            if (player.defence >= diff)
            {
                decreasedHealth = rand.Next(20 - player.defence + diff, 35 - player.defence + diff + 1);    // 깎이는 체력 수치
                result = rand.Next(100 + player.attack, 100 + player.attack * 2 + 1);   // 보상 배율 랜덤 설정
            }
            else
            {
                int win = rand.Next(0, 10);
                if (win < 4)
                {
                    result = 0;
                    if (player.health > 60) decreasedHealth = player.health / 2;
                    else decreasedHealth = 30;
                }
                else
                {
                    decreasedHealth = rand.Next(20 - player.defence + diff, 35 - player.defence + diff + 1);    // 깎이는 체력 수치
                    result = rand.Next(100 + player.attack, 100 + player.attack * 2 + 1);   // 보상 배율 랜덤 설정
                }
            }

            Console.Clear();
            if (player.health <= decreasedHealth)
            {
                GameOver();
            }
            else
            {
                if (result == 0) // 던전 클리어 실패 시
                {
                    Console.Clear();
                    Console.WriteLine("던전 클리어 실패");
                    Console.WriteLine($"체력이 {decreasedHealth}만큼 줄어듭니다.");

                    Console.WriteLine("\n[탐험 결과]");
                    Console.WriteLine($"체력 {player.health} -> {player.health - decreasedHealth}");

                    player.health -= decreasedHealth;
                }
                else            // 던전 클리어 시
                {
                    Console.WriteLine("던전 클리어");
                    Console.WriteLine("축하합니다");
                    Console.WriteLine($"{(DungeonDiff)diff} 던전을 클리어 하였습니다.");

                    Console.WriteLine("\n[탐험 결과]");
                    Console.WriteLine($"체력 {player.health} -> {player.health - decreasedHealth}");
                    Console.WriteLine($"Gold {player.gold} -> {player.gold + dungeonGold[diff / 5 - 1] * result / 100}");

                    player.health -= decreasedHealth;
                    player.gold += dungeonGold[diff / 5 - 1] * result / 100;
                    player.numberDungeonClear++;

                    if (player.numberDungeonClear >= player.level)
                    {
                        player.level++;
                        player.numberDungeonClear = 0;
                        player.attack += player.level % 2; // 2레벨마다 1씩 오르게 설정
                        player.defence += 1;

                        Console.WriteLine("레벨업!");
                        if (player.level % 2 == 1) Console.WriteLine("공격력이 1 증가했습니다.");
                        Console.WriteLine("방어력이 1 증가했습니다.\n");
                    }
                }

                Console.Write("\n돌아가려면 아무 키나 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                DungeonMenu(player);
            }
        }

        static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("게임 오버");
            Console.WriteLine("체력이 0 이하로 떨어졌습니다.");

        }

        static int CheckInput(int min, int max)
        {
            Console.WriteLine("\n원하시는 행동을 선택해주세요.");
            Console.Write(">>");
            int result;
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max)
                {
                    return result;
                }
            }
            Console.WriteLine("잘못된 입력입니다.");
            Thread.Sleep(1000);
            ClearPreviousLines(4);
            return CheckInput(min, max);
        }

        static void ClearPreviousLines(int numberOfLines)
        {
            int currentLineCursor = Console.CursorTop;
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.SetCursorPosition(0, currentLineCursor - 1 - i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, currentLineCursor - numberOfLines);
        }

        //static void SaveCharacter(Character character, string filePath)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(Character));
        //    using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        serializer.Serialize(stream, character);
        //    }
        //}

        //static Character LoadCharacter(string filePath)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(Character));
        //    using (FileStream stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        return (Character)serializer.Deserialize(stream);
        //    }
        //}


        static void Main(string[] args)
        {
            string name;
            int class_type;

            name = Start();
            class_type = SelectClass();

            Character player = new Character(name, class_type); // player class 초기화
            itemList.Add(new Item("수련자 갑옷\t\t", 1, 5, 1000));
            itemList.Add(new Item("무쇠 갑옷\t\t", 1, 9, 2000));
            itemList.Add(new Item("스파르타의 갑옷 \t", 1, 15, 3500));
            itemList.Add(new Item("낡은 검\t\t", 2, 2, 600));
            itemList.Add(new Item("청동 도끼\t\t", 2, 5, 1500));
            itemList.Add(new Item("스파르타의 창 \t", 2, 7, 2300));
            MainMenu(player); // 게임 시작
        }
    }
}
