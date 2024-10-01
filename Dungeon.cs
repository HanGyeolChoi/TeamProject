//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

using static TextRPG_project.Program;

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
                    int monsterType = rand.Next(0, monsterList.Count); // 몬스터 타입 중 랜덤
                    Monster monster = new Monster(monsterList[monsterType]);
                    monsters.Add(monster);
                }
            }
            public void PrintMonsters()     // 던전 내의 몬스터 정보 출력
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (!monsters[i].IsDead())
                        Console.WriteLine($"Lv. {monsters[i].level}\t{monsters[i].name}  \tHP {monsters[i].health}");
                    else
                        WriteLineColoredConsole($"Lv. {monsters[i].level}\t{monsters[i].name}  \tDead", ConsoleColor.DarkGray);
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            public void PrintMonstersWithNumber()     // 던전 내의 몬스터 정보 출력
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (!monsters[i].IsDead()) {
                        WriteColoredConsole($"{i+1}.", ConsoleColor.Red);
                        Console.WriteLine($" Lv. {monsters[i].level}\t{monsters[i].name}  \tHP {monsters[i].health}");
                    }
                    else
                        WriteLineColoredConsole($"{i + 1} Lv. {monsters[i].level}\t{monsters[i].name}  \tDead", ConsoleColor.DarkGray);
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            public void EnemyPhase(Character player)
            {
                int input;
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].health > 0)
                    {
                        Console.Clear();
                        int damage = (int)(monsters[i].attack * 10 / (10 + player.defence));
                        Console.WriteLine("Battle!!\n");
                        Console.WriteLine($"Lv. {monsters[i].level} {monsters[i].name}의 공격!");
                        Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                        Console.WriteLine();
                        Console.WriteLine($"Lv. {player.level} {player.name}");
                        Console.WriteLine($"HP {player.health} -> {MathF.Max(0, player.health - damage)}");
                        player.health -= damage;

                        Console.WriteLine();
                        WriteColoredConsole("0", ConsoleColor.Red);
                        Console.WriteLine(". 다음");
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
                    if (player.health <= 0)
                    {
                        player.health = 0;
                        GameOver(player);
                        break;
                    }
                }
                if (player.health > 0) EnterDungeon(player);
            }

            public void EnterDungeon(Character player)
            {

                Console.Clear();
                Console.WriteLine("Battle!");
                Console.WriteLine();
                PrintMonsters();
                player.PrintSimpleStats();
                Console.WriteLine();
                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 공격");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 스킬");
                WriteColoredConsole("3", ConsoleColor.Red);
                Console.WriteLine(". 포션 사용");
                Console.WriteLine();
                int input = CheckInput(1, 2);

                switch (input)
                {
                    case 1:
                        AttackPhase(player);
                        break;
                    case 2:
                        Skill(player);
                        break;
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
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 취소");
                
                AttackInput(player, 1);
            }

            void Skill(Character player)
            {
                Console.Clear();
                Console.WriteLine("Battle!");
                Console.WriteLine();
                PrintMonsters();
                player.PrintSimpleStats();
                Console.WriteLine();
                
                Random rand = new Random();

                //if (player.class_type == 1)                     // 전사일 경우
                //{
                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 알파 스트라이크 - MP 10");
                Console.WriteLine("   공격력 * 2 로 하나의 적을 공격합니다.");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 더블 스트라이크 - MP 15");
                Console.WriteLine("   공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 취소");
                //}

                //else if (player.class_type == 2)                // 도적일 경우
                //{
                //    Console.WriteLine("1.\t알파 스트라이크 - MP 10");
                //    Console.WriteLine("\t공격력 * 2 로 하나의 적을 공격합니다.");
                //    Console.WriteLine("2.\t더블 스트라이크 - MP 15");
                //    Console.WriteLine("\t공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
                //    Console.WriteLine("0.\t취소");
                //}

                int input = CheckInput(0, 2);
                if (input == 0)
                {
                    EnterDungeon(player);
                }
                else if (input == 1)
                {
                    if (player.mp < 10)
                    {
                        Console.WriteLine("MP가 부족합니다.");
                        Thread.Sleep(1000);
                        Skill(player);
                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Battle!");
                        Console.WriteLine();
                        PrintMonstersWithNumber();
                        player.PrintSimpleStats();
                        Console.WriteLine();
                        WriteColoredConsole("0", ConsoleColor.Red);
                        Console.WriteLine(". 취소");
                        AttackInput(player, 2);
                    }

                }
                else if (input == 2)
                {
                    if (player.mp < 15)
                    {
                        Console.WriteLine("MP가 부족합니다.");
                        Thread.Sleep(1000);
                        Skill(player);
                    }

                    else
                    {

                        if (monsters.Count - DeadCount > 1)
                        {
                            List<Monster> liveMonsters = new List<Monster>();       // 살아있는 monster의 List
                            liveMonsters = monsters.ToList();
                            for (int i = liveMonsters.Count - 1; i >= 0; i--)
                            {
                                if (liveMonsters[i].IsDead()) liveMonsters.Remove(liveMonsters[i]);     // 죽은 monster를 List에서 제외시킴
                            }
                            while (liveMonsters.Count > 2)
                            {
                                liveMonsters.Remove(liveMonsters[rand.Next(0, liveMonsters.Count)]);    // 2마리가 남을때까지 제외시킴
                            }
                            player.mp -= 15;
                            AttackResult(player, (int)(player.attack * 1.5f), liveMonsters, 2);
                        }

                        else
                        {
                            Console.WriteLine("남은 몬스터가 부족합니다.");
                            Thread.Sleep(1000);
                            Skill(player);
                        }
                    }
                }
            }


            void AttackResult(Character player, int damage, List<Monster> attackedmonsters, int attackOrSkill)
            {
                Console.Clear();
                Random rand = new Random();


                Console.WriteLine("Battle!");
                Console.WriteLine();
                Console.WriteLine($"{player.name}의 공격!");
                for (int i = 0; i < attackedmonsters.Count; i++)
                {
                    Monster monster = attackedmonsters[i];
                    bool isCrit = false;
                    int critical = rand.Next(1, 21);
                    if (critical < 4) isCrit = true;             // 치명타 확률계산
                    bool isEvade = false;
                    int evasion = rand.Next(1, 11);
                    if (evasion == 1 && attackOrSkill == 1) isEvade = true;
                    int attackDamage;
                    if (isEvade)
                    {
                        Console.WriteLine($"Lv.{monster.level} {monster.name}을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                        attackDamage = 0;
                    }
                    else
                    {

                        int errorRange = (damage + 9) / 10;   // 공격의 오차 범위, 올림처리 위해 (공격력+9) / 10을 함
                        attackDamage = rand.Next(damage - errorRange, damage + errorRange + 1);
                        if (isCrit) attackDamage = (int)(attackDamage * 1.6f);
                        Console.Write($"Lv.{monster.level} {monster.name}을(를) 맞췄습니다. [데미지 : {attackDamage}]");
                        if (isCrit) Console.Write(" - 치명타 공격!!");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{monster.level} {monster.name}");
                        Console.Write($"HP {monster.health} -> ");
                        if (monster.health - attackDamage <= 0)
                        {
                            Console.WriteLine("Dead");
                            DeadCount++;
                        }
                        else Console.WriteLine($"{monster.health - attackDamage}");
                    }
                    monster.health -= attackDamage;          // 공격 데미지 처리
                    Console.WriteLine();
                }

                Console.WriteLine();
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 다음");
                int input = CheckInput(0, 0);

                if (DeadCount == monsters.Count)
                {
                    // 퀘스트1(미니언 처치) 수락한 상태고 완료된 상태가 아니면
                    if (player.acceptQuest[0] && !player.questCleared[0])
                    {
                        int deadMinion = 0;
                        foreach (Monster mon in monsters)
                        {
                            if (mon.level == 2)
                                deadMinion++;
                        }
                        player.questNumber[0] += deadMinion; // 처치한 미니언의 수 더하기
                    }
                    GameClear(player, this);
                }
                else EnemyPhase(player);
            }

            void AttackInput(Character player, int attackOrSkill) // attack일 시 1, skill-1일시 2
            {
                int input = CheckInput(0, monsters.Count);
                if (input == 0)
                {
                    if (attackOrSkill == 1) EnterDungeon(player);
                    else Skill(player);
                }
                else if (input >= 1 && input <= monsters.Count)
                {
                    if (monsters[input - 1].IsDead())
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        ClearPreviousLines(4);
                        AttackInput(player, attackOrSkill);
                    }
                    else
                    {
                        Monster monster = monsters[input - 1];
                        int attackDamage = player.attack * attackOrSkill;
                        if (attackOrSkill == 2) player.mp -= 10;
                        List<Monster> attackedmonsters = new List<Monster>
                        {
                            monster
                        };
                        AttackResult(player, attackDamage, attackedmonsters, attackOrSkill);
                    }
                }
            }
        }
    }
}