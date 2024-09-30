//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;

namespace TextRPG_project
{
    internal partial class Program
    {
        public class Monster
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
            public Monster(Monster monster)
            {
                level = monster.level;
                name = monster.name;
                health = monster.health;
                attack = monster.attack;
            }

            public bool IsDead()
            {
                return health <= 0;
            }

        }
    }

}
