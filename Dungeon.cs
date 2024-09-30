//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class Dungeon
        {
            public List<Monster> monsters;
            Random rand;
            public int DeadCount = 0;
            public bool Clear = false;

            public Dungeon()        // 던전 내의 몬스터들 초기화
            {
                monsters = new List<Monster>();
                rand = new Random();
                int numMonster = rand.Next(1, 5); // 몬스터 1~4마리중 랜덤
                for (int i = 0; i < numMonster; i++)
                {
                    int monsterType = rand.Next(0, 3); // 몬스터 타입 1,2,3중 랜덤
                    Monster monster = new Monster(monsterList[monsterType]);
                    monsters.Add(monster);
                }
            }
            public void PrintMonsters()     // 던전 내의 몬스터 정보 출력
            {
                for (int i = 0; i < monsters.Count; i++)
                {
<<<<<<< Updated upstream
                    Console.WriteLine($"Lv. {monsters[i].level}\t{monsters[i].name}  \tHP {monsters[i].health}");
=======
                    if (!monsters[i].IsDead())
                        Console.WriteLine($"Lv. {monsters[i].level}\t{monsters[i].name}  \tHP {monsters[i].health}");
                    else
                        WriteColoredConsole($"Lv. {monsters[i].level}\t{monsters[i].name}  \tDead", ConsoleColor.DarkGray);
>>>>>>> Stashed changes
                }
                Console.WriteLine();
                Console.WriteLine();
            }

<<<<<<< Updated upstream
            public void EnemyAttack(Character player)
=======
            public void PrintMonstersWithNumber()     // 던전 내의 몬스터 정보 출력
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (!monsters[i].IsDead())
                        Console.WriteLine($"{i + 1} Lv. {monsters[i].level}\t{monsters[i].name}  \tHP {monsters[i].health}");
                    else
                        WriteColoredConsole($"{i + 1} Lv. {monsters[i].level}\t{monsters[i].name}  \tDead", ConsoleColor.DarkGray);
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            public void EnemyPhase(Character player)
>>>>>>> Stashed changes
            {
                int input;
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].health > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Battle!!\n");
                        Console.WriteLine($"Lv. {monsters[i].level} {monsters[i].name}의 공격!");
                        Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {monsters[i].attack}]");
                        Console.WriteLine();
                        Console.WriteLine($"Lv. {player.level} {player.name}");
                        Console.WriteLine($"HP {player.health} -> {MathF.Max(0, player.health - monsters[i].attack)}");
                        player.health -= monsters[i].attack;

                        Console.WriteLine();
                        Console.WriteLine("0. 다음");
                        input = CheckInput(0, 0);
                    }
                    //if (monsters[i].health <= 0)
                    //{
                    //    DeadCount++;
                    //    monsters[i].dead = true;

                    //    if (DeadCount == monsters.Count)
                    //    {
                    //        Clear = true;
                    //    }

                    //}
                    if(player.health <= 0)
                    {
                        player.health = 0;
                        GameOver(player);
                        break;
                    }
                }
<<<<<<< Updated upstream
                 Fight(player, this);
=======
                 if(player.health > 0) EnterDungeon(player);
            }

            public void EnterDungeon(Character player)
            {

                Console.Clear();
                Console.WriteLine("Battle!");
                Console.WriteLine();
                PrintMonsters();
                player.PrintSimpleStats();
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine();
                int input = CheckInput(1, 1);

                switch (input)
                {
                    case 1:
                        AttackPhase(player);
                        break;
                    //case 2:
                    //    Skill(player, dungeon);
                    //    break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.- EnterDungeon() 함수 내");
                        Thread.Sleep(1000);
                        break;
                }

            }

            void AttackPhase(Character player)
            {
                Console.Clear();
                Console.WriteLine("Battle!");
                Console.WriteLine();
                PrintMonstersWithNumber();
                player.PrintSimpleStats();
                Console.WriteLine();
                Console.WriteLine("0. 취소");
                Console.WriteLine();
                int input;
                int attackDamage;
                Random rand = new Random();
                Monster monster;
                while (true)
                {
                    Console.WriteLine("대상을 선택해주세요.");
                    Console.Write(">> ");
                    string temp = Console.ReadLine();
                    if (int.TryParse(temp, out input))
                    {
                        if (input >= 1 && input <= monsters.Count)
                        {
                            if (monsters[input - 1].IsDead())
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                            else
                            {
                                monster = monsters[input - 1];
                                int errorRange = (player.attack + 9) / 10; // 공격의 오차 범위, 올림처리 위해 (공격력+9) / 10을 함
                                attackDamage = rand.Next(player.attack - errorRange, player.attack + errorRange + 1);
                                break;
                            }
                        }
                        else if (input == 0) EnterDungeon(player);    // 0 입력 시 이전으로 돌아감
                        else Console.WriteLine("잘못된 입력입니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    Thread.Sleep(1000);
                    ClearPreviousLines(3);

                }

                AttackResult(player, attackDamage, monster);

            }

            void AttackResult(Character player, int attackDamage, Monster monster)
            {
                Console.Clear();
                Random rand = new Random();
                bool isCrit = false;
                int critical = rand.Next(1, 21);
                if (critical < 4) isCrit = true;             // 치명타 확률계산
                bool isEvade = false;
                int evasion = rand.Next(1, 11);
                if (evasion == 1) isEvade = true;

                Console.WriteLine("Battle!");
                Console.WriteLine();
                Console.WriteLine($"{player.name}의 공격!");
                if (isEvade)
                {
                    Console.WriteLine($"Lv.{monster.level} {monster.name}을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                    attackDamage = 0;
                }
                else
                {
                    if (isCrit) attackDamage = (int)(attackDamage * 1.6f);
                    Console.Write($"Lv.{monster.level} {monster.name}을(를) 맞췄습니다. [데미지 : {attackDamage}]");
                    if (isCrit) Console.Write(" - 치명타 공격!!");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{monster.level} {monster.name}");
                    Console.Write($"HP {monster.health} -> ");
                    if (monster.health - attackDamage <= 0) Console.WriteLine("Dead");
                    else Console.WriteLine($"{monster.health - attackDamage}");
                }
                monster.health -= attackDamage;          // 공격 데미지 처리
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                int input = CheckInput(0, 0);

                bool isAllDead = true;
                foreach (Monster mons in monsters)
                {
                    if (!mons.IsDead()) isAllDead = false;
                }
                if (isAllDead) GameClear(player, this);
                else EnemyPhase(player);
>>>>>>> Stashed changes
            }
        }
    }
}
