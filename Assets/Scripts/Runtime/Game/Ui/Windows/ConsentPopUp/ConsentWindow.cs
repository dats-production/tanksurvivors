using SimpleUi;
using SimpleUi.Interfaces;
using UnityEngine.UI;

namespace Runtime.UI.QuitConcentPopUp
{
    public class ConsentWindow : WindowBase, IPopUp
    {
        public override string Name => "ConsentPopUp";

        protected override void AddControllers()
        {
            AddController<ConsentPopUpViewController>();
        }
    }
}