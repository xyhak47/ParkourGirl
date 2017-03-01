using UnityEngine;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
    [System.Serializable]
    public class Music
    {
        public AudioClip audioClip;
        public string MusicName;
    }

    //public static MusicPlayer Instance;
    //MusicPlayer()
    //{
    //    Instance = this;
    //}

    public List<Music> List_Music;
    private AudioSource SelfAudioSource;
    private float BackGroundMusicVolume;

    void Start()
    {
        SelfAudioSource = GetComponent<AudioSource>();
        PlayMusicBg(Config.BgM3);
    }

    public void PlayMusic(string MusicName)
    {
        AudioSource.PlayClipAtPoint(List_Music.Find(it=>it.MusicName == MusicName).audioClip, Camera.main.transform.position, BackGroundMusicVolume * 2);
    }

    void PlayMusicBg(string MusicName)
    {
        SelfAudioSource.clip = List_Music.Find(it => it.MusicName == MusicName).audioClip;
        SelfAudioSource.loop = true;
        SelfAudioSource.Play();
        BackGroundMusicVolume = SelfAudioSource.volume;
    }
}
