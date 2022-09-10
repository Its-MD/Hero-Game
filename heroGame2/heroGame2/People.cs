using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heroGame2
{
    internal class People
    {
        public int hp = 0;
        public int damage = 0;
        public Bonus bonus = Bonus.Empty;
        public string name;
        public Roles role = Roles.Empty;

        public void Attack(People target)
        {
            target.hp -= damage;
            Console.WriteLine($"{name} attacked {target.name}, left them with {target.hp}");
            if (target.hp <= 0)
            {
                Console.WriteLine($"{target.name} has died");
            }

        }
    }
}
