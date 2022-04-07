using System;
using System.Diagnostics.CodeAnalysis;
using DataBase.Game;
using DataBase.Objects;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Systems.Move;
using ECS.Utils.Extensions;
using ECS.Views.Impls;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Services.Uid;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class WavesSystem : IEcsUpdateSystem
{
    [Inject] private readonly GetPointFromScene _getPointFromScene;
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent>.Exclude<IsAvailableComponent>  _enemies;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private EcsFilter<TimerComponent>  _timer;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    float spawnTimer;
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var waveTimer = _timer.Get1(0).Value.ToInt();
        spawnTimer += Time.deltaTime;
        for (int i = 0; i < _config.EnemyWavesCfg.enemyWave.Length; i++)
        {
            var startTime = _config.EnemyWavesCfg.enemyWave[i].startTime;
            var endTime = _config.EnemyWavesCfg.enemyWave[i].endTime;
            var spawnRate = _config.EnemyWavesCfg.enemyWave[i].spawnRate;


            if (waveTimer < startTime || waveTimer >= endTime) continue;
            if (spawnTimer < spawnRate) return;

            var playerTr = _player.Get1(0).View.Transform;
            var enemiesPerSpawn = _config.EnemyWavesCfg.enemyWave[i].enemiesPerSpawn;

            for (int j = 0; j < enemiesPerSpawn; j++)
            {
                
                var pos = playerTr.position + _getPointFromScene.GetRandomPointWithSaveArea();
                var spawnEntity = _world.NewEntity();
                spawnEntity.Get<SpawnEnemyComponent>().Enemy = _config.EnemyWavesCfg.enemyWave[i].enemy;
                spawnEntity.Get<SpawnEnemyComponent>().Pos = pos;
            }
            spawnTimer=0;
        }
    }
}

public struct SpawnEnemyComponent
{
    public Vector3 Pos;
    public Enemy Enemy;
}


