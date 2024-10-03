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
                    if (!monsters[i].IsDead())
                    {
                        WriteColoredConsole($"{i+1}.", ConsoleColor.Red);
                        Console.WriteLine($" Lv. {monsters[i].level}\t{monsters[i].name}  \tHP {monsters[i].health}");
                    }
                    else
                        WriteLineColoredConsole($"{i + 1}  Lv. {monsters[i].level}\t{monsters[i].name}  \tDead", ConsoleColor.DarkGray);
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
                        int damage = (int)(monsters[i].attack * 10 / (10 + player.defence));
                        player.health -= damage;
                        ConsoleUI.ShowEnemyPhase(player, monsters, damage, i);  // 몬스터가 공격하는 콘솔 출력
                        input = CheckInput(0, 0);
                    }

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
                ConsoleUI.ShowEnterDungeon(player, this);   // 던전 메뉴 콘솔에 출력

                int input = CheckInput(1, 3);

                switch (input)
                {
                    case 1:
                        AttackPhase(player);
                        break;
                    case 2:
                        SkillMenu(player);
                        break;
                    case 3:
                        heal.UsePotion(player);
                        Thread.Sleep(1000);
                        EnterDungeon(player);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.- EnterDungeon() 함수 내");
                        Thread.Sleep(1000);
                        break;
                }

            }

            void AttackPhase(Character player)
            {
                ConsoleUI.ShowBattleInfo(player, this);
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 취소");
                
                AttackInput(player, true, skillList[0]);
            }

            public void SkillMenu(Character player)
            {
                ConsoleUI.ShowSelectSkill(player, this);   // 스킬 선택하는 콘솔 출력

                int input = CheckInput(0, 2);
                if (input == 0)
                {
                    EnterDungeon(player);
                }
                else
                {
                    skillList[input].UseSkill(player, this);
                }

            }


            public void AttackResult(Character player, int damage, List<Monster> attackedmonsters, bool isAttack)
            {
                Random rand = new Random();

                Console.Clear();
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
                    int attackDamage;
                    if (evasion == 1 && isAttack)
                    {
                        isEvade = true;
                        attackDamage = 0;
                    }
                    else
                    {
                        int errorRange = (damage + 9) / 10;   // 공격의 오차 범위, 올림처리 위해 (공격력+9) / 10을 함
                        attackDamage = rand.Next(damage - errorRange, damage + errorRange + 1);
                        if (isCrit) attackDamage = (int)(attackDamage * 1.6f);
                        monster.health -= attackDamage;          // 공격 데미지 처리

                        if (monster.health <= 0)
                        {
                            DeadCount++;
                            player.experience += monster.level;
                            player.gold += monster.level * 100;
                        }
                    }
                    ConsoleUI.ShowAttackResult(player, monster, i, isEvade, isCrit, attackDamage);
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

            public void AttackInput(Character player, bool isAttack, Skill skill) // attack일 시 1, skill-1일시 2
            {
                int input = CheckInput(0, monsters.Count);
                if (input == 0)
                {
                    if (isAttack) EnterDungeon(player);
                    else SkillMenu(player);
                }
                else if (input >= 1 && input <= monsters.Count)
                {
                    if (monsters[input - 1].IsDead())
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        ClearPreviousLines(4);
                        AttackInput(player, isAttack, skill);
                    }
                    else
                    {
                        Monster monster = monsters[input - 1];
                        int attackDamage = (int)(player.attack * skill.damageMultiplier);
                        if (!isAttack) player.mp -= skill.needMP;
                        List<Monster> attackedmonsters = new List<Monster>
                        {
                            monster
                        };
                        AttackResult(player, attackDamage, attackedmonsters, isAttack);
                    }
                }
            }
        }
    }
}