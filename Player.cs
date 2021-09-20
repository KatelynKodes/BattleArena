using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BattleArena 
{
    class Player : Entity
    {
        public Item[] items;
        public int currentItemIndex;
        public string Class;
        private Item CurrItem;

        public Item currentItem
        {
            get { return CurrItem; }
            set { CurrItem = value; }
        }

        public string CurrentJob 
        {
            get { return Class; }
            set { Class = value; }
        }

        public override float Attack 
        {
            set
            {
                if (currentItem.ItemId == ItemType.ATTACK)
                {
                    base.Attack += currentItem.StatBoost;
                }
            }
        }

        public override float Defense 
        {
            set
            {
                if (currentItem.ItemId == ItemType.DEFENSE)
                {
                    base.Defense += currentItem.StatBoost;
                }
            }
        }

        public string[] GetItemNames()
        {
            string[] ItemNames = new string[items.Length];

            for (int i = 0; i < ItemNames.Length; i++)
            {
                ItemNames[i] = items[i].name;
            }

            return ItemNames;
        }

        public Player() : base()
        {
            
        }

        public Player(string name, float health, float Attk, float Def, Item[] Inventory, string job) : base(name, health, Attk, Def)
        {
            items = Inventory;
            Class = job;
            currentItemIndex = -1;
        }

        /// <summary>
        /// Allows player to equip an item based on the item's index number.
        /// </summary>
        /// <param name="currentItemValue"></param>
        /// <returns></returns>
        public bool EquipItem(int currentItemValue)
        {
            if (currentItemValue <= items.Length || currentItemIndex < 0)
            {
                return false;
            }

            currentItemIndex = currentItemValue;
            currentItem = items[currentItemIndex];
            return true;
        }

        public bool UnEquipItem()
        {
            if (currentItem.name == "nothing")
            {
                return false;
            }

            return true;
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine(CurrentJob);
            base.Save(writer);
            writer.WriteLine(currentItemIndex);
        }

        public override bool Load(StreamReader reader)
        {
            if (!base.Load(reader))
            {
                return false;
            }

            if (!int.TryParse(reader.ReadLine(), out currentItemIndex))
            {
                return false;
            }

            if (!EquipItem(currentItemIndex))
            {
                return false;
            }

            return true;
        }
    }
}
