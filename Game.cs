using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{

    public struct Item
    {
        public string name;
        public float StatBoost;
    }

    //TEST COMMIT
    class Game
    {
        private bool _gameOver;
        private int _currentScene;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex;
        private Entity _currentEnemy;
        private Item[] KnightItems;
        private Item[] WizardItems;

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
        }

        void initializeItems()
        {
            Item wand = new Item { name = "Wand", StatBoost = 20f};
            Item Shield = new Item { name = "Shield", StatBoost = 15f};
            Item Sword = new Item { name = "Sword", StatBoost = 15f };
            Item Gun = new Item { name = "Gun", StatBoost = 30f };
            Item shoes = new Item { name = "Shoes", StatBoost = 30f };

            WizardItems = new Item[] { wand, Gun };
            KnightItems = new Item[] { shoes, Sword, Shield};

        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            _currentScene = 0;
            InitializeEnemies();
            initializeItems();

        }

        public void InitializeEnemies()
        {
            _currentEnemyIndex = 0;

            Entity slime = new Entity { Name = "Slime", Attack = 1, Defense = 0, Health = 10 };
            Entity zombie = new Entity { Name = "Zom-B", Attack = 5, Defense = 2, Health = 15 };
            Entity human = new Entity { Name = "Uncle Phil", Attack = 10, Defense = 5, Health = 25 };
            _enemies = new Entity[] { slime, zombie, human };

            _currentEnemy = _enemies[_currentEnemyIndex];

        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("Goodbye!");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Gets an input from the _player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the _player can choose</param>
        /// <param name="option2">The second option the _player can choose</param>
        /// <returns></returns>
        int GetInput(string description, params string[] options)
        {
            string input;
            int InputRecieved = -1;
            while (InputRecieved == -1) ;
            {
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine("[" + (i + 1) + "]" + " " + options[i]);
                }
                Console.Write(">");
                input = Console.ReadLine();
                if (int.TryParse(input, out InputRecieved))
                {
                    if (InputRecieved <= 0 || InputRecieved > options.Length)
                    {
                        InputRecieved = -1;
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
            }
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void DisplayCurrentScene()
        {
            switch(_currentScene)
            {
                case 0:
                    DisplayMainMenu();
                    break;
                case 1:
                    GetPlayerName();
                    break;
                case 2:
                    Battle();
                    CheckBattleResults();
                    break;
                case 3:
                    DisplayMainMenu();
                    break;
            }
        }

        /// <summary>
        /// Displays the menu that allows the _player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int PlayAgain = GetInput("Play Again", "Yes", "No");
            if(PlayAgain == 1)
            {
                _currentScene = 0;
                _currentEnemyIndex = 0;
                _currentEnemy = _enemies[_currentEnemyIndex];
            }
            else
            {
                _gameOver = true;
            }   
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the _player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            Console.WriteLine("What is your name");
            Console.Write(">");
            _player.Name = Console.ReadLine();
            Console.Clear();
            int correctName = GetInput("Are you sure you'd like to keep the name: " + _player.Name + "?", "Yes", "No");
            if(correctName == 1)
            {
                _currentScene++;
            }
        }

        /// <summary>
        /// Gets the players choice of character. Updates _player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
           int CharacterClass = GetInput("Choose your class", "Wizard", "Knight");
            if(CharacterClass == 1)
            {
                _player.Health = 50;
                _player.Attack = 25;
                _player.Defense = 5;
                _currentScene++;
            }
            else if (CharacterClass == 2)   
            {
                _player.Health = 75;
                _player.Attack = 15;
                _player.Attack = 10;
                _currentScene++;
            }

            _currentScene++;
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {
            Console.WriteLine(character.Name + "\nHP:" + character.Health + "\nAttack:" + character.Attack
                + "\nDefense:" + character.Defense);

        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float DamageDealt = 0f;
            DisplayStats(_player);
            DisplayStats(_currentEnemy);
            int Choice = GetInput("A " + _currentEnemy + " Approaches you", "Attack", "Equip");
            if(Choice == 1)
            {
                DamageDealt = _player.AttackEntity(_currentEnemy);
                Console.WriteLine("You did " + DamageDealt + " damage");
            }
            else if (Choice == 2)
            {
                Console.WriteLine("You dodged the " + _currentEnemy + "'s attack");
                Console.ReadKey(true);
                Console.Clear();
                return;
            }

            DamageDealt = _currentEnemy.AttackEntity(_player);
            Console.WriteLine(_currentEnemy.Name + "dealt" + DamageDealt + " damage.");
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Checks to see if either the _player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            if(_player.Health <= 0)
            {
                Console.WriteLine("You were slain");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = 3;
            }
            else if(_currentEnemy.Health <= 0)
            {
                Console.WriteLine("You slayed the " + _currentEnemy.Name);
                if(_currentEnemyIndex >= _enemies.Length)
                {
                    _currentScene = 3;
                    return;
                }

                _currentEnemy = _enemies[_currentEnemyIndex];
            }
        }


    }
}
