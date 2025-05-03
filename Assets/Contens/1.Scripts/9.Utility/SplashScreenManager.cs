using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        yield return new WaitForSecondsRealtime(1f);
        S_BGMManager._instance.ChangeVolume(0.0f);
        yield return new WaitForSecondsRealtime(0.2f);
        S_BGMManager._instance.Play("clear", 0.1f);
        yield return new WaitForSecondsRealtime(0.2f);
        S_BGMManager._instance.Stop("clear", 0.1f);
        yield return new WaitForSecondsRealtime(1f);
        S_BGMManager._instance.ChangeVolume(0.64f);
        yield return new WaitForSecondsRealtime(0.2f);
        S_LoadSceneSystem._instance.LoadScene(SceneKind.title);
    }
}
