//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

using System.Text.Json.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class Item
        {
            public string name;
            public int itemType;    // 타입 1 = 방어구, 타입 2 = 무기
            public int stat;       // 아이템으로 올라가는 수치
            public string description;
            public int price;
            public bool sold;
            public bool equip;
            public Item(string name, int type, int stat, string description,int price)
            {
                this.name = name;
                itemType = type;
                this.stat = stat;
                this.price = price;
                this.description = description;
                sold = false;
                equip = false;
            }

            public void ShowItem()
            {
                // 출력 너비 포맷 설정
                int nameWidth = 17;
                int statWidth = 3;
                int descriptionWidth = 50;

                // 전체 문자열 길이 계산
                int nameLen = GetStringLenghth(name);
                int statLen = stat.ToString().Length;
                int descripLen = GetStringLenghth(description);

                // 너비에 맞게 공백을 패딩
                string nameStr = name + new string(' ', Math.Max(nameWidth - nameLen, 0));
                string statStr = stat + new string(' ', Math.Max(statWidth - statLen, 0));
                string descripStr = description + new string(' ', Math.Max(descriptionWidth - descripLen, 0));

                if (itemType == 1)
                    Console.Write($"{nameStr}| 방어력 +{statStr} | {descripStr}");
                else
                    Console.Write($"{nameStr}| 공격력 +{statStr} | {descripStr}");
            }
        }
    }
}
