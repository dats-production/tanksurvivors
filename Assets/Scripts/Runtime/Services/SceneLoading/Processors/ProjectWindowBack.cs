using System;
using Game.Ui.BlackScreen;
using PdUtils.SceneLoadingProcessor.Impls;
using Signals;
using SimpleUi.Signals;
using UniRx;
using Zenject;

namespace Runtime.Services.SceneLoading.Processors
{
    public class ProjectWindowBack : Process
    {
        private readonly SignalBus _signalBus;
        private readonly bool _isShow;

        public ProjectWindowBack(SignalBus signalBus, bool isShow = false)
        {
            _signalBus = signalBus;
            _isShow = isShow;
            if(isShow)
                _signalBus.OpenWindow<BlackScreenWindow>(EWindowLayer.Project);
        }

        public override void Do(Action onComplete)
        {
            _signalBus.Fire(new SignalBlackScreen(_isShow, () => Complete(onComplete), 1f));
			
            if (!_isShow)
            {
                Observable.NextFrame().Subscribe(_ => onComplete());
            }
        }

        private void Complete(Action onComplete)
        {
            if (_isShow)
            {
                onComplete();
            }
        }
    }
}