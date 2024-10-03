using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static TextRPG_project.Program;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class Skill
        {
            public string name;         // 스킬 이름
            public float damageMultiplier;  // 스킬의 공격력 배수
            public int needMP;          // 스킬 사용에 필요한 마나
            public string explaination; // 스킬 설명
            public bool randomTarget;   // 랜덤타겟인지 아닌지
            public int targetNumber;    // 맞는 마리 수

            public Skill(string  name, float damageMultiplier, int needMP, string explaination, bool randomTarget, int targetNumber)
            {
                this.name = name;
                this.damageMultiplier = damageMultiplier;
                this.needMP = needMP;
                this.explaination = explaination;
                this.randomTarget = randomTarget;
                this.targetNumber = targetNumber;
            }

            public void ShowSkill()
            {
                Console.WriteLine($"{name} - MP {needMP}");
                Console.WriteLine(explaination);
            }

            public void UseSkill(Character player, Dungeon dungeon)
            {
                if(player.mp < needMP)
                {
                    Console.WriteLine("MP가 부족합니다.");
                    Thread.Sleep(1000);
                    dungeon.SkillMenu(player);
                }
                else if (randomTarget) // 타겟을 랜덤으로 정하는 스킬
                {
                    if(dungeon.monsters.Count - dungeon.DeadCount < targetNumber)   // 살아있는 몬스터가 타겟 수 보다 적을경우
                    {
                        Console.WriteLine("남은 몬스터가 부족합니다.");
                        Thread.Sleep(1000);
                        dungeon.SkillMenu(player);
                    }

                    else    
                    {
                        Random rand = new Random();
                        List<Monster> liveMonsters = new List<Monster>();       // 살아있는 monster의 List
                        liveMonsters = dungeon.monsters.ToList();
                        for (int i = liveMonsters.Count - 1; i >= 0; i--)
                        {
                            if (liveMonsters[i].IsDead()) liveMonsters.Remove(liveMonsters[i]);     // 죽은 monster를 List에서 제외
                        }
                        while (liveMonsters.Count > targetNumber)
                        {
                            liveMonsters.Remove(liveMonsters[rand.Next(0, liveMonsters.Count)]);    // targetNumber 만큼 남을때까지 리스트에서 랜덤으로 제외
                        }
                        player.mp -= needMP;
                        dungeon.AttackResult(player, (int)(player.attack * damageMultiplier), liveMonsters, false);
                    }

                    

                }
                else    // 타겟을 직접 정하는 스킬
                {
                    ConsoleUI.ShowBattleInfo(player, dungeon);
                    WriteColoredConsole("0", ConsoleColor.Red);
                    Console.WriteLine(". 취소");
                    dungeon.AttackInput(player, false, this);
                }
            }
        }
    }
}
