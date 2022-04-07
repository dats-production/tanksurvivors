using Signals;
using SimpleUi.Abstracts;
using SimpleUi.Interfaces;
using UniRx;
using Zenject;

namespace Scripts.Runtime.Game.Ui.Windows.FpsCounter 
{
    public class FpsCounterViewController : UiController<FpsCounterView>, IInitializable
    {
        private readonly SignalBus _signalBus;
    
        public FpsCounterViewController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.GetStream<SignalFPS>().Subscribe(x => OnFPS(x.Value)).AddTo(View);
        }

        private void OnFPS(float value)
        {
            //View.fps.text = value.ToString("#");
        }
    }
}