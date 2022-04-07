
namespace Runtime.DataBase.General.GameCFG
{
    public interface IGameConfig
    {
        PlayerCfg PlayerCfg { get; }
        ExperienceCfg ExperienceCfg { get; }
        GlobalCfg GlobalCfg { get; }
        EnemyWavesCfg EnemyWavesCfg { get; }
        //EnemyCfg EnemyCfg { get; }
        WeaponsCfg WeaponsCfg { get; }
    }
}