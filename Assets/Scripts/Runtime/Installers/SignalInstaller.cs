using Signals;
using Zenject;

namespace Installers
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<SignalGameInit>();
            Container.DeclareSignal<SignalScoreOpen>();
            Container.DeclareSignal<SignalMakeHudButtonsVisible>();
            Container.DeclareSignal<SignalBlackScreen>();
            Container.DeclareSignal<SignalQuestionChoice>();
            Container.DeclareSignal<SignalFPS>();
            Container.DeclareSignal<SignalExperience>();
            Container.DeclareSignal<SignalNewLevel>();
            Container.DeclareSignal<SignalEnemyKilled>();
            Container.DeclareSignal<SignalTimer>();
        }
    }
}