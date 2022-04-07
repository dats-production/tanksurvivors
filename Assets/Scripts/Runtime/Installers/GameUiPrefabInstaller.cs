using Game.Ui.InGameMenu;
using Runtime.Game.Ui.Windows.ConsentPopUp;
using Runtime.Game.Ui.Windows.FocusSpace;
using Runtime.Game.Ui.Windows.TouchPad;
using Runtime.Installers;
using Runtime.UI.QuitConcentPopUp;
using Scripts.Runtime.Game.Ui.Windows.ExperienceUI;
using Scripts.Runtime.Game.Ui.Windows.FpsCounter;
using Scripts.Runtime.Game.Ui.Windows.GameOver;
using Scripts.Runtime.Game.Ui.Windows.LevelUp;
using Scripts.Runtime.Game.Ui.Windows.Stats;
using Scripts.Runtime.Game.Ui.Windows.StickInput;
using SimpleUi;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/GameUiPrefabInstaller", fileName = "GameUiPrefabInstaller")]
    public class GameUiPrefabInstaller : ScriptableObjectInstaller
    {
        [FormerlySerializedAs("Canvas"), SerializeField]
        private Canvas canvas;

        [SerializeField] private InGameMenuView inGameMenu;
        [SerializeField] private FocusView focusView;
        [SerializeField] private ConsentPopUpTarget consentPopUpTarget;
        [SerializeField] private TouchpadView touchpadView;
        [SerializeField] private StickInputView stickInputView;
        [SerializeField] private FpsCounterView fpsCounterView;
        [SerializeField] private ExperienceUIView experienceUIView;
        [SerializeField] private LevelUpView levelUpView;
        [SerializeField] private StatsView statsView;
        [SerializeField] private GameOverView gameOverView;

        public override void InstallBindings()
        {
            var canvasObj = Instantiate(canvas);
            var canvasTransform = canvasObj.transform;
            var camera = canvasTransform.GetComponentInChildren<Camera>();
            camera.clearFlags = CameraClearFlags.Depth;
            camera.orthographic = false;
            camera.transform.SetParent(null);

            Container.Bind<Canvas>().FromInstance(canvasObj).AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(camera).AsSingle().WithConcreteId(ECameraType.GameCamera).NonLazy();

            Container.BindUiView<InGameMenuViewController, InGameMenuView>(inGameMenu, canvasTransform);
            Container.BindUiView<FocusViewController, FocusView>(focusView, null);
            Container.BindUiView<ConsentPopUpViewController, ConsentPopUpTarget>(consentPopUpTarget, canvasTransform);
            Container.BindUiView<TouchpadViewController, TouchpadView>(touchpadView, canvasTransform);
            Container.BindUiView<StickInputViewController, StickInputView>(stickInputView, null);
            Container.BindUiView<FpsCounterViewController, FpsCounterView>(fpsCounterView, canvasTransform);
            Container.BindUiView<ExperienceUIViewController, ExperienceUIView>(experienceUIView, canvasTransform);
            Container.BindUiView<LevelUpViewController, LevelUpView>(levelUpView, canvasTransform);
            Container.BindUiView<StatsViewController, StatsView>(statsView, canvasTransform);
            Container.BindUiView<GameOverViewController, GameOverView>(gameOverView, canvasTransform);
        }
    }
}