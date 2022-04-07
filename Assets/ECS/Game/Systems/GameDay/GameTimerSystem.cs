using DataBase.Game;
using DataBase.Timer;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Events;
using ECS.Game.Components.Listeners;
using Leopotam.Ecs;
using Runtime.DataBase.General.CommonParamsBase;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems.GameDay
{
    public class GameTimerSystem : IEcsUpdateSystem, IEcsInitSystem
    {
        [Inject] private readonly ICommonParamsBase _commonParamsBase;
        
        private readonly EcsWorld _world;
        private readonly EcsFilter<GameStageComponent> _gameStage;
        private readonly EcsFilter<TimerComponent> _timerEntity;
        private readonly EcsFilter<ChangeStageComponent> _changeStageEvent;

        private int _cacheSecond = int.MaxValue;
        private float _timer;

        public void Init()
        {
            foreach (var i in _timerEntity)
            {
                var value = _timerEntity.Get1(i).Value;
                _timer = _timerEntity.Get1(i).Value.ToFloat();
                _cacheSecond = value.ToInt();
            }
                
        }
        public void Run()
        {
            foreach (var g in _changeStageEvent)
            {
                ref var stage = ref _gameStage.Get1(g).Value;
                if (stage == EGameStage.DayEnd) _timer = 0;
            }
            foreach (var g in _gameStage)
            {
                ref var stage = ref _gameStage.Get1(g).Value;
                if (stage != EGameStage.Play) return;
            }
            
            foreach (var t in _timerEntity)
            {
                ref var timerValue = ref _timerEntity.Get1(t).Value;
                ref var multiplier = ref _timerEntity.GetEntity(t).Get<MultiplierComponent>().Value;
                _timer += Time.deltaTime / 2 * (multiplier == 0 ? 1 : multiplier);
                timerValue.Increment(_timer);
                var toSeconds = timerValue.ToInt();
                if (_cacheSecond != toSeconds)  //Second tick
                {
                    _timerEntity.GetEntity(t).Get<TimerTickEventComponent>().Second = toSeconds;
                    _cacheSecond = toSeconds;
                    _timerEntity.GetEntity(t).Get<ListenerComponent<Timer>>().Action?.Invoke(timerValue);
                }
                return;
            }
        }
    }
}