using System.ComponentModel;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;


namespace TextRPG_project
{
    internal partial class Program
    {
        static List<Item> itemList = new List<Item>(6); // 아이템 리스트 초기화;
        static List<Monster> monsterList = new List<Monster>(3); // 몬스터 리스트 초기화;

        static int potionHp = 30;
        static List<int> potionList = new List<int>(3); // 포션 리스트 초기화;
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
            Console.WriteLine("4. 체력 회복하기");
            Console.WriteLine("5. 전투 시작");
            Console.WriteLine("6. 퀘스트");
            Console.WriteLine("0. 종료");

            int input = CheckInput(0, 6);

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
                    Rest(player);   // 휴식하기
                    break;
                case 5:
                    Dungeon dungeon = new Dungeon();    // 전투 시작
<<<<<<< Updated upstream
                    Fight(player, dungeon);
=======
                    player.lasthp = player.health;
                    dungeon.EnterDungeon(player);
                    dungeon.DeadCount = 0;
>>>>>>> Stashed changes
                    break;
                case 6:
                    QuestMenu(player);
                    break;
                case 0:
                    break;
                default:
                    WriteColoredConsole("\n잘못된 입력입니다.", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    MainMenu(player);
                    break;

            }

        }
<<<<<<< Updated upstream

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
        static void Fight(Character player, Dungeon dungeon)
        {
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine();
            dungeon.PrintMonsters();
            player.PrintSimpleStats();
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            int input = CheckInput(1, 1);

            if (input == 1)            // 몬스터 공격 선택
            {
                Console.WriteLine("공격 창 출력");
                Thread.Sleep(2000);                     // 임시 공격 창 출력
                //Attack(player, dungeon);
                dungeon.EnemyAttack(player);

            }
            //else if (input == 2)      // 스킬이 추가되면 쓸 곳
            //{
            //      Skill(player, dungeon);
            //}

            //else
            //{
            //    Console.WriteLine("잘못된 입력입니다.- Fight() 함수 내");
            //    Thread.Sleep(1000);
            //}
        }

        static void QuestMenu(Character player)
        {
            Console.Clear();
            Console.WriteLine("퀘스트");
            Console.WriteLine();
            Console.WriteLine("1. 마을을 위협하는 미니언 처치");
            Console.WriteLine("2. 장비 착용하기");
            Console.WriteLine("3. 더욱 더 강해지기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = CheckInput(0, 3);
            switch(input)
=======
        static void GameOver(Character player)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            WriteColoredConsole("\nYou Lose", ConsoleColor.Red);
            Console.WriteLine($"\nLV{player.level} {player.name}");
            Console.WriteLine($"Hp {player.lasthp} -> {player.health}");
            //Console.WriteLine("\n0. 처음부터 다시 시작하기");
            Console.WriteLine("\n0. 게임 종료하기");
            int input = CheckInput(0, 0);

            switch (input)
            {
                //case 0:
                //    Start();
                //    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Error in GameOver");
                    Thread.Sleep(1000);
                    break;
            }
        }

        static void GameClear(Character player, Dungeon dungeon)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            WriteColoredConsole("\nYou Win", ConsoleColor.Green);
            Console.WriteLine($"던전에서 몬스터를 {dungeon.monsters.Count} 마리 잡았습니다.");
            Console.WriteLine($"\nLV{player.level} {player.name}");
            Console.WriteLine($"Hp {player.lasthp} -> {player.health}");
            GetPotion();    // 10%의 확률로 포션 획득
            Console.WriteLine("\n0. 돌아가기");
            int input = CheckInput(0, 0);

            switch (input)
>>>>>>> Stashed changes
            {
                case 0:
                    MainMenu(player);
                    break;
<<<<<<< Updated upstream
                case 1:
                    QuestFirst(player);
                    break;
                case 2:
                    QuestSecond(player);
                    break;
                case 3:
                    QuestThird(player);
                    break;
                default:
                    Console.WriteLine("Error in QuestMenu");
                    break;
            }            
        }

        static void QuestFirst(Character player)
        {
            Console.Clear();
            Console.WriteLine("퀘스트\n");
            
            Console.WriteLine("마을을 위협하는 미니언 처치\n");
            
            Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?");
            Console.WriteLine("마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!");
            Console.WriteLine("모험가인 자네가 좀 처치해주게!\n");
            
            Console.WriteLine($"- 미니언 5마리 처치 ({player.questNumber[0]} / 5)\n");
            
            Console.WriteLine("- 보상 -");
            //Console.WriteLine("대충 아이템 이름");
            Console.WriteLine("100G\n");
            CheckQuest(player, 0);
        }

