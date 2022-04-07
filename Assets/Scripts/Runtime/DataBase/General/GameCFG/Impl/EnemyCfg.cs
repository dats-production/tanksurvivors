using System;

namespace Runtime.DataBase.General.GameCFG
{
    [Serializable]
    public struct EnemyCfg
    {
        public float enemySpeed;
        public float health;
        public float enemyRadius;
        public float spawnRate;
        public int enemiesPerSpawn;
        public int exp;
        public int poolCount;
    }
}