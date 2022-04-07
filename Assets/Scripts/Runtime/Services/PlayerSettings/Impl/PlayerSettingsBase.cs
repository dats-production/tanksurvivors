using UnityEngine;

namespace Runtime.Services.PlayerSettings
{
    [CreateAssetMenu(fileName = "PlayerSettingsBase", menuName = "Settings/PlayerSettingsBase", order = 0)]
    public class PlayerSettingsBase : ScriptableObject, IPlayerSettingsBase
    {
        [SerializeField] private PlayerSettings playerSettings;
        public PlayerSettings DefaultSettings()
        {
            
            return playerSettings;
    }
    }
}