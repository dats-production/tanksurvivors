using System;

namespace Runtime.DataBase.General.GameCFG
{
    [Serializable]
    public struct Weapon3
    {
        public string name;
        public int level;
        public float damageMin;
        public float damageMax;
        public float triggerDistance;
        public float fireRate;
        public float explosionRadius;         
        public float poolCount;
    }
}