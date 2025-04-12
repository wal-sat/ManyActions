using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AmbientSoundManager : Singleton<S_AmbientSoundManager>
{
    [System.Serializable] class AmbientSoundInfo
    {
        public string name;
        public AudioClip audioClip;
    }

    [SerializeField] float MAX_VOLUME;
    [SerializeField] List<AmbientSoundInfo> AmbientSoundList = new List<AmbientSoundInfo>();

    private AudioSource[] _audioSourceList = new AudioSource[3];
    private Dictionary<string, AmbientSoundInfo> _soundDictionary = new Dictionary<string, AmbientSoundInfo>();

    private float _volume;

    public override void Awake()
    {
        base.Awake();

        for (var i = 0; i < _audioSourceList.Length; ++i)
        {
            _audioSourceList[i] = gameObject.AddComponent<AudioSource>();
            _audioSourceList[i].priority = 1;
        }
 
        foreach (var AmbientSoundInfo in AmbientSoundList)
        {
            _soundDictionary.Add(AmbientSoundInfo.name, AmbientSoundInfo);
        }
    }

    public void Play(string name, float fadeTime)
    {
        string playingAmbientSound = GetPlayingAmbientSound();
        if (playingAmbientSound == name) return;
        if (playingAmbientSound != null) Stop(playingAmbientSound, fadeTime);

        if (_soundDictionary.TryGetValue(name, out var ambientSoundInfo))
        {
            var audioSource = GetUnusedAudioSource();
            if (audioSource == null) return; //再生できませんでした
            audioSource.clip = ambientSoundInfo.audioClip;
            audioSource.loop = true;
            StartCoroutine( CPlay( audioSource, fadeTime) );
        }
    }
    IEnumerator CPlay(AudioSource audioSource, float fadeTime)
    {
        audioSource.volume = 0;
        audioSource.Play();

        for (int i = 0; i < 100; i++)
        {
            audioSource.volume += _volume / 100;
            if (audioSource.volume > _volume) audioSource.volume = _volume;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        audioSource.volume = _volume;
    }
    public void Stop(string name, float fadeTime)
    {
        foreach (AudioSource audioSource in _audioSourceList)
        {
            if ( audioSource.clip == _soundDictionary[name].audioClip)
            {
                StartCoroutine( CStop( audioSource, fadeTime) );
            }
        }
    }
    IEnumerator CStop(AudioSource audioSource, float fadeTime)
    {
        float initVolume = audioSource.volume;
        for (int i = 0; i < 100; i++)
        {
            audioSource.volume -= initVolume / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        audioSource.volume = 0;

        audioSource.Stop();
    }
    public void Pause(string name, float fadeTime)
    {
        foreach (AudioSource audioSource in _audioSourceList)
        {
            if ( audioSource.clip == _soundDictionary[name].audioClip)
            {
                StartCoroutine( CPause( audioSource, fadeTime) );
            }
        }
    }
    IEnumerator CPause(AudioSource audioSource, float fadeTime)
    {
        float initVolume = audioSource.volume;
        for (int i = 0; i < 100; i++)
        {
            audioSource.volume -= initVolume / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        audioSource.volume = 0;

        audioSource.Pause();
    }
    public void UnPause(string name, float fadeTime)
    {
        foreach (var audioSource in _audioSourceList)
        {
            if ( audioSource.clip == _soundDictionary[name].audioClip)
            {
                StartCoroutine(CUnPause(audioSource, fadeTime));
            }
        }
    }
    IEnumerator CUnPause(AudioSource audioSource, float fadeTime)
    {
        audioSource.UnPause();

        for (int i = 0; i < 100; i++)
        {
            audioSource.volume += _volume / 100;
            if (audioSource.volume > _volume) audioSource.volume = _volume;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        audioSource.volume = _volume;
    }
    public void ChangeVolume(float volume)
    {
        _volume = volume * MAX_VOLUME;
        foreach (var audioSource in _audioSourceList)
        {
            audioSource.volume = _volume;
        }
    }
    public string GetPlayingAmbientSound()
    {
        foreach (var audioSource in _audioSourceList)
        {
            if (!audioSource.isPlaying) continue;
            foreach (var sound in _soundDictionary)
            {
                if ( audioSource.clip == sound.Value.audioClip )
                {
                    return sound.Value.name;
                }
            }
        }
        return null;
    }

    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < _audioSourceList.Length; ++i)
        {
            if (_audioSourceList[i].isPlaying == false) return _audioSourceList[i];
        }
 
        return null;
    }
}
