﻿//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

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
    }
}