using System;
using UniRx;
using CustomSelectables;
using UnityEngine.UI;

namespace Utils.UiExtensions
{
    public static class CustomUIComponentExtensions
    {
        /// <summary>Observe onClick event.</summary>
        public static IObservable<Unit> OnClickAsObservable(this CustomButton button)
        {
            return button.onClick.AsObservable();
        }
        
        /// <summary>Observe onValueChanged with current `isOn` value on subscribe.</summary>
        public static IObservable<bool> OnValueChangedAsObservable(this CustomToggle toggle)
        {
            // Optimized Defer + StartWith
            return Observable.CreateWithState<bool, CustomToggle>(toggle, (t, observer) =>
            {
                observer.OnNext(t.isOn);
                return t.onValueChanged.AsObservable().Subscribe(observer);
            });
        }
    }
}