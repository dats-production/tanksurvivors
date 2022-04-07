using SimpleUi;

namespace Scripts.Runtime.Game.Ui.Windows.GameOver 
{
    public class GameOverWindow : WindowBase 
    {
        public override string Name => "GameOver";
        protected override void AddControllers()
        {
            AddController<GameOverViewController>();
        }
    }
}