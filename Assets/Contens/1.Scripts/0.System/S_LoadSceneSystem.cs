using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName { title, selectDifficulty, normalStage, extraStage }

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
            case SceneName.normalStage:
                SceneManager.LoadScene("NormalStage");
            break;
            case SceneName.extraStage:
                SceneManager.LoadScene("ExtraStage");
            break;
        }
    }
}
