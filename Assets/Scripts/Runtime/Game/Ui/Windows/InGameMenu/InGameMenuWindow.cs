using SimpleUi;

namespace Game.Ui.InGameMenu
{
    public class InGameMenuWindow : WindowBase
    {
        public override string Name => "InGameMenu";
        protected override void AddControllers()
        {
            AddController<InGameMenuViewController>();
        }
    }
}