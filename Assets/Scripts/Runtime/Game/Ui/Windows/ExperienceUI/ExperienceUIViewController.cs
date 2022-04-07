using Signals;
using SimpleUi.Abstracts;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts.Runtime.Game.Ui.Windows.ExperienceUI 
{
    public class ExperienceUIViewController : UiController<ExperienceUIView>, IInitializable
    {
        private readonly SignalBus _signalBus;
        private float _maxExp;
        public ExperienceUIViewController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.GetStream<SignalExperience>().Subscribe(x => OnUpdateExp(x.CurExp, x.MaxExp)).AddTo(View);
            _signalBus.GetStream<SignalNewLevel>().Subscribe(x => OnUpdateLevel(x.Value)).AddTo(View);
        }

        private void OnUpdateExp(float curExp, float maxExp)
        {
            View.slider.value = curExp / maxExp;
            _maxExp = maxExp;
        }
        private void OnUpdateLevel(int level)
        {
            View.level.text = "Level " + level;
        }        
    }
}