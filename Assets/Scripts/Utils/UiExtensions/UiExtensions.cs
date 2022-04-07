using System;
using Runtime.UI.QuitConcentPopUp;
using Signals;
using SimpleUi.Signals;
using Zenject;

namespace Utils.UiExtensions
{
    public static partial class UiExtensions
    {
        public static void OpenQuestionPopUp(this SignalBus signalBus, string title, Action<bool> action)
        {
            signalBus.Fire(new SignalQuestionChoice(title, action));
            signalBus.OpenWindow<ConsentWindow>();
        }
    }
}