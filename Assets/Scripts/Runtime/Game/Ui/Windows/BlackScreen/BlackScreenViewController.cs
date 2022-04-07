using System;
using DG.Tweening;
using Game.SceneLoading;
using Plugins.PdUtils.Runtime.PdAudio;
using Signals;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using Zenject;

namespace Game.Ui.BlackScreen
{
    public class BlackScreenViewController : UiController<BlackScreenView>, IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly PdAudio _pdAudio;

        public BlackScreenViewController(SignalBus signalBus, PdAudio pdAudio)
        {
            _signalBus = signalBus;
            _pdAudio = pdAudio;
        }
        
        public void Initialize()
        {
            const float duration = 0.2f;
            _signalBus.Subscribe<SignalBlackScreen>(x =>
            {
                View.Show(() =>
                {
                    OnComplete(x.IsShow, x.Complete);
                }, x.IsShow, (x.Duration == 0 ? duration : x.Duration), x.ChangeColor);
                //DOVirtual.Float(x.IsShow ? 0 : 1, x.IsShow ? 1 : 0, duration, _pdAudio.SetMusicVolume);
            });
        }

        private void OnComplete(bool isShow, Action complete)
        {
            if (!isShow)
            {
                _signalBus.BackWindow(EWindowLayer.Project);
            }
            complete?.Invoke();
        }
    }
}