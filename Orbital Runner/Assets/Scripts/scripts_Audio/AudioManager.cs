using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
namespace AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Dependencies")]
        [SerializeField]
        private SFXHandler sfxHandler;
        [Tooltip("A reference to which Audio Mixer the source is outputting towards. ")]
        [SerializeField]
        private AudioMixer audioMixer;
        [Tooltip("The name of the exposed parameter representing volume in the Master Tracker of the Mixer")]
        [SerializeField]
        private string globalTrackVolumeParameterName = "masterVolume";
        [Tooltip("The name of the exposed parameter representing volume in the Music Tracker of the Mixer")]
        [SerializeField]
        private string musicTrackVolumeParameterName = "musicVolume";
        [Tooltip("The name of the exposed parameter representing volume in the Music Tracker of the Mixer")]
        [SerializeField]
        private string musicTrackPitchParameterName = "musicPitch";
        [Header("Fading Properties")]
        [SerializeField]
        private float fadeOutDuration;
        [SerializeField]
        private float fadeInDuration;
        private AudioSource audioSource;
        [Header("Current Music Track")]
        [SerializeField]
        private AudioTrack currMusicTrack;
        [Header("SFX Properties")]
        [SerializeField]
        private Vector2 sfxDefaultPitchRandomRange = new(0.9f, 1.1f);
        [SerializeField]
        private bool playOnAwake = false;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }
        void Start()
        {
            sfxHandler.AudioMixer = this.audioMixer;
        }
        void OnEnable()
        {
            if (playOnAwake)
            {
                this.PlayMusicTrack(currMusicTrack, true);
            }
        }
        void Update()
        {
            audioMixer.GetFloat(musicTrackPitchParameterName, out float pitch);
        }
        public void SetGlobalVolume(float _preferedVolume)
        {
            AudioMixerUtils.SetVolume(audioMixer, globalTrackVolumeParameterName, _preferedVolume);
        }
        /// <summary>
        /// Fades out or completely stops current playing audio clip. 
        /// </summary>
        /// <param name="fadeMusicTrack"> Will it fade out the AudioTrack?</param>
        public void StopCurrentMusicTrack(bool fadeMusicTrack)
        {
            if (!fadeMusicTrack)
            {
                audioSource.Stop();
                return;
            }
            StartCoroutine(AudioMixerUtils.StartFade(audioMixer, musicTrackVolumeParameterName, fadeOutDuration, 0.0001f));
        }
        /// <summary>
        /// Plays a new audio clip with the option of fading it in. If a clip is currently playing, it will fade out or stop
        /// according to preference.
        /// </summary>
        /// <param name="AudioTrack"> Which clip it will play. </param>
        /// <param name="fadeMusicTrack"> Will it fade in the audio? </param>
        public void PlayMusicTrack(AudioTrack AudioTrack, bool fadeMusicTrack)
        {
            float vol = PlayerSettings.MusicVolume;
            if (!fadeMusicTrack)
            {
                AudioMixerUtils.SetVolume(audioMixer, musicTrackVolumeParameterName,
                    PlayerSettings.MusicVolume);
                PlayMusicTrack(AudioTrack);
                return;
            }
            if (!audioSource.isPlaying)
            {
                //Sets volume to the minumum, then fades in to the prefered volume.
                AudioMixerUtils.SetVolume(audioMixer, musicTrackVolumeParameterName, 0);
                PlayMusicTrack(AudioTrack);
                StartCoroutine(AudioMixerUtils.StartFade(audioMixer, musicTrackVolumeParameterName,
                    fadeInDuration, vol));
            }
            else
            {
                //Fades out previous track by setting its volume to 0, then fades in the new track.
                StartCoroutine(AudioMixerUtils.StartFade(audioMixer, musicTrackVolumeParameterName,
                    fadeOutDuration, 0, AudioTrack, (paramClip) =>
                    {
                        PlayMusicTrack(paramClip);
                        StartCoroutine(AudioMixerUtils.StartFade(audioMixer, musicTrackVolumeParameterName
                            , fadeInDuration, vol));
                    }));
            }
            this.currMusicTrack = AudioTrack;
        }
        /// <summary>
        /// Play a SFX Audio Clip with its original pitch.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="pan"></param>
        public void PlaySFX(AudioClip clip, float pan = 0)
        {
            if (clip != null)
            {
                sfxHandler.PlaySFX(clip, PlayerSettings.SfxVolume, pan);
            }
        }
        /// <summary>
        /// Play a SFX Audio Clip with a custom pitch range. 
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="pan"></param>
        public void PlaySFX(AudioClip clip, Vector2 customPitchRange, float pan = 0)
        {
            if (clip != null)
            {
                sfxHandler.PlaySFX(clip, PlayerSettings.SfxVolume, customPitchRange, pan);
            }
        }
        /// <summary>
        /// Play a SFX Audio Clip with an exact pitch.
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="pan"></param>
        public void PlaySFX(AudioClip clip, float exactPitch, float pan = 0)
        {
            if (clip != null)
            {
                sfxHandler.PlaySFX(clip, PlayerSettings.SfxVolume, exactPitch, pan);
            }
        }
        /// <summary>
        /// Play a SFX Audio Clip with the default
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="pan"></param>
        public void PlaySFX(AudioClip clip, bool useDefaultSFXPitchRange, float pan = 0)
        {
            if (clip != null)
            {
                if (useDefaultSFXPitchRange)
                    sfxHandler.PlaySFX(clip, PlayerSettings.SfxVolume, this.sfxDefaultPitchRandomRange, pan);
                else
                    sfxHandler.PlaySFX(clip, PlayerSettings.SfxVolume, pan);
            }
        }
        public void PlayLoopableSFX(AudioClip clip)
        {
            sfxHandler.PlayLoopingSFXTrack(clip, true);
        }
        public void StopLoopableSFX()
        {
            sfxHandler.StopCurrentSFXTrack(true);
        }
        public void ChangeSFXPitchManually(float newPitch)
        {
            sfxHandler.ChangePitchManual(newPitch);
        }
        public void ChangeVolumeOfMixerGroup(string mixerGroupVolParam, float newVolume)
        {
            AudioMixerUtils.SetVolume(audioMixer, mixerGroupVolParam, newVolume);
        }

        private void PlayMusicTrack(AudioTrack AudioTrack)
        {
            audioSource.Stop();
            audioSource.clip = AudioTrack.AudioClip;
            audioSource.loop = AudioTrack.IsLoopable;
            audioSource.Play();
            audioMixer.SetFloat(musicTrackPitchParameterName, AudioTrack.Pitch);
            audioMixer.GetFloat(musicTrackPitchParameterName, out float pitch);
        }
    }
}
