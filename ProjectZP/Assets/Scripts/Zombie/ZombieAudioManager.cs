using UnityEngine;

namespace ZP.BHS.Zombie
{
    class ZombieAudioManager : MonoBehaviour
    {
        [SerializeField] AudioClip _audioClipIdle;
        [SerializeField] AudioClip _audioClipAngry;
        [SerializeField] AudioClip _audioClipSearch;
        [SerializeField] AudioClip _audioClipAttack;
        [SerializeField] AudioClip _audioClipDamage;
        [SerializeField] private AudioSource _audioSource;

        public AudioClip AudioClipIdle => _audioClipIdle;
        public AudioClip AudioClipAngry => _audioClipAngry;
        public AudioClip AudioClipSearch => _audioClipSearch;
        public AudioClip AudioClipAttack => _audioClipAttack;
        public AudioClip AudioClipDamage => _audioClipDamage;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Input Audio Clip To Use.
        /// </summary>
        /// <param name="audioClip">Audio Clip To Use.</param>
        public void PlayAudioClip(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}
