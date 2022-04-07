using System;
using DataBase.Game;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils;
using ECS.Views;
using ECS.Views.Impls;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems
{
    public class ReplaceChunksSystem : ReactiveSystem<ChunkChangeEventComponent>
    {
        private EcsFilter<LinkComponent, PositionComponent, ChunkComponent>  _chunks;
        private EcsFilter<CurrentChunkComponent, PositionComponent>  _current;
        private readonly EcsFilter<GameStageComponent> _gameStage;
        protected override EcsFilter<ChunkChangeEventComponent> ReactiveFilter { get; }
        protected override void Execute(EcsEntity entity)
        {
            if (_gameStage.Get1(0).Value != EGameStage.Play) return;
            foreach (var c in _chunks)
            {
                var side = entity.Get<ChunkChangeEventComponent>().side;
                ref var pos = ref _chunks.Get2(c).Value;
                var posCur = _current.Get2(0).Value;
                var offset = 200;
                switch (side)
                {
                    case ESide.Up:
                        if (posCur.z - pos.z >= offset)
                        {
                            pos.z = posCur.z + 100;
                        }
                        break;
                    case ESide.Down:
                        if (posCur.z - pos.z <= -offset)
                        {
                            pos.z = posCur.z - 100;
                        }
                        break;
                    case ESide.Right:
                        if (posCur.x - pos.x >= offset)
                        {
                            pos.x = posCur.x + 100;
                        }
                        break;
                    case ESide.Left:
                        if (posCur.x - pos.x <= -offset)
                        {
                            pos.x = posCur.x - 100;
                        }
                        break;
                }

                //var chunkView = _chunks.Get1(c).View as ChunkView;
                //chunkView.surface.BuildNavMesh();
            }
        }
    }
}