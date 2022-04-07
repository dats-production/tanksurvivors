using System;

namespace Runtime.DataBase.General.GameCFG
{
    [Serializable]
    public struct Weapon1
    {
        public string name;
        public int level;
        public float damageMin;
        public float damageMax;
        public float fireRate;
        public float attackRange;
        public float rotationSpeed;
        public int bulletCountPerShot;
        public float bulletSpeed;
        public float bulletRadius;
        public float bulletFlyDistance;
        public float poolCount;
    }
}