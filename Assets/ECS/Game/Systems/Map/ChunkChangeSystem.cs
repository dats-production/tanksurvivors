using System;
using DataBase.Game;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using ECS.Views.Impls;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ChunkChangeSystem : IEcsUpdateSystem
{
    [Inject] private SignalBus _signalBus;
    //[Inject] private IGameStageService _gameStage;

    private EcsFilter<LinkComponent, ChunkComponent>  _chunks;
    private EcsFilter<CurrentChunkComponent>  _current;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private EcsFilter<TimerComponent>  _timer;
    private readonly EcsFilter<GameStageComponent> _gameStage;

    private float minDistanse=9999;
    private float countdown;
    float timer;    
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        // if (_timer.Get1(0).Value.Seconds - 1f >= countdown)
        // {

        // }
        timer += Time.deltaTime;
        if (!(timer >= 0.1f)) return;
        
        var fps = 1 / Time.unscaledDeltaTime;
        _signalBus.Fire(new SignalFPS(fps));       
        
        var playerTransform = _player.Get1(0).View.Transform;
        Debug.DrawRay(playerTransform.position, Vector3.down, Color.green);
        int screwPathIndex = LayerMask.NameToLayer("Ground");
        int screwMask = (1 << screwPathIndex);
        if (!Physics.Raycast(playerTransform.position + new Vector3(0, 1, 0), Vector3.down, out var hit, 20, screwMask)) return; 
        if (!_chunks.TryGetLinkOf(hit.collider.gameObject, out var newChunk)) return;
        foreach (var cur in _current)
        {
            var oldChunk = _current.GetEntity(cur);                 
            if (newChunk != oldChunk)
            {
                oldChunk.Del<CurrentChunkComponent>();                        
                newChunk.Get<CurrentChunkComponent>();
                var posOld = oldChunk.Get<PositionComponent>().Value;
                var posNew = newChunk.Get<PositionComponent>().Value;
                            
                if (posNew.z - posOld.z>0)
                    newChunk.Get<ChunkChangeEventComponent>().side = ESide.Up;
                if (posNew.z - posOld.z<0)
                    newChunk.Get<ChunkChangeEventComponent>().side = ESide.Down;
                if (posNew.x - posOld.x>0)
                    newChunk.Get<ChunkChangeEventComponent>().side = ESide.Right;
                if (posNew.x - posOld.x<0)
                    newChunk.Get<ChunkChangeEventComponent>().side = ESide.Left;
            }
        }
        timer=0;
    }
}