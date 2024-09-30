using System.ComponentModel;
using System.ComponentModel.Design;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
using static TextRPG_project.Program;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;


namespace TextRPG_project
{
    internal partial class Program
    {
        static Character player = new Character();
        static List<Item> itemList = new List<Item>(6); // 아이템 리스트 초기화;
        static List<Monster> monsterList = new List<Monster>(3); // 몬스터 리스트 초기화;
        static int potionHp = 30;
        static List<int> potionList = new List<int>(3); // 포션 리스트 초기화;
        public enum DungeonDiff { 쉬운 = 5, 일반 = 11, 어려운 = 17 }
        static bool isDataLoaded = false;
        static string Start()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            if (LoadData(ref player, itemList, potionList))
            {
                isDataLoaded = true;
                return player.name;
            }

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
            Console.WriteLine("7. 데이터 저장");
            Console.WriteLine("0. 종료");

            int input = CheckInput(0, 7);

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
                    player.lasthp = player.health;
                    dungeon.EnterDungeon(player);
                    dungeon.DeadCount = 0;
                    break;
                case 6:
                    QuestMenu(player);
                    break;
                case 7:
                    SaveData(player, itemList, potionList);
                    Thread.Sleep(1000);
                    MainMenu(player);
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
            {
                case 0:
                    MainMenu(player);
                    break;
                default:
                    break;
            }
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
            if (!isDataLoaded)
            {
                class_type = SelectClass();

                player = new Character(name, class_type); // player class 초기화

                // 아이템 데이터 추가
                itemList.Add(new Item("수련자 갑옷", 1, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
                itemList.Add(new Item("무쇠 갑옷", 1, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000));
                itemList.Add(new Item("스파르타의 갑옷", 1, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
                itemList.Add(new Item("낡은 검", 2, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
                itemList.Add(new Item("청동 도끼", 2, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
                itemList.Add(new Item("스파르타의 창", 2, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2300));

                // 기본 포션 초기화
                for (int i = 0; i < 3; i++)
                    potionList.Add(potionHp);
            }
            // 몬스터 데이터 추가
            monsterList.Add(new Monster("미니언", 2, 15, 5));
            monsterList.Add(new Monster("공허충", 3, 10, 9));
            monsterList.Add(new Monster("대포미니언", 5, 25, 8));

            MainMenu(player); // 게임 시작
        }
    }
}
