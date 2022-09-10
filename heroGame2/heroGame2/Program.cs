using System;
namespace heroGame2
{
    enum Roles
    {
        Archer,
        Warrior,
        Wizard,
        Empty
    }
    enum Bonus
    {
        HP,
        Damage,
        Empty
    }
    class Program
    {
        static List<People> EnemyList = new List<People>();
        static void Main(string[] args)
        {
            People player = new();
            People enemey1 = new();
            People enemey2 = new();
            EnemyList.Add(enemey1);
            EnemyList.Add(enemey2);
            CreatePeople(player, true);
            CreatePeople(enemey1, false);
            CreatePeople(enemey2, false);
            Days(player);
        }
        static void CreatePeople(People createObj, bool player)
        {
            string[] ROLES = { "ARCHER", "WARRIOR", "WIZARD" };
            string[] BONUS = { "More Health", "More Damage" };
            if (player)
            {
                Console.WriteLine("enter name");
                createObj.name = Console.ReadLine();
                Console.WriteLine($"Hello {createObj.name}!");
            }
            else
            {
                string[] enemNames = { "A", "B", "C", "D" };
                Random rand = new();
                createObj.name = enemNames[rand.Next(enemNames.Length)];
            }
            ChooseRoles(ROLES, createObj, player);

            ChooseBonus(BONUS, createObj, player);

        }
        static void ChooseRoles(string[] ROLES, People RolesObj, bool role)
        {
            string roleChoice;
            while (RolesObj.role == Roles.Empty)
            {
                if (role)
                {
                    Console.WriteLine($"Choose your role: 1-{ROLES[(int)Roles.Archer]}, 2-{ROLES[(int)Roles.Warrior]}, 3-{ROLES[(int)Roles.Wizard]}");
                    roleChoice = Console.ReadLine();
                }
                else
                {
                    Random rand = new();
                    roleChoice = rand.Next(1,ROLES.Length + 1).ToString();
                }
                
                switch (roleChoice)
                {
                    case "1":
                        RolesObj.role = Roles.Archer;
                        RolesObj.damage = 30;
                        RolesObj.hp = 110;
                        break;

                    case "2":
                        RolesObj.role = Roles.Warrior;
                        RolesObj.damage = 50;
                        RolesObj.hp = 90;
                        break;

                    case "3":
                        RolesObj.role = Roles.Wizard;
                        RolesObj.damage = 40;
                        RolesObj.hp = 100;
                        break;

                    default:
                        Console.WriteLine("Invalid number, try again");
                        break;
                }
            }
            Console.WriteLine($"{RolesObj.name} chose the role of {ROLES[(int)RolesObj.role]}.");
            Console.WriteLine($"Your damage is {RolesObj.damage} and your hp is {RolesObj.hp}");
        }
        static void ChooseBonus(string[] BONUS, People BonusObj, bool bonus)
        {
            string bonusChoice;
            while (BonusObj.bonus == Bonus.Empty)
            {
                if (bonus)
                {
                    Console.WriteLine($"Choose a bonus: 1-{BONUS[(int)Bonus.HP]}, 2-{BONUS[(int)Bonus.Damage]}. To which you choose, will be added 10 more.");
                    bonusChoice = Console.ReadLine();           
                }
                else
                {
                    Random rand = new();
                    bonusChoice = rand.Next(1, BONUS.Length +1).ToString();
                }
                switch (bonusChoice)
                {
                    case "1":
                        BonusObj.hp = BonusObj.hp + 10;
                        BonusObj.bonus = Bonus.HP;
                        break;

                    case "2":
                        BonusObj.damage = BonusObj.damage + 10;
                        BonusObj.bonus = Bonus.Damage;
                        break;

                    default:
                        Console.WriteLine("Invalid number, try again");
                        break;
                }
            }
            Console.WriteLine($"{BonusObj.name} chose bonus - {BONUS[(int)BonusObj.bonus]}.");
            Console.WriteLine($"Your final damage is {BonusObj.damage} and health is {BonusObj.hp}.");
        }
        static void Days(People daysObj)
        {
            int limitHp = daysObj.hp;
            for (int days = 1; days < 4; days++) //days beginning
            {
                Console.WriteLine($"Day {days} has began");
                int countRevive = 0;
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Choose action: 1-walk, 2-sleep, 3-attack, 4-revive. You have 5 actions per day. You can revive once a day.");
                    switch (Console.ReadLine())
                    {

                        case "1": // walking
                            Random rnd = new();
                            int km = rnd.Next(11);
                            Console.WriteLine($"{daysObj.name} walked {km} km.");
                            break;

                        case "2": //sleeping
                            Console.WriteLine($"{daysObj.name} slept until the next day.");
                            i = 5;
                            break;

                        case "3": //attacking
                            if(EnemyList.Count != 0)
                            {
                                Attacking(daysObj, EnemyList[0]);
                                if (daysObj.hp <= 0)
                                {
                                    Console.WriteLine("Game over");
                                    i = 5;
                                    days = 4;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No more enemies left");
                            }
                            break;

                        case "4": //reviving
                            if (countRevive == 0)
                            {
                                if (daysObj.hp == limitHp)
                                {
                                    Console.WriteLine("Cannot revive since your health is at max.");
                                }
                                else if (daysObj.hp < limitHp)
                                {
                                    daysObj.hp = limitHp;
                                    Console.WriteLine($"You have revived. You health is at {daysObj.hp}");
                                    countRevive = 1;
                                }
                            }
                            else
                            {
                                Console.WriteLine("You can only revive once a day.");
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid number, try again");
                            break;
                    }


                } // end of actions per day
                Console.WriteLine($"Day {days} has ended");
            } // end of days (end of program)

        }
        static void Attacking(People player, People enemey)
        {
            while(player.hp > 0 && enemey.hp > 0)
            {
                player.Attack(enemey);
                if (enemey.hp > 0)
                {
                    enemey.Attack(player);
                }
            }
            if (enemey.hp <= 0)
            {
                EnemyList.Remove(enemey);
            }
        }
    }
}