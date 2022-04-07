using UnityEngine;

namespace Runtime.DataBase.General.GameCFG.Impl
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Settings/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField] private PlayerCfg playerCfg;
        //[SerializeField] private EnemyCfg enemyCfg;
        [SerializeField] private ExperienceCfg experienceCfg;
        [SerializeField] private WeaponsCfg weaponsCfg;        
        [SerializeField] private EnemyWavesCfg enemyWavesCfg;
        [SerializeField] private GlobalCfg globalCfg;        

        public PlayerCfg PlayerCfg => playerCfg;

        public ExperienceCfg ExperienceCfg => experienceCfg;

        public GlobalCfg GlobalCfg => globalCfg;
        public WeaponsCfg WeaponsCfg => weaponsCfg;        
        public EnemyWavesCfg EnemyWavesCfg => enemyWavesCfg;
        //public EnemyCfg EnemyCfg => enemyCfg;
    }
}