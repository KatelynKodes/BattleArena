using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    /// <summary>
    /// Entity Class
    /// </summary>
    public class Entity
    {
        //Private Vars
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;

        // Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public float Attack
        {
            get { return _attackPower; }
            set { _attackPower = value; }
        }
        public float Defense
        {
            get { return _defensePower; }
            set { _defensePower = value; }
        }

        public Entity()
        {
            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;
        }

        public Entity(string name, float health, float AttkPwr, float DefnsePwer)
        {
            _name = name;
            _health = health;
            _attackPower = AttkPwr;
            _defensePower = DefnsePwer;
        }

        public float TakeDamage(float damageamount)
        {
            float DamageTaken = damageamount - _defensePower;
            if (DamageTaken <= 0)
            {
                DamageTaken = 0;
            }

            _health -= DamageTaken;
            return DamageTaken;
        }

        public float AttackEntity(Entity defender)
        {
            return defender.TakeDamage(_attackPower);
        }
    }
}
