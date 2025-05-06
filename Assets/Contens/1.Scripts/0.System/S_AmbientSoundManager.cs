using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AmbientSoundManager : Singleton<S_AmbientSoundManager>
{
    enum ASStatus {  none, fadeIn, play, fadeOut, pause, stop }

    [Serializable] class ASInfo
    {
        [SerializeField] public string name;
        [SerializeField] public AudioClip[] audioClip;

        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public IEnumerator coroutine;
        [HideInInspector] public ASStatus status = ASStatus.none;
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
    [SerializeField] List<ASInfo> ASList = new List<ASInfo>();

    private Dictionary<string, ASInfo> _ASDictionary = new Dictionary<string, ASInfo>();

    private float _volume;
 
    public override void Awake()
    {
        base.Awake();

        for (var i = 0; i < ASList.Count; ++i)
        {
            ASList[i].audioSource = gameObject.AddComponent<AudioSource>();
            ASList[i].audioSource.loop = false;
            ASList[i].audioSource.priority = 1;
            ASList[i].audioSource.volume = 0;

            _ASDictionary.Add(ASList[i].name, ASList[i]);
        }
    }
    public void Update()
    {
        foreach (var item in _ASDictionary.Values)
        {
            if ( (item.status == ASStatus.fadeIn || item.status == ASStatus.play || item.status == ASStatus.fadeOut) && item.IsFinished())
            {
                item.clipIndex++;
                item.audioSource.Play();
            }
        }   
    }

    /// <summary>
    /// ASを再生する
    /// </summary>
    public void Play(string name, float fadeTime)
    {
        List<string> playingAS = GetPlayingAS();
        foreach (var item in playingAS) 
        {
            if (item != name) Stop(item, fadeTime);
        }
        if (playingAS.Any(x => x == name)) return;
        
        if (_ASDictionary.TryGetValue(name, out var ASInfo))
        {
            if (ASInfo.coroutine != null) StopCoroutine(ASInfo.coroutine);

            if (ASInfo.status == ASStatus.fadeOut || ASInfo.status == ASStatus.pause) {
                ASInfo.coroutine = CResume(ASInfo, fadeTime);
            }
            else {
                ASInfo.clipIndex = ASInfo.clipIndex;
                ASInfo.coroutine = CPlay(ASInfo, fadeTime);
            }

            StartCoroutine(ASInfo.coroutine);
        }
    }
    IEnumerator CPlay(ASInfo ASInfo, float fadeTime)
    {
        ASInfo.status = ASStatus.fadeIn;
        ASInfo.audioSource.Play();
        float currentVolume = ASInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            ASInfo.audioSource.volume += ( _volume - currentVolume ) / 100;
            if (ASInfo.audioSource.volume > _volume) ASInfo.audioSource.volume = _volume;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        ASInfo.audioSource.volume = _volume;
        ASInfo.status = ASStatus.play;
    }
    IEnumerator CResume(ASInfo ASInfo, float fadeTime)
    {
        ASInfo.status = ASStatus.fadeIn;
        ASInfo.audioSource.pitch = 1;
        float currentVolume = ASInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            ASInfo.audioSource.volume += ( _volume - currentVolume ) / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        ASInfo.audioSource.volume = _volume;
        ASInfo.status = ASStatus.play;
    }

    /// <summary>
    /// ASを停止する
    /// </summary>
    public void Stop(string name, float fadeTime)
    {
        if (_ASDictionary.TryGetValue(name, out var ASInfo))
        {
            if (ASInfo.coroutine != null) StopCoroutine(ASInfo.coroutine);

            ASInfo.coroutine = CStop(ASInfo, fadeTime);
            StartCoroutine(ASInfo.coroutine);
        }
    }
    IEnumerator CStop(ASInfo ASInfo, float fadeTime)
    {
        ASInfo.status = ASStatus.fadeOut;
        float currentVolume = ASInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            ASInfo.audioSource.volume -= currentVolume / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        ASInfo.audioSource.volume = 0;
        ASInfo.audioSource.Stop();
        ASInfo.status = ASStatus.stop;
    }

    /// <summary>
    /// ASを一時停止する
    /// </summary>
    public void Pause(string name, float fadeTime)
    {
        if (_ASDictionary.TryGetValue(name, out var ASInfo))
        {
            if (ASInfo.coroutine != null) StopCoroutine(ASInfo.coroutine);

            ASInfo.coroutine = CPause(ASInfo, fadeTime);
            StartCoroutine(ASInfo.coroutine);
        }
    }
    IEnumerator CPause(ASInfo ASInfo, float fadeTime)
    {
        ASInfo.status = ASStatus.fadeOut;
        float currentVolume = ASInfo.audioSource.volume;

        for (int i = 0; i < 100; i++)
        {
            ASInfo.audioSource.volume -= currentVolume / 100;
            yield return new WaitForSecondsRealtime(fadeTime / 100);
        }
        ASInfo.audioSource.volume = 0;
        ASInfo.audioSource.pitch = 0;
        ASInfo.status = ASStatus.pause;
    }

    /// <summary>
    /// ASの音量を変更する
    /// </summary>
    public void ChangeVolume(float volume)
    {
        _volume = volume * MAX_VOLUME;
        foreach (var item in _ASDictionary.Values)
        {
            if (item.status == ASStatus.fadeIn || item.status == ASStatus.play) item.audioSource.volume = _volume;
        }
    }

    /// <summary>
    /// ASをミュートする
    /// </summary>
    public void Mute()
    {
        foreach (var item in _ASDictionary.Values)
        {
            item.audioSource.pitch = 0;
        }
    }
    /// <summary>
    /// ASのミュートを解除する
    /// </summary>
    public void UnMute()
    {
        foreach (var item in _ASDictionary.Values)
        {
            item.audioSource.pitch = 1;
        }
    }

    /// <summary>
    /// 再生中のASを取得する
    /// </summary>
    private List<string> GetPlayingAS()
    {
        List<string> playingAS = new List<string>();
        foreach (var item in _ASDictionary.Values)
        {
            if (item.status == ASStatus.fadeIn || item.status == ASStatus.play) playingAS.Add(item.name);
        }
        return playingAS;
    }
}
