using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Services;

namespace Helper
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHelper : MonoBehaviour
    {
        [SerializeField] private AudioType type;
        public enum AudioType
        {
            sfx,
            music
        }

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            switch (type)
            {
                case AudioType.sfx:
                 //   audioSource.mute = LocalData.AudioSfx();
                    break;

                case AudioType.music:
                  //  audioSource.mute = LocalData.AudioMusic();
                    break;
            }        
        }
    }

}
