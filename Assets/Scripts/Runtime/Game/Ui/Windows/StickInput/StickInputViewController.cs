using Signals;
using SimpleUi.Abstracts;
using UniRx;
using Zenject;

namespace Scripts.Runtime.Game.Ui.Windows.StickInput 
{
    public class StickInputViewController : UiController<StickInputView>
    {
        private readonly SignalBus _signalBus;
    
        public StickInputViewController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
    }
}