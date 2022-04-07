using UnityEngine;

namespace Services.FX
{
    public interface IFxPoolService
    {
        ParticleSystem Get(string key);
    }
}