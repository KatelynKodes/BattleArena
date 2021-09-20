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
        public Item currentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
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

        public Player(string name, float health, float Attk, float Def, Item[] Inventory) : base(name, health, Attk, Def)
        {
            items = Inventory;
            
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
            base.Save(writer);
            writer.WriteLine(currentItemIndex);
        }
    }
}
