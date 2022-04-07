namespace Signals
{
    public struct SignalMakeHudButtonsVisible
    {
        public readonly bool Visible;

        public SignalMakeHudButtonsVisible(bool visible)
        {
            Visible = visible;
        }
    }
}