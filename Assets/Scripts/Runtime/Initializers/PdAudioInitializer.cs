using PdUtils.PdAudio;
using Plugins.PdUtils.Runtime.PdAudio;
using Zenject;

namespace Game.Utils
{
    public class PdAudioInitializer : IInitializable
    {
        private readonly PdAudio _pdAudio;
        

        public PdAudioInitializer(PdAudio pdAudio)
        {
            _pdAudio = pdAudio;
        }

        public void Initialize()
        {
            _pdAudio.MusicEnabled = true;
            _pdAudio.SoundFxEnabled = true;
        }
    }
}