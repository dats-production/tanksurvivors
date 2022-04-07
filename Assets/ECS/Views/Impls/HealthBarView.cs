using System.Diagnostics;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using ECS.Views;
using Ecs.Views.Linkable.Impl;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.Ui
{
    public class HealthBarView : LinkableView
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Image image;
        [SerializeField] private Color green;
        [SerializeField] private Color yellow;
        [SerializeField] private Color red;

        public void SetPosition(Vector3 player)
        {
            transform.position = player + offset;
        }
        public void UpdateHealth(float CurHealth, float MaxHealth)
        {
            var ratio = CurHealth / MaxHealth;
            slider.value = ratio;
            if (ratio < 0.33f)
                image.color = red;
            else if (ratio > 0.77)
                image.color = green;
            else
                image.color = yellow;
        }
    }
}
