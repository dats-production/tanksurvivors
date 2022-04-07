using System;
namespace Runtime.DataBase.General.CommonParamsBase
{
    public interface ICommonParamsBase
    {
        
    }

    [Serializable]
    public struct DepressionPercentStep
    { 
        public int maxStepsCount;
        public int stepPercent;
    }

    [Serializable]
    public struct GameParams
    {
        
    }
}