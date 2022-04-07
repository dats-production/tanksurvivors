using System;
using CustomSelectables;
using DataBase.Shop.Impl;
using SimpleUi.Abstracts;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils.UiExtensions;

namespace Scripts.Runtime.Game.Ui.Windows.LevelUp 
{
    public class UpgradeView : UiView 
    {
        public EUpgradeType type;
        public TMP_Text name;
        public TMP_Text description;
        public Transform levelTr;
        public TMP_Text level;
        public Image image;
        public Button choose;

        public void Initialise(Upgrade upg, Action<EUpgradeType> onChoose)
        {
            choose.OnClickAsObservable().Subscribe(x => onChoose(type)).AddTo(choose);
            
            name.text = upg.name;
            type = upg.type;
            description.text = upg.levels[upg.unlockedLevel].description;
            image.sprite = upg.icon;
            
            SetUpgradeLevelText(upg);
        }


        public void SetUpgradeLevelText(Upgrade upg)
        {
            if(upg.unlockedLevel == 0)
            {
                levelTr.GetChild(0).gameObject.SetActive(true);
                description.text = upg.levels[upg.unlockedLevel].description;
            }
            else if (upg.unlockedLevel > 0 && upg.unlockedLevel < upg.levels.Length)
            {
                levelTr.GetChild(0).gameObject.SetActive(false);
                levelTr.GetChild(1).gameObject.SetActive(true);
                var correctLevel = upg.unlockedLevel+1;
                level.text = "Level " + correctLevel;
                description.text = upg.levels[upg.unlockedLevel].description;
            }
            else if(upg.isMaxLevel)
            {
                description.text = null;
                choose.interactable = false;
                levelTr.GetChild(0).gameObject.SetActive(false);
                levelTr.GetChild(1).gameObject.SetActive(false);
                levelTr.GetChild(2).gameObject.SetActive(true);
            }

        }
    }
}