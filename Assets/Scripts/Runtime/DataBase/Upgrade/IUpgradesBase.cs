using System.Collections.Generic;
using DataBase.Shop.Impl;
using UnityEngine;

namespace DataBase.Shop
{
    public interface IUpgradesBase
    {
        Upgrade Get(EUpgradeType type);
        IEnumerable<Upgrade> GetAllUpgrades { get; }
        void Set(EUpgradeType type, Upgrade upg);
    }
}