using UnityEngine;
namespace AudioSystem
{
    [CreateAssetMenu(menuName = "Audio/Audio Track", fileName = "New Audio Track")]
    public class AudioTrack : ScriptableObject
    {
        [Tooltip("Name of the track. ")]
        [SerializeField]
        private string musicName;
        [Tooltip("Reference to the audio clip. ")]
        [SerializeField]
        private AudioClip audioClip;
        [Tooltip("Will the track loop? ")]
        [SerializeField]
        private bool isLoopable;
        [SerializeField]
        [Range(0.0f, 10.0f)]
        private float audioPitch = 1.0f;


        public AudioClip AudioClip { get { return audioClip; } private set { audioClip = value; } }
        public bool IsLoopable { get { return isLoopable; } private set { isLoopable = value; } }
        public float Pitch => audioPitch;
    }
}

