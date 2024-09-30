﻿//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class Dungeon
        {
            List<Monster> monsters;
            Random rand;

            public Dungeon()        // 던전 내의 몬스터들 초기화
            {
                monsters = new List<Monster>();
                rand = new Random();
                int numMonster = rand.Next(1, 5); // 몬스터 1~4마리중 랜덤
                for (int i = 0; i < numMonster; i++)
                {
                    int monsterType = rand.Next(0, 3); // 몬스터 타입 1,2,3중 랜덤
                    monsters.Add(monsterList[monsterType]);
                }
            }
            public int DeadCount = 0;
            public bool Clear = false;
            public void PrintMonsters()     // 던전 내의 몬스터 정보 출력
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.WriteLine($"Lv. {monsters[i].level}\t{monsters[i].name}  \tHP {monsters[i].health}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            public void EnemyAttack(Character player)
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
                        Console.WriteLine($"HP {player.health} -> {player.health - monsters[i].attack}");
                        player.health -= monsters[i].attack;

                        Console.WriteLine();
                        Console.WriteLine("0. 다음");
                        input = CheckInput(0, 0);
                    }
                    if (monsters[i].health <= 0)
                    {
                        DeadCount++;
                        monsters[i].dead = true;

                        if (DeadCount == monsters.Count)
                        {
                            Clear = true;
                        }

                    }
                }
                 Fight(player, this);
            }
        }
    }
}
