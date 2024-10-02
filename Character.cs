//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class Character
        {
            public int level;
            public int lastlevel;
            public string name;
            public int class_type;     //전사일 경우 1, 도적일 경우 2
            public int attack;
            public int defence;
            public int maxHP;
            public int health;
            public int lasthp;
            public int maxMP;
            public int mp;
            public int gold;
            public int lastgold;
            public int itemAttack;     //아이템으로 올라간 총 공격력
            public int itemDefence;    //아이템으로 올라간 총 방어력
            public int experience;      //경험치
            public int maxexp;
            public int lastexp;
            public List<Item> items;   // 인벤토리의 아이템
            public Item? equippedArmor;
            public Item? equippedWeapon;
            public bool[] acceptQuest;  //퀘스트 수락 여부
            public int[] questNumber;   //각 퀘스트 진행상황
            public bool[] questCleared; //퀘스트 클리어 여부
            public Character()
            {

            }
            public Character(string _name, int class_num)
            {
                level = 1;
                lastlevel = 1;
                name = _name;
                class_type = class_num;
                if (class_num == 1)
                {
                    attack = 10;
                    defence = 5;
                    maxHP = 100;
                }
                else
                {
                    attack = 13;
                    defence = 2;
                    maxHP = 85;
                }
                health = maxHP;
                lasthp = maxHP;
                maxMP = 50;
                mp = maxMP;
                gold = 1500;
                lastgold = 1500;
                
                items = new List<Item>();
                experience = 0;
                maxexp = FullExp();
                lastexp = 0;
                //itemAttack = 0;
                //itemDefence = 0;      //자동으로 0으로 초기화됨
                equippedArmor = null;
                equippedWeapon = null;
                acceptQuest = new bool[3];
                questNumber = new int[3];
                questCleared = new bool[3];
            }


            public void Buy(Item item)
            {
                item.sold = true;
                items.Add(item);
                gold -= item.price;
            }

            public void Sell(Item item)
            {
                if (item.equip == true) Unequip(item);
                item.sold = false;
                items.Remove(item);
                gold += item.price * 85 / 100;
            }


            public void ShowStats()
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                Console.Write($"Level\t: ");
                WriteLineColoredConsole($"{level}", ConsoleColor.Red);
                Console.WriteLine($"이  름\t: {name}");
                Console.Write($"직  업\t: ");
                if (class_type == 1) Console.WriteLine("전사");
                else Console.WriteLine("도적");
                Console.Write($"공격력\t: ");
                WriteColoredConsole($"{attack}", ConsoleColor.Red);
                if (itemAttack > 0) WriteColoredConsole($" (+{itemAttack})", ConsoleColor.Green);
                Console.Write($"\n방어력\t: ");
                WriteColoredConsole($"{defence}", ConsoleColor.Red);
                if (itemDefence > 0) WriteColoredConsole($" (+{itemDefence})", ConsoleColor.Green);
                Console.Write($"\n체  력\t: ");
                WriteColoredConsole($"{health}", ConsoleColor.Red);
                Console.WriteLine($" / {maxHP}");
                Console.Write($"M   P\t: ");
                WriteColoredConsole($"{mp}", ConsoleColor.Red);
                Console.WriteLine($" / {maxMP}");
                Console.Write($"Gold\t: ");
                WriteLineColoredConsole($"{gold}", ConsoleColor.Red);
                Console.WriteLine($"경험치\t: {experience} / {maxexp}");

                Console.WriteLine("\n0. 나가기");
                int input = CheckInput(0, 0);

                if (input == 0)
                {
                    MainMenu(this);
                }
            }


            public void ShowInventory()
            {
                Console.Clear();
                Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.Write("- ");
                    if (items[i].equip) Console.Write("[E] ");
                    items[i].ShowItem();
                    Console.WriteLine();
                }
                WriteColoredConsole("\n1", ConsoleColor.Red);
                Console.WriteLine(". 장착 관리");
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 나가기");

                int input = CheckInput(0, 1);


                if (input == 1)
                {
                    ItemManagement();
                }
                else if (input == 0)
                {
                    MainMenu(this);
                }
            }


            public void ItemManagement()
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.Write("-");
                    WriteColoredConsole($" {i + 1}", ConsoleColor.Red);
                    Console.Write(". ");
                    if (items[i].equip) Console.Write("[E] ");
                    items[i].ShowItem();
                    Console.WriteLine();
                }
                Console.WriteLine();
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 나가기");


                int input = CheckInput(0, items.Count);

                if (input == 0)
                {
                    ShowInventory();
                }
                else
                {
                    if (items[input - 1].equip) Unequip(items[input - 1]);
                    else Equip(items[input - 1]);
                    ItemManagement();
                }

            }


            private void Equip(Item item)
            {

                if (item.itemType == 1)
                {
                    if (equippedArmor != null) Unequip(equippedArmor);
                    defence += item.stat;
                    itemDefence += item.stat;
                    equippedArmor = item;
                }
                else
                {
                    if (equippedWeapon != null) Unequip(equippedWeapon);
                    attack += item.stat;
                    itemAttack += item.stat;
                    equippedWeapon = item;
                }
                item.equip = true;
                questNumber[1] = 1;
            }

            private void Unequip(Item item)
            {
                if (item.itemType == 1)
                {
                    defence -= item.stat;
                    itemDefence -= item.stat;
                    equippedArmor = null;
                }
                else
                {
                    attack -= item.stat;
                    itemAttack -= item.stat;
                    equippedWeapon = null;
                }
                item.equip = false;
                if (equippedArmor == null && equippedWeapon == null) questNumber[1] = 0;
            }

            public void PrintSimpleStats()
            {
                string? job = null;
                switch (class_type)
                {
                    case 1:
                        job = "전사";
                        break;
                    case 2:
                        job = "도적";
                        break;
                }
                Console.WriteLine("[내정보]");

                Console.Write("Lv.");
                WriteColoredConsole($"{level}", ConsoleColor.Red);
                Console.WriteLine($"\t이름:{name}  직업: {job}");

                Console.Write($"HP ");
                WriteColoredConsole($"{health}", ConsoleColor.Red);
                Console.WriteLine($"/{maxHP}");
                Console.Write($"MP ");
                WriteColoredConsole($"{mp}", ConsoleColor.Red);
                Console.WriteLine($"/{maxMP}");
                Console.WriteLine();
            }
            public int FullExp()
            {
                switch (level)
                {
                    case 1:
                        return 10;
                    case 2:
                        return 35;
                    case 3:
                        return 65;
                    case 4:
                        return 100;
                    default:
                        return 0;
                }
            }
            public void LevelUp()
            {

                if (experience >= maxexp)
                {
                    WriteLineColoredConsole("레벨업!", ConsoleColor.Blue);
                    level++;
                    experience -= maxexp;
                    maxexp = FullExp();
                    defence += 1;
                    attack += 1;
                    Console.WriteLine($"\nLV.{lastlevel} {name} -> LV.{level} {name}");
                }
                else
                {
                    Console.WriteLine($"\nLv.{level} {name}");
                }
            }
        }

    }
}
