//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

using static System.Net.Mime.MediaTypeNames;
using static TextRPG_project.Program;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class UI
        {
            private static UI _instance;
            private UI() { }

            // 프로퍼티로 접근하도록 설정
            public static UI UIInstance
            {
                get
                {
                    if (_instance == null)  // 한번만 생성되도록 제한
                    {
                        _instance = new UI();
                    }
                    return _instance;
                }
                // 설정은 필요없으므로 setter 생략
            }
            public void ShowSelectLoadData()
            {
                Console.WriteLine("저장된 데이터를 불러오시겠습니까?");
                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 데이터 불러오기");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 새로 시작하기");
            }
            public void ShowSelectSaveData()
            {
                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 저장");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 취소");
            }
            public void ShowSelectClass()
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 전사");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 도적\n");
            }
            public void ShowMainMenu()
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전에 들어가기 전 활동을 할 수 있습니다.\n");

                WriteColoredConsole("1. ", ConsoleColor.Red);
                Console.WriteLine("상태 보기");
                WriteColoredConsole("2. ", ConsoleColor.Red);
                Console.WriteLine("인벤토리");
                WriteColoredConsole("3. ", ConsoleColor.Red);
                Console.WriteLine("상점");
                WriteColoredConsole("4. ", ConsoleColor.Red);
                Console.WriteLine("체력 회복하기");
                WriteColoredConsole("5. ", ConsoleColor.Red);
                Console.WriteLine("전투 시작");
                WriteColoredConsole("6. ", ConsoleColor.Red);
                Console.WriteLine("퀘스트");
                WriteColoredConsole("7. ", ConsoleColor.Red);
                Console.WriteLine("데이터 저장");
                WriteColoredConsole("0. ", ConsoleColor.Red);
                Console.WriteLine("종료");
            }
            public void ShowGameOver(Character player)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                WriteLineColoredConsole("\nYou Lose", ConsoleColor.Red);
                Console.WriteLine($"\nLV{player.level} {player.name}");
                Console.WriteLine($"Hp {player.lasthp} -> {player.health}");
                Console.WriteLine("\n0. 게임 종료하기");
            }
            public void ShowGameClear(Character player, Dungeon dungeon, bool potionFlag)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                WriteLineColoredConsole("\nYou Win", ConsoleColor.Green);
                Console.WriteLine($"던전에서 몬스터를 {dungeon.monsters.Count} 마리 잡았습니다.");
                Console.WriteLine($"Hp {player.lasthp} -> {player.health}");
                Console.Write($"남은 MP {player.mp - 10} -> ");
                Console.WriteLine($"{player.mp}");

                Console.WriteLine("\n획득 아이템");
                Console.WriteLine($"Gold {player.lastgold} -> {player.gold}");
                if (potionFlag)
                    WriteLineColoredConsole("포션을 획득했습니다!", ConsoleColor.Blue);
                Console.WriteLine("\n0. 돌아가기");

            }

            public void ShowBattleInfo(Character player, Dungeon dungeon)
            {
                Console.Clear();
                Console.WriteLine("Battle!");
                Console.WriteLine();
                dungeon.PrintMonstersWithNumber();
                player.PrintSimpleStats();
                Console.WriteLine();
            }
            public void ShowEnterDungeon(Character player, Dungeon dungeon)
            {
                ShowBattleInfo(player, dungeon);
                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 공격");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 스킬");
                WriteColoredConsole("3", ConsoleColor.Red);
                Console.WriteLine(". 포션 사용");
                Console.WriteLine();
            }
            public void ShowEnemyPhase(Character player, List<Monster> monsters, int damage, int idx)
            {
                Console.Clear();
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"Lv. {monsters[idx].level} {monsters[idx].name}의 공격!");
                Console.Write($"{player.name} 을(를) 맞췄습니다. [데미지 : ");
                WriteColoredConsole($"{damage}", ConsoleColor.Red);
                Console.WriteLine("]");
                Console.WriteLine();
                Console.WriteLine($"Lv. {player.level} {player.name}");
                Console.WriteLine($"HP {player.health + damage} -> {MathF.Max(0, player.health)}");
                Console.WriteLine();
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 다음");
            }

            public void ShowSelectSkill(Character player, Dungeon dungeon)
            {
                ConsoleUI.ShowBattleInfo(player, dungeon);
                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 알파 스트라이크 - MP 10");
                Console.WriteLine("   공격력 * 2 로 하나의 적을 공격합니다.");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 더블 스트라이크 - MP 15");
                Console.WriteLine("   공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 취소");
            }
            public void ShowAttackResult(Character player, Monster monster, int idx, bool isEvade, bool isCritic, int damage)
            {
                if (isEvade)    // 몬스터가 피했으면
                {
                    Console.WriteLine($"Lv.{monster.level} {monster.name}을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                    return;
                }
                Console.Write($"Lv.{monster.level} {monster.name}을(를) 맞췄습니다. [데미지 : ");
                WriteColoredConsole($"{damage}", ConsoleColor.Red);
                Console.Write("]");
                if (isCritic)   // 치명타 공격이면
                {
                    Console.Write(" - 치명타 공격!!");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Lv.{monster.level} {monster.name}");
                Console.Write($"HP {monster.health + damage} -> "); // 공격 받기 이전의 체력 출력
                if (monster.health <= 0)    // 몬스터 죽은 상태면 Dead 출력
                {
                    WriteLineColoredConsole("Dead", ConsoleColor.DarkGray);
                }
                else    // 몬스터 살아있으면 남은 체력 출력
                {
                    Console.WriteLine($"{monster.health}");
                }
                Console.WriteLine();
            }
            public void ShowHealMenu(Character player)
            {
                Console.Clear();
                Console.WriteLine("체력 회복하기");
                WriteColoredConsole("500", ConsoleColor.Red);
                Console.Write($" G 를 내어 휴식하면 체력을 40, 마나를 10 회복할 수 있습니다. 보유 골드: ");
                WriteColoredConsole($"{player.gold}", ConsoleColor.Red);
                Console.WriteLine(" G");
                Console.WriteLine($"포션을 사용하면 체력을 {potionHp} 회복할 수 있습니다. (남은 포션 : {potionList.Count} )");
                Console.Write($"현재 체력: ");
                WriteColoredConsole($"{player.health}", ConsoleColor.Red);
                Console.WriteLine($" / {player.maxHP}\n");
                Console.Write($"현재 마나: ");
                WriteColoredConsole($"{player.mp}", ConsoleColor.Red);
                Console.WriteLine($" / {player.maxMP}\n");

                WriteColoredConsole("1", ConsoleColor.Red);
                Console.WriteLine(". 휴식 하기");
                WriteColoredConsole("2", ConsoleColor.Red);
                Console.WriteLine(". 포션 사용하기");
                Console.WriteLine();
                WriteColoredConsole("0", ConsoleColor.Red);
                Console.WriteLine(". 나가기");
            }
        }
    }

}
