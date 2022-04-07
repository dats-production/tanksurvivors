using System;
using DataBase.Game;
using DataBase.Objects;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.DataSave;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Systems.Move;
using ECS.Utils.Extensions;
using ECS.Views.Impls;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.CommonParamsBase;
using Runtime.DataBase.General.GameCFG;
using Runtime.Game.Ui;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Runtime.Services.GameStateService;
using Services.Uid;
using Signals;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ECS.Game.Systems
{
    public class GameInitializeSystem : IEcsInitSystem
    {
        [Inject] private readonly IGameStateService<GameState> _generalState;
        [Inject] private readonly GetPointFromScene _getPointFromScene;
        [Inject] private readonly ICommonParamsBase _commonParamsBase;
        [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _commonPlayerData;
        [Inject] private IGameConfig _config;
        
        private readonly EcsWorld _world;
        public void Init()
        {
            if (LoadGame()) return;
            CreateCamera();
            CreateTimer();
            CreatePlayer();
            CreateChunks();
            CreateEnemies();
            CreateWeapon1();
            CreateBullets();
            CreateWeapon2();
            CreateMissiles();
            CreateWeapon3();
            CreateMines();
            CreateExperience();
            CreateWeapon4();
            CreateDamageUI();
            HealthBar();
            CreateExplosions();
            CreateSparks();
            Amplitude.Instance.logEvent("level_started");
        }

        private void CreateSparks()
        {
            for (int i = 0; i < 10; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<SparksComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "Sparks";
            }
        }
        private void CreateExplosions()
        {
            for (int i = 0; i < 10; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<ExplosionComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "Explosion";
            }
        }
        
        private void HealthBar()
        {
            var hbFromScene = Object.FindObjectsOfType<HealthBarView>();
            foreach (var link in hbFromScene)
            {
                var entity = _world.NewEntity();
                entity.Get<HealthBarComponent>();
                entity.Get<HealthComponent>().Current = _config.PlayerCfg.health;
                entity.Get<HealthComponent>().Max = _config.PlayerCfg.health;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                link.Link(entity);
                entity.Get<LinkComponent>().View = link;
            }
        }
        private void CreateDamageUI()
        {
            for (int i = 0; i < 20; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<DamageUIComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "DamageUI";
            }
        }

        private void CreateWeapon4()
        {
            var w4FromScene = Object.FindObjectsOfType<Weapon4View>();
            foreach (var link in w4FromScene)
            {
                var entity = _world.NewEntity();
                entity.Get<Weapon4Component>();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                link.Link(entity);
                entity.Get<LinkComponent>().View = link;
            } 
        }

        private void CreateExperience()
        {
            for (int i = 0; i < _config.ExperienceCfg.poolCount; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<ExperienceComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "Experience";
            }
        }

        private void CreateMines()
        {
            for (int i = 0; i < _config.WeaponsCfg.w3.poolCount; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<MineComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "Mine";
            }
        }

        private void CreateWeapon3()
        {
            var w3FromScene = Object.FindObjectsOfType<Weapon3View>();
            foreach (var link in w3FromScene)
            {
                var entity = _world.NewEntity();
                entity.Get<Weapon3Component>();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                link.Link(entity);
                entity.Get<LinkComponent>().View = link;
            } 
        }

        private void CreateMissiles()
        {
            for (int i = 0; i < _config.WeaponsCfg.w2.poolCount; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<MissileComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "Rocket";
            }
        }

        private void CreateWeapon2()
        {
            var w2FromScene = Object.FindObjectsOfType<Weapon2View>();
            foreach (var link in w2FromScene)
            {
                var entity = _world.NewEntity();
                entity.Get<Weapon2Component>();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                link.Link(entity);
                entity.Get<LinkComponent>().View = link;
            } 
        }

        private void CreateBullets()
        {
            for (int i = 0; i < _config.WeaponsCfg.w1.poolCount; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<BulletComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "Bullet";
            }
        }

        private void CreateWeapon1()
        {
            var w1FromScene = Object.FindObjectsOfType<Weapon1View>();
            foreach (var link in w1FromScene)
            {
                var entity = _world.NewEntity();
                entity.Get<Weapon1Component>();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                link.Link(entity);
                entity.Get<LinkComponent>().View = link;
            }   
        }

        private void CreateEnemies()
        {
            var data = _commonPlayerData.GetData();
            var enemyNum = 100;//data.EnemiesNum;
            for (int i = 0; i < enemyNum; i++)
            {
                var entity = _world.NewEntity();
                entity.Get<EnemyComponent>();
                entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                entity.GetAndFire<PrefabComponent>().Value = "Enemy";
            }
        }

        private void CreateChunks()
        {
            var id = 0;
            var chunkNumInRaw = 3;
            for (int i = 0; i < chunkNumInRaw; i++)
            {
                for (int j = 0; j < chunkNumInRaw; j++)
                {
                    var x = i - 1;
                    var z = j - 1;
                    var entity = _world.NewEntity();
                    entity.Get<ChunkComponent>().id = id;
                    entity.Get<PositionComponent>().Value.x = x*100;
                    entity.Get<PositionComponent>().Value.z = z*100;
                    entity.Get<UIdComponent>().Value = UidGenerator.Next();
                    entity.GetAndFire<PrefabComponent>().Value = "Chunk";
                    if (id == 4) entity.Get<CurrentChunkComponent>();                    
                    id++;                    
                }
            }
        }


        private void CreatePlayer()
        {
            var playerFromScene = Object.FindObjectsOfType<PlayerView>();
            foreach (var link in playerFromScene)
            {
                var entity = _world.NewEntity();
                entity.Get<PlayerComponent>();
                entity.Get<AddExpEventComponent>();
                entity.Get<PlayerLevelComponent>().Value = 1;
                entity.Get<PlayerExpComponent>();

                entity.Get<SpeedComponent>().Value = _config.PlayerCfg.baseSpeed;
                entity.Get<TriggerPickupComponent>().Value = _config.PlayerCfg.triggerDistance;
                entity.Get<DirectionComponent>();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                link.Link(entity);
                entity.Get<LinkComponent>().View = link;
            }        
        }

        private bool LoadGame()
        {
            _world.NewEntity().Get<GameStageComponent>().Value = EGameStage.Play;
            var gState = _generalState.GetData();
            if (gState.SaveState.IsNullOrEmpty()) return false;
            foreach (var state in gState.SaveState)
            {
                var entity =_world.NewEntity();
                state.ReadState(entity);
            }
            return true;
        }

        private void CreateTimer()
        {
            var entity = _world.NewEntity();
            entity.Get<TimerComponent>();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
        }
        
        private void CreateCamera()
        {
            var camFromScene = Object.FindObjectsOfType<CameraView>();
            foreach (var link in camFromScene)
            {
                var entity = _world.NewEntity();
                entity.Get<CameraComponent>();
                entity.Get<UIdComponent>().Value = UidGenerator.Next();
                link.Link(entity);
                entity.Get<LinkComponent>().View = link;
            }
        }
    }
}

public struct HealthComponent
{
    public float Current;
    public float Max;
}
public struct SpeedComponent
{
    public float Value;
}
public struct TriggerPickupComponent
{
    public float Value;
}
public struct HealthBarComponent : IEcsIgnoreInFilter
{
}
public struct ExplosionComponent : IEcsIgnoreInFilter
{
}
public struct SparksComponent : IEcsIgnoreInFilter
{
}