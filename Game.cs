using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>
    struct Character
    {
        public string name;
        public float health;
        public float attackPower;
        public float defensePower;
    }

    class Game
    {
        bool gameOver;
        int currentScene;
        Character player;
        Character[] enemies;
        private int currentEnemyIndex = 0;
        private Character currentEnemy;

        //Enemies
        public Character BeeGhostEnemy;
        public Character PlantSnakeEnemy;
        public Character JakeEnemy;
        public Character UnclePhilBoss;

        //TracksSnakeEncounter
        int SnakeEncounter = 0;

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();
            Update();
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            currentScene = 0;

            //BeeGhostEnemy
            BeeGhostEnemy.name = "Beekeeper Ghost";
            BeeGhostEnemy.health = 100;
            BeeGhostEnemy.attackPower = 20;
            BeeGhostEnemy.defensePower = 30;

            //PlantSnake
            PlantSnakeEnemy.name = "Plant Snake";
            PlantSnakeEnemy.health = 130;
            PlantSnakeEnemy.attackPower = 30;
            PlantSnakeEnemy.defensePower = 10;

            //Jake
            JakeEnemy.name = "Jake, from StateFarm.";
            JakeEnemy.health = 200;
            JakeEnemy.attackPower = 30;
            JakeEnemy.defensePower = 20;

            //Phil
            UnclePhilBoss.name = "Uncle Phil";
            UnclePhilBoss.health = 300;
            UnclePhilBoss.attackPower = 30;
            UnclePhilBoss.defensePower = 20;

            enemies = new Character[] {BeeGhostEnemy, PlantSnakeEnemy, JakeEnemy, UnclePhilBoss};
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            while (!gameOver)
            {
                currentScene++;
                DisplayCurrentScene();
            }
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("Farewell");
        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, string option1, string option2)
        {
            string input = "";
            int inputReceived = 0;

            while (inputReceived != 1 && inputReceived != 2)
            {//Print options
                Console.WriteLine(description);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If player selected the first option...
                if (input == "1" || input == option1)
                {
                    //Set input received to be the first option
                    inputReceived = 1;
                }
                //Otherwise if the player selected the second option...
                else if (input == "2" || input == option2)
                {
                    //Set input received to be the second option
                    inputReceived = 2;
                }
                //If neither are true...
                else
                {
                    //...display error message
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }

                Console.Clear();
            }
            return inputReceived;
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void DisplayCurrentScene()
        {
            switch (currentScene)
            {
                case 1:
                    GetPlayerName();
                    break;
                case 2:
                    Console.WriteLine("You eventually run into a small little ghost..who's apparently..a beekeeper? Interesting.");
                    Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("-- A NEW CHALLENGER APPROACHES --");
                    DisplayStats(BeeGhostEnemy);
                    Console.ReadKey(true);
                    Console.Clear();
                    currentEnemy = enemies[0];
                    Battle();
                    break;
                case 3:
                    Console.WriteLine("After that encounter, you walk into this small garden where you see what seems to be a snake watering her plants");
                    Console.WriteLine("She doesn't seem like much of a threat");
                    Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("-- A NEW CHALLENGER APPROACHES --");
                    DisplayStats(PlantSnakeEnemy);
                    SnakeEncounter = GetInput("What do you do?", "Help water plants", "Attack");
                    if (SnakeEncounter == 1)
                    {
                        Console.WriteLine("You help the snake water her plants");
                        Console.WriteLine("She lets you pass through her garden as you venture forth.");
                    }
                    else if (SnakeEncounter == 2)
                    {
                        Console.WriteLine("Feeling threatened that you decided to attack her, she charges back towards you, starting a battle");
                        currentEnemy = enemies[1];
                        Battle();
                    }
                    break;
                case 4:
                    Console.WriteLine("As you walk further along the path you start to hear this..ominous and unsettling jingle");
                    Console.WriteLine("\"Like A Good Neighbor State Farm Is There\"");
                    Console.WriteLine("Oh no");
                    Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("-- A NEW CHALLENGER APPROACHES --");
                    DisplayStats(JakeEnemy);
                    Console.ReadKey(true);
                    Console.Clear();
                    currentEnemy = enemies[2];
                    Battle();
                    break;
                case 5:
                    if (SnakeEncounter == 1)
                    {
                        Console.WriteLine("During your jouney you receive a letter and a package");
                        Console.WriteLine("It's from the garden snake");
                        Console.WriteLine("\"I have a feelng you will need this for your next challenge, thank you for watering my flowers\"- Gertrude.");
                        Console.WriteLine("Inside the package is a piece of lemon pie.");
                        int EatPie = GetInput("Eat Pie", "Yes", "No");
                        if (EatPie == 1)
                        {
                            Console.WriteLine("You eat the pie");
                            player.health += 60;
                            Console.WriteLine("You gained 60 HP");
                        }
                        else if (EatPie == 2)
                        {
                            Console.WriteLine("You decide not to eat the pie.");                            
                        }
                    }
                    break;
                case 6:
                    Console.WriteLine("Finally! After hours, days, and even months travelling.");
                    Console.WriteLine("You find him");
                    Console.ReadKey(true);
                    Console.Clear();
                    Console.WriteLine("-- A NEW CHALLENGER APPROACHES --");
                    DisplayStats(UnclePhilBoss);
                    Console.ReadKey(true);
                    Console.Clear();
                    currentEnemy = enemies[3];
                    Battle();
                    break;
                case 7:
                    DisplayMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int PlayAgain = GetInput("Would you like to replay? " , "Yes" , "No");
            if (PlayAgain == 1)
            {
                ReInitializeValues();
                currentScene = 0;
            }
            else if (PlayAgain == 2)
            {
                gameOver = true;
            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            Console.WriteLine("Greetings adventurer");
            Console.WriteLine("What is your name?");
            player.name = Console.ReadLine();
            Console.WriteLine("Nice to meet you, " + player.name);
            Console.ReadKey(true);
            Console.Clear();
            CharacterSelection();
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int ClassSelection = GetInput("Now tell me, What is your class?", "Knight", "Bard");
            if (ClassSelection == 1)
            {
                player.health = 150;
                player.attackPower = 50;
                player.defensePower = 20;
            }
            else if (ClassSelection == 2)
            {
                player.health = 100;
                player.attackPower = 40;
                player.defensePower = 30;
            }
            DisplayStats(player);
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Character character)
        {
            Console.WriteLine("Name: " + character.name + "\nHealth: " + character.health + "\nAttack: " + character.attackPower
                + "\nDefense: " + character.defensePower);
            Console.WriteLine("");
        }

        /// <summary>
        /// Calculates the amount of damage that will be done to a character
        /// </summary>
        /// <param name="attackPower">The attacking character's attack power</param>
        /// <param name="defensePower">The defending character's defense power</param>
        /// <returns>The amount of damage done to the defender</returns>
        float CalculateDamage(float attackPower, float defensePower)
        {
            float DamageDealt = attackPower - defensePower;
            if (DamageDealt < 0)
            {
                return 0;
            }
            else
            {
                return DamageDealt;
            }
        }

        /// <summary>
        /// Deals damage to a character based on an attacker's attack power
        /// </summary>
        /// <param name="attacker">The character that initiated the attack</param>
        /// <param name="defender">The character that is being attacked</param>
        /// <returns>The amount of damage done to the defender</returns>
        public float Attack(ref Character attacker, ref Character defender)
        {
            return CalculateDamage(attacker.attackPower, defender.defensePower);
        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float DamageTaken = 0;

            while (player.health > 0 && currentEnemy.health > 0)
            {
                Console.Clear();
                DamageTaken = Attack(ref player, ref currentEnemy);
                Console.WriteLine("You attack " + currentEnemy.name);
                currentEnemy.health = currentEnemy.health - DamageTaken;
                Console.WriteLine(currentEnemy.name + " takes " + DamageTaken + "Points of damage");
                Console.ReadKey(true);
                Console.Clear();

                DamageTaken = Attack(ref currentEnemy, ref player);
                Console.WriteLine(currentEnemy.name + " attacks you!");
                player.health = player.health - DamageTaken;
                Console.WriteLine("You take " + DamageTaken + "Points of damage");
                Console.ReadKey(true);
                Console.Clear();

                DisplayStats(player);
                DisplayStats(currentEnemy);
                Console.ReadKey(true);
                Console.Clear();
            }

            CheckBattleResults();
        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            if (currentEnemy.health <= 0)
            {
                Console.WriteLine("You won!");
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (player.health <= 0)
            {
                Console.WriteLine("You Lost!");
                DisplayMainMenu();
            }
        }

        /// <summary>
        /// Resets the values of the enemies to their starting values
        /// </summary>
        void ReInitializeValues()
        {

            //BeeGhostEnemy
            BeeGhostEnemy.name = "Beekeeper Ghost";
            BeeGhostEnemy.health = 100;
            BeeGhostEnemy.attackPower = 20;
            BeeGhostEnemy.defensePower = 30;

            //PlantSnake
            PlantSnakeEnemy.name = "Plant Snake";
            PlantSnakeEnemy.health = 130;
            PlantSnakeEnemy.attackPower = 30;
            PlantSnakeEnemy.defensePower = 10;

            //Jake
            JakeEnemy.name = "Jake, from StateFarm.";
            JakeEnemy.health = 200;
            JakeEnemy.attackPower = 30;
            JakeEnemy.defensePower = 20;

            //Phil
            UnclePhilBoss.name = "Uncle Phil";
            UnclePhilBoss.health = 300;
            UnclePhilBoss.attackPower = 30;
            UnclePhilBoss.defensePower = 20;

            enemies = new Character[] { BeeGhostEnemy, PlantSnakeEnemy, JakeEnemy, UnclePhilBoss };
        }

    }
}
