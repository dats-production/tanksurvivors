using Game.Ui.InGameMenu;
using Runtime.Game.Ui.Windows.TouchPad;
using Scripts.Runtime.Game.Ui.Windows.ExperienceUI;
using Scripts.Runtime.Game.Ui.Windows.FpsCounter;
using Scripts.Runtime.Game.Ui.Windows.LevelUp;
using Scripts.Runtime.Game.Ui.Windows.Stats;
using Scripts.Runtime.Game.Ui.Windows.StickInput;
using SimpleUi;

namespace Runtime.Game.Ui
{
    public class GameHudWindow : WindowBase
    {
        public override string Name => nameof(GetType);
        protected override void AddControllers()
        {
            AddController<FpsCounterViewController>();            
            AddController<StatsViewController>();            
            AddController<ExperienceUIViewController>();
            AddController<TouchpadViewController>();
            AddController<StickInputViewController>();
        }
    }
}