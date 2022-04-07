using PdUtils.Dao;

namespace Runtime.Services.PlayerSettings
{
    public class PlayerSettingsService : IPlayerSettingsService
    {
        private readonly IDao<PlayerSettings> _dao;

        public PlayerSettingsService(IDao<PlayerSettings> dao)
        {
            _dao = dao;
        }
        
        private PlayerSettings _playerSettings;

        public PlayerSettings PlayerSettings
        {
            get
            {
                _playerSettings = _dao.Load();
                return _playerSettings;
            }
        }

        public void SaveSettings(PlayerSettings settings)
        {
            _playerSettings = settings;
            _dao.Save(settings);
        }
    }
}