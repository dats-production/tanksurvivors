using System;
using DG.Tweening;
using SimpleUi.Abstracts;
using SimpleUi.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.BlackScreen
{
    public class BlackScreenView : UiView
    {
        [SerializeField] private Image image;

        private Color _defaultColor = Color.black;

        public void Show(Action complete, bool isShow, float duration, Color color)
        {
            if (color != default && isShow)
            {
                image.color = color;
            }
            
            image.DOFade(isShow ? 0 : 1, 0);
            image.DOFade(isShow ? 1 : 0, duration)
                .OnComplete(() =>
                {
                    if (!isShow)
                    {
                        image.color = _defaultColor;
                    }
                    complete?.Invoke();
                });
        }
    }
}