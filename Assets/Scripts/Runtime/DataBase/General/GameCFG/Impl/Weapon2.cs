using System;

namespace Runtime.DataBase.General.GameCFG
{
    [Serializable]
    public struct Weapon2
    {
        public string name;
        public int level;
        public float damageMin;
        public float damageMax;
        public float explosionRadius;        
        public float fireRate;
        public float attackRange;
        //public float rotationSpeed;
        public int missileCountPerShot;
        public float missileSpeed;
        public float missileRadius;
        //public float missileFlyDistance;
        public float poolCount;
    }
}