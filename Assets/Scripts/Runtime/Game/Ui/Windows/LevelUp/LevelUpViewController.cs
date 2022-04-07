using System;
using System.Collections.Generic;
using System.Linq;
using DataBase.Game;
using DataBase.Shop;
using DataBase.Shop.Impl;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using ECS.Views.Impls;
using Leopotam.Ecs;
using Runtime.Game.Ui;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Signals;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Scripts.Runtime.Game.Ui.Windows.LevelUp 
{
    public class LevelUpViewController : UiController<LevelUpView>, IInitializable, IEcsInitSystem
    {
        private readonly SignalBus _signalBus;
        private readonly IUpgradesBase _upgBase;
        [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
        [Inject] private readonly EcsWorld _world;
        private readonly Dictionary<EUpgradeType, UpgradeView> _upgViews = new Dictionary<EUpgradeType, UpgradeView>();
        private UltimateJoystick _ultimateJoystick;
        
        public LevelUpViewController(SignalBus signalBus, IUpgradesBase upgBase)
        {
            _signalBus = signalBus;
            _upgBase = upgBase;
        }
        
        public void Initialize()
        {
            var upgs = _upgBase.GetAllUpgrades;
            //var playerData = _playerData.GetData();
            for (var i = 0; i < _upgBase.GetAllUpgrades.Count(); i ++)
            {
                var upg = upgs.ElementAt(i);
                if(_upgViews.ContainsKey(upg.type)) continue;
                
                if (upg.type == EUpgradeType.Weapon1)
                    upg.unlockedLevel = 1;
                else upg.unlockedLevel = 0;
                upg.isMaxLevel = false;
                _upgBase.Set(upg.type, upg);
                var upgView = Object.Instantiate(View.upgPrefab, View.upgContainer);
                upgView.Initialise(upg, OnChoose);
                _upgViews.Add(upg.type, upgView);                
            }
        }

        public override void OnShow()
        {
            SetUpgrades();
            var level = _world.GetEntity<PlayerComponent>().Get<PlayerLevelComponent>().Value;
            View.level.text = level.ToString();
            Time.timeScale = 0;
            //_world.GetEntity<GameStageComponent>().Get<GameStageComponent>().Value = EGameStage.Pause;
            _ultimateJoystick.HorizontalAxis = 0;
            _ultimateJoystick.VerticalAxis = 0;
            _ultimateJoystick.UpdatePositioning();
        }

        private void SetUpgrades()
        {
            var upgrades = _upgBase.GetAllUpgrades;

            foreach (var upgrade in upgrades)
            {
                var upgView = _upgViews[upgrade.type];
                upgView.SetUpgradeLevelText(upgrade);
            }
        }
        private void OnChoose(EUpgradeType type)
        {
            var upg = _upgBase.Get(type);

            if (type == EUpgradeType.Weapon2 && upg.unlockedLevel == 0
                || type == EUpgradeType.Weapon3 && upg.unlockedLevel == 0
                || type == EUpgradeType.Weapon4 && upg.unlockedLevel == 0)
            {
                _world.GetEntity<PlayerComponent>().Get<ActivateWeaponEventComponent>().Type = type;
            }
            
            if (type == EUpgradeType.Weapon1 && upg.unlockedLevel >= 1)
            {
                _world.GetEntity<Weapon1Component>().Get<UpgWeapon1EventComponent>().Level = upg.unlockedLevel;
                _world.GetEntity<Weapon1Component>().Get<UpgWeapon1EventComponent>().Value = upg.levels[upg.unlockedLevel].value;
            }
            if (type == EUpgradeType.Weapon2 && upg.unlockedLevel >= 1)
            {
                _world.GetEntity<Weapon2Component>().Get<UpgWeapon2EventComponent>().Level = upg.unlockedLevel;
                _world.GetEntity<Weapon2Component>().Get<UpgWeapon2EventComponent>().Value = upg.levels[upg.unlockedLevel].value;
            }
            if (type == EUpgradeType.Weapon3 && upg.unlockedLevel >= 1)
            {
                _world.GetEntity<Weapon3Component>().Get<UpgWeapon3EventComponent>().Level = upg.unlockedLevel;
                _world.GetEntity<Weapon3Component>().Get<UpgWeapon3EventComponent>().Value = upg.levels[upg.unlockedLevel].value;
            }
            if (type == EUpgradeType.Weapon4 && upg.unlockedLevel >= 1)
            {
                _world.GetEntity<Weapon4Component>().Get<UpgWeapon4EventComponent>().Level = upg.unlockedLevel;
                _world.GetEntity<Weapon4Component>().Get<UpgWeapon4EventComponent>().Value = upg.levels[upg.unlockedLevel].value;
            }
            if (type == EUpgradeType.Health)
            {
                var healthEntity= _world.GetEntity<HealthBarComponent>();
                var health = healthEntity.Get<HealthComponent>().Current 
                        = _world.GetEntity<HealthBarComponent>().Get<HealthComponent>().Max 
                            *= (1 + upg.levels[upg.unlockedLevel].value);
                var healthView = healthEntity.Get<LinkComponent>().Get<HealthBarView>();
                healthView.UpdateHealth(health, health);
            }
            
            if (type == EUpgradeType.Speed)
                _world.GetEntity<PlayerComponent>().Get<SpeedComponent>().Value *= (1 + upg.levels[upg.unlockedLevel].value);
            
            if (type == EUpgradeType.MagneticField)
                _world.GetEntity<PlayerComponent>().Get<TriggerPickupComponent>().Value *= (1 + upg.levels[upg.unlockedLevel].value);
            
            
            
                
            upg.unlockedLevel++;
            if (upg.unlockedLevel >= upg.levels.Length)
            {
                upg.unlockedLevel = upg.levels.Length;
                upg.isMaxLevel = true;
            }
            _upgBase.Set(type, upg);
            _signalBus.BackWindow();
            Time.timeScale = 1;            
            //_world.GetEntity<GameStageComponent>().Get<GameStageComponent>().Value = EGameStage.Play;
            //var playerData = _playerData.GetData();
            //_playerData.Save(playerData);
        }

        public void Init()
        {
            _ultimateJoystick = UltimateJoystick.GetUltimateJoystick("Main");
        }
    }
}