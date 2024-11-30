using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName { 
    title, selectDifficulty, 
    normalStage_0, normalStage_1, normalStage_2, normalStage_3, normalStage_4, normalStage_5, normalStage_6, normalStage_7, normalStage_8, normalStage_9,  
    extraStage, 
    develop }

public class S_LoadSceneSystem : Singleton<S_LoadSceneSystem>
{
    public void LoadScene(SceneName sceneName)
    {
        S_FadeManager._instance.Fade(() => Load(sceneName), () => {}, FadeType.Black, 0.5f,1f,0.5f);
    }

    private void Load(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.title:
                SceneManager.LoadScene("Title");
            break;
            case SceneName.selectDifficulty:
                SceneManager.LoadScene("SelectDifficulty");
            break;
            case SceneName.normalStage_0:
                SceneManager.LoadScene("NormalStage_0");
            break;
            case SceneName.normalStage_1:
                SceneManager.LoadScene("NormalStage_1");
            break;
            case SceneName.normalStage_2:
                SceneManager.LoadScene("NormalStage_2");
            break;
            case SceneName.normalStage_3:
                SceneManager.LoadScene("NormalStage_3");
            break;
            case SceneName.normalStage_4:
                SceneManager.LoadScene("NormalStage_4");
            break;
            case SceneName.normalStage_5:
                SceneManager.LoadScene("NormalStage_5");
            break;
            case SceneName.normalStage_6:
                SceneManager.LoadScene("NormalStage_6");
            break;
            case SceneName.normalStage_7:
                SceneManager.LoadScene("NormalStage_7");
            break;
            case SceneName.normalStage_8:
                SceneManager.LoadScene("NormalStage_8");
            break;
            case SceneName.normalStage_9:
                SceneManager.LoadScene("NormalStage_9");
            break;
            case SceneName.extraStage:
                SceneManager.LoadScene("ExtraStage");
            break;
            case SceneName.develop:
                SceneManager.LoadScene("DevelopScene");
            break;
        }
    }
}
