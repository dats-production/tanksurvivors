using ECS.Game.Systems;
using ECS.Game.Systems.GameDay;
using ECS.Game.Systems.Linked;
using ECS.Game.Systems.Move;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace ECS.Installers
{
    public class EcsInstaller : MonoInstaller
    {
        [SerializeField] private GetPointFromScene _getPointFromScene;
        [SerializeField] private PlayerConfigSettings _playerConfigSettings;
        public override void InstallBindings()
        {
            Container.Bind<GetPointFromScene>().FromInstance(_getPointFromScene).AsSingle();
            Container.Bind<PlayerConfigSettings>().FromInstance(_playerConfigSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<EcsWorld>().AsSingle().NonLazy();
            BindSystems();
            Container.BindInterfacesTo<EcsMainBootstrap>().AsSingle();

        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<IsAvailableSetViewSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<AddPoolMembersSystem>().AsSingle();  
            Container.BindInterfacesAndSelfTo<ActivateWeaponSystem>().AsSingle();            
            Container.BindInterfacesAndSelfTo<InstantiateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTimerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PositionRotationTranslateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<WavesSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnEnemySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyMoveSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChunkChangeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ReplaceChunksSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraMoveSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SetClosestEnemySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<Weapon1System>().AsSingle();
            Container.BindInterfacesAndSelfTo<Weapon2System>().AsSingle();
            Container.BindInterfacesAndSelfTo<Weapon3System>().AsSingle();
            Container.BindInterfacesAndSelfTo<Weapon4System>().AsSingle();
            Container.BindInterfacesAndSelfTo<MoveRotateToTargetSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<HomingMissileSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletDamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<MineDamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExplosionDamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageUISystem>().AsSingle();            
            Container.BindInterfacesAndSelfTo<DeathSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExperiensPickupSystem>().AsSingle();            
            Container.BindInterfacesAndSelfTo<AddExpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelUpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgWeapon1System>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgWeapon2System>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgWeapon3System>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpgWeapon4System>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveGameSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStageSystem>().AsSingle();        //always must been last
            Container.BindInterfacesAndSelfTo<CleanUpSystem>().AsSingle();          //must been latest than last!
        }       
    }
}