using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Services
{
    public class LocalData 
    {
        private static string audioSfx = "audio_sxf";
        private static string audioMusic = "audio_music";

        public static bool AudioSfx()
        {
            string val = PlayerPrefs.GetString(audioSfx);

            if (val != "true" && val != "false") {
                SetAudioSfx(true);
                return true;
            }
            else return val == "true";
        }

        public static void SetAudioSfx(bool value)
        {
            PlayerPrefs.SetString(audioSfx, (value)? "true":"false");
        }

        public static bool AudioMusic()
        {
            string val = PlayerPrefs.GetString(audioMusic);

            if (val != "true" && val != "false") {
                SetAudioMusic(true);
                return true;
            }
            else return val == "true";
        }

        public static void SetAudioMusic(bool value)
        {
            PlayerPrefs.SetString(audioMusic, (value) ? "true" : "false");
        }
    }

}
