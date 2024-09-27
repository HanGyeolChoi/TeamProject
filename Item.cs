//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class Item
        {
            private string name;
            public int itemType;    // 타입 1 = 방어구, 타입 2 = 무기
            public int stat;       // 아이템으로 올라가는 수치
            private string description;
            public int price;
            public bool sold;
            public bool equip;
            public Item(string _name, int _type, int _stat, string _description,int _price)
            {
                name = _name;
                itemType = _type;
                stat = _stat;
                price = _price;
                description = _description;
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
