using UnityEngine;
using UnityEngine.Audio;
namespace AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SFXHandler : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _sfxSource;
        [SerializeField]
        private AudioSource _rollingSource;
        [SerializeField]
        private string sfxVolumeParameter = "sfxVolume";
        [SerializeField]
        private string sfxPitchParameterName = "sfxPitch";
        private AudioMixer audioMixer;

        public AudioMixer AudioMixer { private get { return audioMixer; } set { audioMixer = value; } }
        private void Awake()
        {
            if (_sfxSource == null)
                _sfxSource = GetComponent<AudioSource>();
        }
        public void StopCurrentSFXTrack(bool fadeAudioTrack)
        {
            if (!fadeAudioTrack)
            {
                _rollingSource.Stop();
                return;
            }
            StartCoroutine(AudioMixerUtils.StartFade(audioMixer, "rollingVolume", 0.05f, 0.0001f));
        }
        public void PlayLoopingSFXTrack(AudioClip AudioClip, bool fadeMusicTrack)
        {
            float vol = PlayerSettings.SfxVolume;
            if (!fadeMusicTrack)
            {
                AudioMixerUtils.SetVolume(audioMixer, "rollingVolume",
                    PlayerSettings.SfxVolume);
                _rollingSource.clip = AudioClip;
                _rollingSource.loop = true;
                _rollingSource.Play();
                return;
            }
            if (!_rollingSource.isPlaying)
            {
                //Sets volume to the minumum, then fades in to the prefered volume.
                AudioMixerUtils.SetVolume(audioMixer, "rollingVolume", 0);
                _rollingSource.clip = AudioClip;
                _rollingSource.loop = true;
                _rollingSource.Play();
                StartCoroutine(AudioMixerUtils.StartFade(audioMixer, "rollingVolume",
                    0.05f, vol));
            }
            else
            {
                //Fades out previous track by setting its volume to 0, then fades in the new track.
                StartCoroutine(AudioMixerUtils.StartFade(audioMixer, "rollingVolume",
                    0.05f, 0, AudioClip, (paramClip) =>
                    {
                        _rollingSource.clip = AudioClip;
                        _rollingSource.loop = true;
                        _rollingSource.Play();
                        StartCoroutine(AudioMixerUtils.StartFade(audioMixer, "rollingVolume"
                            , 0.05f, vol));
                    }));
            }
        }
        /// <summary>
        /// Plays an SFX with no change to its original pitch.
        /// </summary>
        /// <param name="clip">Plays the clip One-Shot.</param>
        /// <param name="volume"></param>
        /// <param name="pan"></param>
        public void PlaySFX(AudioClip clip, float volume, float pan = 0)
        {
            if (IsAudioMixerDataValid() == false) return;
            audioMixer.GetFloat(sfxPitchParameterName, out float currPitch);
            audioMixer.ClearFloat(sfxPitchParameterName);
            _sfxSource.panStereo = pan;
            _sfxSource.PlayOneShot(clip, volume);
        }
        /// <summary>
        /// Plays an SFX with an exact pitch in mind
        /// </summary>
        /// <param name="clip">Plays the clip One-Shot.</param>
        /// <param name="volume"></param>
        /// <param name="pan"></param>
        public void PlaySFX(AudioClip clip, float volume, float exactPitch, float pan = 0)
        {
            if (IsAudioMixerDataValid() == false) return;
            if (exactPitch <= 0 || exactPitch > 10)
            {
                Debug.LogError("Invalid Exact Pitch for SFX when PlaySFX is called.  - SFXHandler");
                return;
            }
            audioMixer.SetFloat(sfxPitchParameterName, exactPitch);
            _sfxSource.panStereo = pan;
            _sfxSource.PlayOneShot(clip, volume);
        }
        /// <summary>
        /// Plays an SFX with a randomized pitch based on a given Vector2 range.
        /// </summary>
        /// <param name="clip">Plays the clip One-Shot. </param>
        /// <param name="volume">.</param>
        /// <param name="pitchRandomRange">X variable represents the lower boundary. Y represents the higher boundary</param>
        /// <param name="pan"></param>
        public void PlaySFX(AudioClip clip, float volume, Vector2 pitchRandomRange, float pan = 0)
        {
            if (pitchRandomRange.x <= 0 || pitchRandomRange.x > pitchRandomRange.y || pitchRandomRange.x > 10)
            {
                Debug.LogError("Invalid Random Range Pitch for SFX when PlaySFX is called.  - SFXHandler");
                return;
            }
            if (pitchRandomRange.y <= 0 || pitchRandomRange.y <= pitchRandomRange.x || pitchRandomRange.y > 10)
            {
                Debug.LogError("Invalid  Random Range Pitch for SFX when PlaySFX is called - SFXHandler");
                return;
            }
            if (IsAudioMixerDataValid() == false) return;
            float randPitch = Random.Range(pitchRandomRange.x, pitchRandomRange.y);
            _sfxSource.panStereo = pan;
            // _sfxSource.pitch = randPitch;  //-- Using the AudioMixer Pitch Parameter instead. 
            audioMixer.SetFloat(sfxPitchParameterName, randPitch);
            _sfxSource.PlayOneShot(clip, volume);
        }
        public void ChangePitchManual(float newPitch)
        {
            audioMixer.SetFloat("rollingPitch", newPitch);
        }
        private bool IsAudioMixerDataValid()
        {
            if (audioMixer == null)
            {
                Debug.LogError("Audio Mixer hasn't been assigned to SFXHandler. Make sure to link SFXHandler to an AudioManager - SFXHandler");
                return false;
            }
            if (sfxPitchParameterName == "")
            {
                Debug.LogError("Invalid Exposed Pitch Parameter Name from AudioMixer in SFXHandler");
                return false;
            }
            return true;
        }
    }
}