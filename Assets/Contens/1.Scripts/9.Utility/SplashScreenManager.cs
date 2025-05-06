using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;

    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        blackPanel.SetActive(true);
        DOVirtual.Float(1, 0, 0.5f, (value) => blackPanel.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, value) );

        yield return null;
        
        S_BGMManager._instance.Mute();
        S_BGMManager._instance.Play("clear", 0.1f);
        S_AmbientSoundManager._instance.Mute();
        S_AmbientSoundManager._instance.Play("heartBeat", 0.1f);

        yield return new WaitForSecondsRealtime(1f);

        S_BGMManager._instance.Stop("clear", 0.1f);
        S_AmbientSoundManager._instance.Stop("heartBeat", 0.1f);

        yield return new WaitForSecondsRealtime(1f);

        S_BGMManager._instance.UnMute();
        S_AmbientSoundManager._instance.UnMute();

        yield return new WaitForSecondsRealtime(2f);

        S_LoadSceneSystem._instance.LoadScene(SceneKind.title);
    }
}
