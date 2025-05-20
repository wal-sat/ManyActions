using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BGMManager : Singleton<S_BGMManager>
{
    enum BGMStatus {  none, fadeIn, play, fadeOut, pause, stop }

    [Serializable] class BGMInfo
    {
        [SerializeField] public string name;
        [SerializeField] public AudioClip[] audioClip;

        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public IEnumerator coroutine;
        [HideInInspector] public BGMStatus status = BGMStatus.none;
        private int _clipIndex = 0;
        public int clipIndex
        {
            get => _clipIndex;
            set
            {
                _clipIndex = value;
                if (_clipIndex >= audioClip.Length) _clipIndex = 0;

                audioSource.clip = audioClip[_clipIndex];
            }
        }

        public bool IsFinished()
        {
            return audioSource.time == 0.0f && !audioSource.isPlaying;
        }
    }

    [SerializeField] float MAX_VOLUME;
    [SerializeField] List<BGMInfo> BGMList = new List<BGMInfo>();

    private Dictionary<string, BGMInfo> _BGMDictionary = new Dictionary<string, BGMInfo>();

    private float _volume;
 
    public override void Awake()
    {
        base.Awake();

        for (var i = 0; i < BGMList.Count; ++i)
        {
            BGMList[i].audioSource = gameObject.AddComponent<AudioSource>();
            BGMList[i].audioSource.loop = false;
            BGMList[i].audioSource.priority = 1;
            BGMList[i].audioSource.volume = 0;

            _BGMDictionary.Add(BGMList[i].name, BGMList[i]);
        }
    }
    public void Update()
    {
        foreach (var item in _BGMDictionary.Values)
        {
            if ( (item.status == BGMStatus.fadeIn || item.status == BGMStatus.play || item.status == BGMStatus.fadeOut) && item.IsFinished())
            {
                item.clipIndex++;
                item.audioSource.Play();
            }
        }   
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    public void Play(string name, float fadeTime)
    {
        List<string> playingBGM = GetPlayingBGM();
        foreach (var item in playingBGM) 
        {
            if (item != name) Stop(item, fadeTime);
        }
        if (playingBGM.Any(x => x == name)) return;
        
        if (_BGMDictionary.TryGetValue(name, out var BGMInfo))
        {
            if (BGMInfo.coroutine != null) StopCoroutine(BGMInfo.coroutine);

            if (BGMInfo.status == BGMStatus.fadeOut || BGMInfo.status == BGMStatus.pause) {
                BGMInfo.coroutine = CResume(BGMInfo, fadeTime);
            }
            else {
                BGMInfo.clipIndex = BGMInfo.clipIndex;
                BGMInfo.coroutine = CPlay(BGMInfo, fadeTime);
            }

            StartCoroutine(BGMInfo.coroutine);
        }
    }
    IEnumerator CPlay(BGMInfo BGMInfo, float fadeTime)
    {
        BGMInfo.status = BGMStatus.fadeIn;
        BGMInfo.audioSource.Play();
        float currentVolume = BGMInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            BGMInfo.audioSource.volume += ( _volume - currentVolume ) / 100;
            if (BGMInfo.audioSource.volume > _volume) BGMInfo.audioSource.volume = _volume;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        BGMInfo.audioSource.volume = _volume;
        BGMInfo.status = BGMStatus.play;
    }
    IEnumerator CResume(BGMInfo BGMInfo, float fadeTime)
    {
        BGMInfo.status = BGMStatus.fadeIn;
        BGMInfo.audioSource.pitch = 1;
        float currentVolume = BGMInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            BGMInfo.audioSource.volume += ( _volume - currentVolume ) / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        BGMInfo.audioSource.volume = _volume;
        BGMInfo.status = BGMStatus.play;
    }

    /// <summary>
    /// BGMを停止する
    /// </summary>
    public void Stop(string name, float fadeTime)
    {
        if (_BGMDictionary.TryGetValue(name, out var BGMInfo))
        {
            if (BGMInfo.coroutine != null) StopCoroutine(BGMInfo.coroutine);

            BGMInfo.coroutine = CStop(BGMInfo, fadeTime);
            StartCoroutine(BGMInfo.coroutine);
        }
    }
    IEnumerator CStop(BGMInfo BGMInfo, float fadeTime)
    {
        BGMInfo.status = BGMStatus.fadeOut;
        float currentVolume = BGMInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            BGMInfo.audioSource.volume -= currentVolume / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        BGMInfo.audioSource.volume = 0;
        BGMInfo.audioSource.Stop();
        BGMInfo.status = BGMStatus.stop;
    }

    /// <summary>
    /// BGMを一時停止する
    /// </summary>
    public void Pause(string name, float fadeTime)
    {
        if (_BGMDictionary.TryGetValue(name, out var BGMInfo))
        {
            if (BGMInfo.coroutine != null) StopCoroutine(BGMInfo.coroutine);

            BGMInfo.coroutine = CPause(BGMInfo, fadeTime);
            StartCoroutine(BGMInfo.coroutine);
        }
    }
    IEnumerator CPause(BGMInfo BGMInfo, float fadeTime)
    {
        BGMInfo.status = BGMStatus.fadeOut;
        float currentVolume = BGMInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            BGMInfo.audioSource.volume -= currentVolume / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        BGMInfo.audioSource.volume = 0;
        BGMInfo.audioSource.pitch = 0;
        BGMInfo.status = BGMStatus.pause;
    }

    /// <summary>
    /// BGMの音量を変更する
    /// </summary>
    public void ChangeVolume(float volume)
    {
        _volume = volume * MAX_VOLUME;
        foreach (var item in _BGMDictionary.Values)
        {
            if (item.status == BGMStatus.fadeIn || item.status == BGMStatus.play) item.audioSource.volume = _volume;
        }
    }

    /// <summary>
    /// BGMをミュートする
    /// </summary>
    public void Mute()
    {
        foreach (var item in _BGMDictionary.Values)
        {
            item.audioSource.pitch = 0;
        }
    }
    /// <summary>
    /// BGMのミュートを解除する
    /// </summary>
    public void UnMute()
    {
        foreach (var item in _BGMDictionary.Values)
        {
            item.audioSource.pitch = 1;
        }
    }

    /// <summary>
    /// 再生中のBGMを取得する
    /// </summary>
    private List<string> GetPlayingBGM()
    {
        List<string> playingBGM = new List<string>();
        foreach (var item in _BGMDictionary.Values)
        {
            if (item.status == BGMStatus.fadeIn || item.status == BGMStatus.play) playingBGM.Add(item.name);
        }
        return playingBGM;
    }
}
