//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        static void QuestMenu(Character player)
        {
            Console.Clear();
            Console.WriteLine("퀘스트");
            Console.WriteLine();
            WriteColoredConsole("1", ConsoleColor.Red);
            Console.WriteLine(". 마을을 위협하는 미니언 처치");
            WriteColoredConsole("2", ConsoleColor.Red);
            Console.WriteLine(". 장비 착용하기");
            WriteColoredConsole("3", ConsoleColor.Red);
            Console.WriteLine(". 더욱 더 강해지기");
            Console.WriteLine();
            WriteColoredConsole("0", ConsoleColor.Red);
            Console.WriteLine(". 나가기");
            Console.WriteLine();

            int input = CheckInput(0, 3);
            switch (input)
            {
                case 0:
                    MainMenu(player);
                    break;
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
            Console.WriteLine("500G\n");
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
            Console.WriteLine("300G\n");
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

            Console.WriteLine($"- 레벨 3 달성하기 ({player.level} / 3)\n");

            Console.WriteLine("- 보상 -");
            Console.WriteLine("1000G\n");
            CheckQuest(player, 2);

        }
        static void CheckQuest(Character player, int questNum)
        {
            int questRequired = 0;
            switch (questNum)
            {
                case 0:
                    questRequired = 5;
                    break;
                case 1:
                    questRequired = 1;
                    break;
                case 2:
                    questRequired = 3;
                    break;
            }

            if (player.questCleared[questNum] == true)      // 퀘스트를 이미 클리어 한 상태
            {
                WriteLineColoredConsole("\n이미 이 퀘스트를 클리어 하였습니다.", ConsoleColor.Red);
                Thread.Sleep(1000);
            }
            else
            {
                if (player.acceptQuest[questNum] == false)  // 퀘스트 수락 한 적 없는 상태
                {
                    WriteColoredConsole("1", ConsoleColor.Red);
                    Console.WriteLine(". 수락");
                    WriteColoredConsole("2", ConsoleColor.Red);
                    Console.WriteLine(". 거절");
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
                    if (player.questNumber[questNum] < questRequired)  //퀘스트 완료 조건 못채움
                    {
                        WriteColoredConsole("1", ConsoleColor.Red);
                        Console.WriteLine(". 확인");
                        int input = CheckInput(1, 1);
                        if (input != 1)
                        {
                            Console.WriteLine("Error in CheckQuest - else");
                            Thread.Sleep(2000);
                        }
                    }
                    else            // 퀘스트 완료 조건 채움
                    {
                        WriteColoredConsole("1", ConsoleColor.Red);
                        Console.WriteLine(". 보상 받기");
                        WriteColoredConsole("2", ConsoleColor.Red);
                        Console.WriteLine(". 돌아가기\n");

                        int input = CheckInput(1, 2);
                        switch (input)
                        {
                            case 1:         // 퀘스트 클리어 보상 받기         -> 추후 퀘스트 보상 정할때 고칠 필요 있음
                                player.acceptQuest[questNum] = false;
                                switch (questNum)
                                {
                                    case 0:
                                        player.gold += 500;
                                        break;
                                    case 1:
                                        player.gold += 300;
                                        break;
                                    case 2:
                                        player.gold += 1000;
                                        break;
                                }
                                    
                        
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
        }
    }
}
