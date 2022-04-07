using System;

namespace Runtime.DataBase.General.GameCFG
{
    [Serializable]
    public struct Weapon4
    {
        public string name;
        public int level;
        public float damageMin;
        public float damageMax;
        public float fireRate;
        public float attackRange;
    }
}