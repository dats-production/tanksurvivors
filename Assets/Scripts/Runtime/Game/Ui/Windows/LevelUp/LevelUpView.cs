using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Runtime.Game.Ui.Windows.LevelUp 
{
    public class LevelUpView : UiView
    {
        public TMP_Text level;
        public UpgradeView upgPrefab;
        public RectTransform upgContainer;
    }
}