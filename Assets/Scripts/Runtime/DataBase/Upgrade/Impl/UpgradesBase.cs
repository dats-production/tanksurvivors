using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase.Shop.Impl
{
    [CreateAssetMenu(menuName = "Bases/UpgradesBase", fileName = "UpgradesBase")]
    public class UpgradesBase : ScriptableObject, IUpgradesBase
    {
        [SerializeField] private Upgrade[] upgrades;
        private List<Upgrade> unlockedUpgrades = new List<Upgrade>();

        public Upgrade Get(EUpgradeType type)
        {
            for (var i = 0; i < upgrades.Length; i++)
            {
                var upg = upgrades[i];
                //if (upg.type == type && upg.isUnlocked)
                if (upg.type == type)
                    return upg;
            }

            throw new Exception("[UpgradesBase] Can't find prefab with name: " + type);
        }
        public void Set(EUpgradeType type, Upgrade upg)
        {
            for (var i = 0; i < upgrades.Length; i++)
            {
                var upgrade = upgrades[i];
                //if (upgrade.type == type && upgrade.isUnlocked)
                if (upgrade.type == type)
                    upgrades[i] = upg;
            }
        }

        public IEnumerable<Upgrade> GetAllUpgrades => upgrades;
        
        //public List<Upgrade> GetUnlockedUpgrades => unlockedUpgrades;

        // public void Upgrade(EUpgradeType type)
        // {
        //     var upg = Get(type);
        //     upg.unlockedLevel++;
        //     Debug.Log(upg.unlockedLevel);
        // }
    }

    [Serializable]
    public struct Upgrade
    {
        public EUpgradeType type;
        public string name;
        public Sprite icon;
        //public bool isNew;
        public bool isMaxLevel;
        public int unlockedLevel;
        public UpgLevels[] levels;
    }
    
    [Serializable]
    public struct UpgLevels
    {
        public string description;
        public float value;
    }
    public enum EUpgradeType
    {
        Weapon1,
        Weapon2,
        Weapon3,
        Weapon4,
        Health,
        Speed,
        MagneticField
    }
}