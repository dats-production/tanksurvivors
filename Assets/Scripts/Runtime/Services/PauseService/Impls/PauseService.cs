﻿using Leopotam.Ecs;

namespace Services.PauseService.Impls
{
    public class PauseService : IPauseService
    {
        private readonly EcsWorld _world;
        public PauseService(EcsWorld world)
        {
            _world = world;
        }
        
        public void PauseGame(bool value)
        {
            //_world.GetGameStage().Get<ChangeStageComponent>().Value = value?  EGameStage.Pause : EGameStage.Play;
        }
    }
}