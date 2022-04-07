using System;

namespace Signals
{
    public class SignalEnemyKilled
    {
        public int Value;
        
        public SignalEnemyKilled(int value)
        {
            Value = value;
        }
    }
}