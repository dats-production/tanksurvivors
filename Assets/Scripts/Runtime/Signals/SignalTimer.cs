using DataBase.Timer;
using System;

namespace Signals
{
    public class SignalTimer
    {
        public Timer Value;
        
        public SignalTimer(Timer value)
        {
            Value = value;
        }
    }
}