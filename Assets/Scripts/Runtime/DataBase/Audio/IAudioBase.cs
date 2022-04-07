using UnityEngine;

namespace DataBase.Audio
{
    public interface IAudioBase
    {
        AudioClip Get(string key);
    }
}