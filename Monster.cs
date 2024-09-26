//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        class Monster
        {
            public int level;
            public string name;
            public int health;
            public int attack;

            public Monster(string _name, int _level, int _health, int _attack)
            {
                name = _name;
                level = _level;
                health = _health;
                attack = _attack;
            }
        }
    }

}
