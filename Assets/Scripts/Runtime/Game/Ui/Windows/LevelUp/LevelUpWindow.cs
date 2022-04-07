using SimpleUi;

namespace Scripts.Runtime.Game.Ui.Windows.LevelUp 
{
    public class LevelUpWindow : WindowBase 
    {
        public override string Name => "LevelUp";
        protected override void AddControllers()
        {
            AddController<LevelUpViewController>();
        }
    }
}