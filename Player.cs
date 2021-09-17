using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena 
{
    class Player : Entity
    {
        public Item[] items;
        public int currentItemIndex;
        public int currentItem
        {
            get { return currentItemIndex; }
        }

        public Player(string name, float health, float Attk, float Def, Item[] Inventory)
        {
            Name = name;
            Health = health;
            Attack = Attk;
            Defense = Def;
            items = Inventory;
        }

        /// <summary>
        /// Allows player to equip an item based on the item's index number.
        /// </summary>
        /// <param name="currentItemValue"></param>
        /// <returns></returns>
        public bool EquipItem(int currentItemValue)
        {
            if (currentItemValue <= items.Length || currentItem < 0)
            {
                return false;
            }

            currentItemIndex = currentItemValue;
            return true;

        }

        public bool UnEquipItem()
        {
            if (currentItem.Name == "nothing")
            {
                return false;
            }

            return true;
        }
    }
}
