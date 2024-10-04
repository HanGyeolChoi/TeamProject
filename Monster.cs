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
            public bool dead;

            public Monster(string name, int level, int health, int attack, bool dead = false)
            {
                this.name = name;
                this.level = level;
                this.health = health;
                this.attack = attack;
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
