using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{ 
    title, selectDifficulty, 
    normalStage_0, normalStage_1, normalStage_2, normalStage_3, normalStage_4, normalStage_5, normalStage_6, normalStage_7, normalStage_8, normalStage_9,
    normalStage_10, normalStage_11, normalStage_12, normalStage_13, normalStage_14, normalStage_15, normalStage_16, normalStage_17, normalStage_18, normalStage_19,
    extraStage, 
    develop 
}

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
            case SceneName.normalStage_10:
                SceneManager.LoadScene("NormalStage_10");
            break;
            case SceneName.normalStage_11:
                SceneManager.LoadScene("NormalStage_11");
            break;
            case SceneName.normalStage_12:
                SceneManager.LoadScene("NormalStage_12");
            break;
            case SceneName.normalStage_13:
                SceneManager.LoadScene("NormalStage_13");
            break;
            case SceneName.normalStage_14:
                SceneManager.LoadScene("NormalStage_14");
            break;
            case SceneName.normalStage_15:
                SceneManager.LoadScene("NormalStage_15");
            break;
            case SceneName.normalStage_16:
                SceneManager.LoadScene("NormalStage_16");
            break;
            case SceneName.normalStage_17:
                SceneManager.LoadScene("NormalStage_17");
            break;
            case SceneName.normalStage_18:
                SceneManager.LoadScene("NormalStage_18");
            break;
            case SceneName.normalStage_19:
                SceneManager.LoadScene("NormalStage_19");
            break;
            case SceneName.extraStage:
                SceneManager.LoadScene("ExtraStage");
            break;
            case SceneName.develop:
                SceneManager.LoadScene("Develop");
            break;
        }
    }

    public SceneName StringToSceneName(string nameString)
    {
        SceneName sceneName = SceneName.title;
        switch (nameString)
        {
            case "Title":
                sceneName = SceneName.title;
            break;
            case "SelectDifficulty":
                sceneName = SceneName.selectDifficulty;
            break;
            case "NormalStage_0":
                sceneName = SceneName.normalStage_0;
            break;
            case "NormalStage_1":
                sceneName = SceneName.normalStage_1;
            break;
            case "NormalStage_2":
                sceneName = SceneName.normalStage_2;
            break;
            case "NormalStage_3":
                sceneName = SceneName.normalStage_3;
            break;
            case "NormalStage_4":
                sceneName = SceneName.normalStage_4;
            break;
            case "NormalStage_5":
                sceneName = SceneName.normalStage_5;
            break;
            case "NormalStage_6":
                sceneName = SceneName.normalStage_6;
            break;
            case "NormalStage_7":
                sceneName = SceneName.normalStage_7;
            break;
            case "NormalStage_8":
                sceneName = SceneName.normalStage_8;
            break;
            case "NormalStage_9":
                sceneName = SceneName.normalStage_9;
            break;
            case "NormalStage_10":
                sceneName = SceneName.normalStage_10;
            break;
            case "NormalStage_11":
                sceneName = SceneName.normalStage_11;
            break;
            case "NormalStage_12":
                sceneName = SceneName.normalStage_12;
            break;
            case "NormalStage_13":
                sceneName = SceneName.normalStage_13;
            break;
            case "NormalStage_14":
                sceneName = SceneName.normalStage_14;
            break;
            case "NormalStage_15":
                sceneName = SceneName.normalStage_15;
            break;
            case "NormalStage_16":
                sceneName = SceneName.normalStage_16;
            break;
            case "NormalStage_17":
                sceneName = SceneName.normalStage_17;
            break;
            case "NormalStage_18":
                sceneName = SceneName.normalStage_18;
            break;
            case "NormalStage_19":
                sceneName = SceneName.normalStage_19;
            break;
            case "ExtraStage":
                sceneName = SceneName.extraStage;
            break;
            case "Develop":
                sceneName = SceneName.develop;
            break;
        }
        return sceneName;
    }
}
