using System;
using UnityEngine;

namespace Runtime.DataBase.General.GameCFG
{
    [Serializable]
    public struct EnemyWavesCfg
    {
        public EnemyWave[] enemyWave;
    }
    
    [Serializable]
    public class EnemyWave
    {
        public string name;
        public int startTime;
        public int endTime;
        public float spawnRate;
        public int enemiesPerSpawn;
        public Enemy enemy;
    }
    
    [Serializable]
    public class Enemy
    {
        [Range(0,7)]
        public int enemyType;
        public float health;
        public float damage;
        public float moveSpeed;
        public int exp;
    }
}