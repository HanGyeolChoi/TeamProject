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
            public int price;
            public bool sold;
            public bool equip;
            public Item(string _name, int _type, int _stat, int _price)
            {
                name = _name;
                itemType = _type;
                stat = _stat;
                price = _price;
                sold = false;
                equip = false;
            }

            public void ShowItem()
            {
                if (itemType == 1)
                    Console.Write($"{name}| 방어력 +{stat}\t");
                else Console.Write($"{name}| 공격력 +{stat}\t");
            }
        }
    }
}
