using System;
using System.Diagnostics.CodeAnalysis;
using DataBase.Game;
using DataBase.Objects;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Systems.Move;
using ECS.Utils.Extensions;
using ECS.Views;
using ECS.Views.Impls;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Services.Uid;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AddExpSystem : ReactiveSystem<AddExpEventComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, PlayerLevelComponent, PlayerExpComponent> _player;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<AddExpEventComponent> ReactiveFilter { get; }    
    
    public int exp;
    public int MAX_LEVEL = 99;
    
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        ref var playerLevel = ref _player.Get2(0).Value;
        ref var playerExp = ref _player.Get3(0).Value;

        var addExp = entity.Get<AddExpEventComponent>().Value;

        int oldLevel = GetLevelForExp(playerExp);
        var sum = playerExp + addExp;
            //Debug.Log(oldLevel+" Level. CurExp " +playerExp+" + " +addExp+" = " +sum +". GetExp next " + GetExpForLevel(playerLevel+1)); 
        playerExp += addExp;
        _signalBus.Fire(new SignalExperience(playerExp - GetExpForLevel(playerLevel), GetExpForLevel(playerLevel+1) - GetExpForLevel(playerLevel)));
        if (oldLevel < GetLevelForExp(playerExp))
        {
            if (playerLevel < GetLevelForExp(playerExp))
            {
                playerLevel = GetLevelForExp(playerExp);
                    //Debug.Log(playerLevel +" new level. CurExp " +playerExp+". GetExp next " + GetExpForLevel(playerLevel+1));
                _signalBus.Fire(new SignalNewLevel(playerLevel));
                _signalBus.Fire(new SignalExperience(playerExp- GetExpForLevel(playerLevel), GetExpForLevel(playerLevel+1)  - GetExpForLevel(playerLevel)));
                _player.GetEntity(0).Get<LevelUpComponent>();
            }
        }
    }
    
    public int GetExpForLevel(float level)
    {
        int firstPass = 0;
        int secondPass = 0;
        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int) Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
        }

        return secondPass;
    }
        
    public int GetLevelForExp(float exp)
    {
        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle < MAX_LEVEL; levelCycle++)
        {
            firstPass += (int) Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
            if (secondPass > exp)
                return levelCycle;
        }

        if (exp > secondPass)
            return MAX_LEVEL;
        return 0;
    }
}




public struct AddExpEventComponent
{
    public float Value;
}

public struct PlayerLevelComponent
{
    public int Value;
}
public struct PlayerExpComponent
{
    public float Value;
}