        static void QuestSecond(Character player)
        {
            Console.Clear();
            Console.WriteLine("퀘스트\n");

            Console.WriteLine("장비 착용하기\n");

            //Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?");
            //Console.WriteLine("마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!");
            //Console.WriteLine("모험가인 자네가 좀 처치해주게!\n");

            Console.WriteLine($"- 아무 장비나 착용하기 ({player.questNumber[1]} / 1)\n");

            Console.WriteLine("- 보상 -");
            //Console.WriteLine("대충 아이템 이름");
            Console.WriteLine("100G\n");
            CheckQuest(player, 1);
        }

        static void QuestThird(Character player)
        {
            Console.Clear();
            Console.WriteLine("퀘스트\n");

            Console.WriteLine("더욱 더 강해지기\n");

            //Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?");
            //Console.WriteLine("마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!");
            //Console.WriteLine("모험가인 자네가 좀 처치해주게!\n");

            Console.WriteLine($"- 아무 장비나 착용하기 ({player.questNumber[2]} / 1)\n");

            Console.WriteLine("- 보상 -");
            //Console.WriteLine("대충 아이템 이름");
            Console.WriteLine("100G\n");
            CheckQuest(player, 2);

        }
        static void CheckQuest(Character player, int questNum)
        {
            int questRequired;
            if (questNum == 0) questRequired = 5;
            else questRequired = 1;

            if (player.questCleared[questNum] == true)      // 퀘스트를 이미 클리어 한 상태
            {
                WriteColoredConsole("\n이미 이 퀘스트를 클리어 하였습니다.", ConsoleColor.Red);
                Thread.Sleep(1000);
            }
            else
            {
                if (player.acceptQuest[questNum] == false)  // 퀘스트 수락 한 적 없는 상태
                {
                    Console.WriteLine("1. 수락");
                    Console.WriteLine("2. 거절");
                    int input = CheckInput(1, 2);
                    switch (input)
                    {
                        case 1:
                            Console.WriteLine("수락했습니다.");
                            player.acceptQuest[questNum] = true;
                            Thread.Sleep(800);
                            break;
                        case 2:
                            Console.WriteLine("거절했습니다.");
                            Thread.Sleep(800);
                            break;
                        default:
                            Console.WriteLine("Error in CheckQuest - if");
                            Thread.Sleep(2000);
                            break;
                    }
                }
                else    // 퀘스트 이미 수락한 상태
                {
                    if (player.questNumber[1] < questRequired)  //퀘스트 완료 조건 못채움
                    {
                        Console.WriteLine("1. 확인");
                        int input = CheckInput(1, 1);
                        if (input != 1)
                        {
                            Console.WriteLine("Error in CheckQuest - else");
                            Thread.Sleep(2000);
                        }
                    }
                    else            // 퀘스트 완료 조건 채움
                    {
                        Console.WriteLine("1. 보상 받기");
                        Console.WriteLine("2. 돌아가기\n");

                        int input = CheckInput(1, 2);
                        switch (input)
                        {
                            case 1:         // 퀘스트 클리어 보상 받기         -> 추후 퀘스트 보상 정할때 고칠 필요 있음
                                player.acceptQuest[questNum] = false;
                                player.gold += 100;
                                //Item item = new Item(이름, 타입, 수치, 가격);
                                //player.items.Add(item);
                                player.questCleared[questNum] = true;
                                break;
                            case 2:         //  넘어감
                                break;
                            default:        //  1,2 외의 다른 인풋(에러)
                                Console.WriteLine($"Error in CheckQuest - else");
                                Thread.Sleep(2000);
                                break;
                        }
                    }
                }
            }
            QuestMenu(player);
=======
                default:
                    break;
            }
>>>>>>> Stashed changes
        }
        static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("게임 오버");
            Console.WriteLine("체력이 0 이하로 떨어졌습니다.");

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

            // 아이템 데이터 추가
            itemList.Add(new Item("수련자 갑옷", 1, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
            itemList.Add(new Item("무쇠 갑옷", 1, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000));
            itemList.Add(new Item("스파르타의 갑옷", 1, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
            itemList.Add(new Item("낡은 검", 2, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
            itemList.Add(new Item("청동 도끼", 2, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
            itemList.Add(new Item("스파르타의 창", 2, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2300));

            // 몬스터 데이터 추가
            monsterList.Add(new Monster("미니언", 2, 15, 5));
            monsterList.Add(new Monster("공허충", 3, 10, 9));
            monsterList.Add(new Monster("대포미니언", 5, 25, 8));

            // 기본 포션 초기화
            for (int i = 0; i < 3; i++)
                potionList.Add(potionHp);

            MainMenu(player); // 게임 시작
        }
    }
}
