//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

using Newtonsoft.Json;
using System.Numerics;
using System.Xml;

namespace TextRPG_project
{
    internal partial class Program
    {
        // 사용자 입력 유효성 검사
        static int CheckInput(int min, int max)
        {
            Console.WriteLine("\n원하시는 행동을 선택해주세요.");
            Console.Write(">>");
            int result;
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max)
                {
                    return result;
                }
            }
            WriteColoredConsole("잘못된 입력입니다.", ConsoleColor.Red);
            Thread.Sleep(1000);
            ClearPreviousLines(4);
            return CheckInput(min, max);
        }

        // 콘솔 커서 위치 조정
        static void ClearPreviousLines(int numberOfLines)
        {
            int currentLineCursor = Console.CursorTop;
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.SetCursorPosition(0, currentLineCursor - 1 - i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, currentLineCursor - numberOfLines);
        }

        // 문자열 길이 계산
        static int GetStringLenghth(string input)
        {
            int len = 0;
            foreach (char c in input)
            {
                // 한글일 경우 너비를 2로, 영어일 경우 1로 계산
                // if (c >= 'ㄱ' && c <= 'ㅎ' || c >= '가' && c <= '힣')
                if (c >= 0x1100 && c <= 0x11FF || c >= 0xAC00 && c <= 0xD7A3)
                    len += 2;
                else
                    len += 1;
            }
            return len;
        }

        // 콘솔 색상 변경하여 출력
        static void WriteColoredConsole(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        // 플레이 데이터 저장
        static void SaveData(Character player, List<Item> item, List<int> potion)
        {
            // 저장할 json 파일의 이름 지정
            string playerDataFileName = "PlayerData.json";
            string itemDataFileName = "ItemData.json";
            string potionDataFileName = "PotionData.json";

            // 데이터 경로 저장 (중단점 이용하여 경로 확인하기)
            string folderName = "../../../PlayData";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources);
            path = Path.Combine(path, folderName);

            // /(데이터 저장되는 폴더 + 파일 이름)으로 경로명 설정
            string playerDataPath = Path.Combine(path, playerDataFileName);
            string itemDataPath = Path.Combine(path, itemDataFileName);
            string potionDataPath = Path.Combine(path, potionDataFileName);

            // data를 Json 으로 Serialize (직렬화)
            string playerJson = JsonConvert.SerializeObject(player, Newtonsoft.Json.Formatting.Indented);
            string itemJson = JsonConvert.SerializeObject(item, Newtonsoft.Json.Formatting.Indented);
            string potionJson = JsonConvert.SerializeObject(potion, Newtonsoft.Json.Formatting.Indented);

            // 새 파일 생성 후 파일에 내용 쓰고 닫음. 대상 파일이 이미 존재하는 경우 덮어씀.
            File.WriteAllText(playerDataPath, playerJson);
            File.WriteAllText(itemDataPath, itemJson);
            File.WriteAllText(potionDataPath, potionJson);

            WriteColoredConsole("저장이 완료되었습니다", ConsoleColor.Green);
        }
        // 플레이 데이터 로드
        static bool LoadData(ref Character player, List<Item> item, List<int> potion)
        {
            // 불러올 json 파일의 이름 지정
            string playerDataFileName = "PlayerData.json";
            string itemDataFileName = "ItemData.json";
            string potionDataFileName = "PotionData.json";

            // 데이터 경로 저장 (중단점 이용하여 경로 확인하기)
            string folderName = "../../../PlayData";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources);
            path = Path.Combine(path, folderName);

            // /(데이터 저장되는 폴더 + 파일 이름)으로 경로명 설정
            string playerDataPath = Path.Combine(path, playerDataFileName);
            string itemDataPath = Path.Combine(path, itemDataFileName);
            string potionDataPath = Path.Combine(path, potionDataFileName);

            if (File.Exists(playerDataPath) && File.Exists(itemDataPath) && File.Exists(potionDataPath)) // json 파일이 존재
            {
                // 파일 내용(json형태) 불러오고 역직렬화
                // json -> data
                string playerJson = File.ReadAllText(playerDataPath);
                player = JsonConvert.DeserializeObject<Character>(playerJson);
                WriteColoredConsole("플레이어 데이터를 불러왔습니다.", ConsoleColor.Green);
                WriteColoredConsole("인벤토리 데이터를 불러왔습니다.", ConsoleColor.Green);
                WriteColoredConsole("퀘스트 데이터를 불러왔습니다.", ConsoleColor.Green);

                string itemJson = File.ReadAllText(itemDataPath);
                item.Clear(); // 기존 아이템 삭제
                item.AddRange(JsonConvert.DeserializeObject<List<Item>>(itemJson)); // json에 저장된 내용으로 새로 쓰기
                WriteColoredConsole("아이템 데이터를 불러왔습니다.", ConsoleColor.Green);

                string potionJson = File.ReadAllText(potionDataPath);
                potion.Clear(); // 기존 포션 삭제
                potion.AddRange(JsonConvert.DeserializeObject<List<int>>(potionJson)); // json에 저장된 내용으로 새로 쓰기
                WriteColoredConsole("포션 데이터를 불러왔습니다.", ConsoleColor.Green);

                return true;
            }
            else if (!File.Exists(playerDataPath))
            {
                Console.WriteLine("저장된 플레이어 데이터가 없습니다.");
                return false;
            }
            else if (!File.Exists(itemDataPath))
            {
                Console.WriteLine("저장된 아이템 데이터가 없습니다.");
                return false;
            }
            else if (!File.Exists(potionDataPath))
            {
                Console.WriteLine("저장된 포션 데이터가 없습니다.");
                return false;
            }
            else // json 파일 없음
            {
                Console.WriteLine("저장된 데이터가 없습니다.");
                return false;
            }
        }
    }
}