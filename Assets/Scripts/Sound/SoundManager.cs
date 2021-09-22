using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
    
    
        private AudioSource _audioSource;
    
        public static SoundManager Inst;

        private void Awake()
        {
            Inst = this;
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudio(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void StopAudio()
        {
            _audioSource.Stop();
        }
    }
}