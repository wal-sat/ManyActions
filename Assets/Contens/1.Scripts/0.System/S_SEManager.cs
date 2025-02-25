using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

[System.Serializable]
public class SEInfo
{
    public string name;
    public AudioClip audioClip;
}

public class S_SEManager : Singleton<S_SEManager>
{
    [SerializeField] float MAX_VOLUME;
    [SerializeField] List<SEInfo> SEList = new List<SEInfo>();

    private AudioSource[] _audioSourceList = new AudioSource[20];
    private Dictionary<string, SEInfo> _soundDictionary = new Dictionary<string, SEInfo>();
    
    private float _volume;
 
    public override void Awake()
    {
        base.Awake();

        for (var i = 0; i < _audioSourceList.Length; ++i)
        {
            _audioSourceList[i] = gameObject.AddComponent<AudioSource>();
            _audioSourceList[i].priority = 128;
        }
 
        foreach (var SEInfo in SEList)
        {
            _soundDictionary.Add(SEInfo.name, SEInfo);
        }
    }

    public void Play(string name)
    {
        if (_soundDictionary.TryGetValue(name, out var SEInfo))
        {
            var audioSource = GetUnusedAudioSource();
            if (audioSource == null) return; //再生できませんでした
            audioSource.PlayOneShot(SEInfo.audioClip);
        }
    }
    public void ChangeVolume(float volume)
    {
        _volume = volume * MAX_VOLUME;
        foreach (var audioSource in _audioSourceList)
        {
            audioSource.volume = _volume;
        }
    }

    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < _audioSourceList.Length; ++i)
        {
            if (_audioSourceList[i].isPlaying == false) return _audioSourceList[i];
        }

        Debug.Log("audioSourceがない");
        return null;
    }
}
