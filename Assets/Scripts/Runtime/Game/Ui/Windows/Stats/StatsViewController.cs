using DataBase.Timer;
using Signals;
using SimpleUi.Abstracts;
using UniRx;
using Zenject;

namespace Scripts.Runtime.Game.Ui.Windows.Stats 
{
    public class StatsViewController : UiController<StatsView> , IInitializable
    {
        private readonly SignalBus _signalBus;
    
        public StatsViewController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.GetStream<SignalEnemyKilled>().Subscribe(x => OnEnemyKilled(x.Value)).AddTo(View);
            _signalBus.GetStream<SignalTimer>().Subscribe(x => OnTimer(x.Value)).AddTo(View);
        }

        private void OnTimer(Timer value)
        {
            View.timer.text = value.ToString();
        }

        private void OnEnemyKilled(int value)
        {
            View.enemyKilled.text = value.ToString();
        }
    }
}