using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{ 
    title, selectDifficulty, 
    normalStage_0, normalStage_1, normalStage_2, normalStage_3, normalStage_4, normalStage_5, normalStage_6, normalStage_7, normalStage_8, normalStage_9,
    normalStage_10, normalStage_11, normalStage_12, normalStage_13, normalStage_14, normalStage_15, normalStage_16, normalStage_17, normalStage_18, normalStage_19,
    extraStage_0, extraStage_1, extraStage_2, extraStage_3, extraStage_4, extraStage_5, extraStage_6, extraStage_7, extraStage_8, extraStage_9,
    extraStage_10, extraStage_11, extraStage_12, extraStage_13, extraStage_14, extraStage_15, extraStage_16, extraStage_17, extraStage_18, extraStage_19,
    develop 
}

public class S_LoadSceneSystem : Singleton<S_LoadSceneSystem>
{
    public void LoadScene(SceneName sceneName)
    {
        S_InputSystem._instance.canInput = false;
        S_FadeManager._instance.Fade(() => Load(sceneName), () => S_InputSystem._instance.canInput = true, FadeType.Black, 0.5f,2f,0.5f);
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
            case SceneName.extraStage_0:
                SceneManager.LoadScene("ExtraStage_0");
            break;
            case SceneName.extraStage_1:
                SceneManager.LoadScene("ExtraStage_1");
            break;
            case SceneName.extraStage_2:
                SceneManager.LoadScene("ExtraStage_2");
            break;
            case SceneName.extraStage_3:
                SceneManager.LoadScene("ExtraStage_3");
            break;
            case SceneName.extraStage_4:
                SceneManager.LoadScene("ExtraStage_4");
            break;
            case SceneName.extraStage_5:
                SceneManager.LoadScene("ExtraStage_5");
            break;
            case SceneName.extraStage_6:
                SceneManager.LoadScene("ExtraStage_6");
            break;
            case SceneName.extraStage_7:
                SceneManager.LoadScene("ExtraStage_7");
            break;
            case SceneName.extraStage_8:
                SceneManager.LoadScene("ExtraStage_8");
            break;
            case SceneName.extraStage_9:
                SceneManager.LoadScene("ExtraStage_9");
            break;
            case SceneName.extraStage_10:
                SceneManager.LoadScene("ExtraStage_10");
            break;
            case SceneName.extraStage_11:
                SceneManager.LoadScene("ExtraStage_11");
            break;
            case SceneName.extraStage_12:
                SceneManager.LoadScene("ExtraStage_12");
            break;
            case SceneName.extraStage_13:
                SceneManager.LoadScene("ExtraStage_13");
            break;
            case SceneName.extraStage_14:
                SceneManager.LoadScene("ExtraStage_14");
            break;
            case SceneName.extraStage_15:
                SceneManager.LoadScene("ExtraStage_15");
            break;
            case SceneName.extraStage_16:
                SceneManager.LoadScene("ExtraStage_16");
            break;
            case SceneName.extraStage_17:
                SceneManager.LoadScene("ExtraStage_17");
            break;
            case SceneName.extraStage_18:
                SceneManager.LoadScene("ExtraStage_18");
            break;
            case SceneName.extraStage_19:
                SceneManager.LoadScene("ExtraStage_19");
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
            case "ExtraStage_0":
                sceneName = SceneName.extraStage_0;
            break;
            case "ExtraStage_1":
                sceneName = SceneName.extraStage_1;
            break;
            case "ExtraStage_2":
                sceneName = SceneName.extraStage_2;
            break;
            case "ExtraStage_3":
                sceneName = SceneName.extraStage_3;
            break;
            case "ExtraStage_4":
                sceneName = SceneName.extraStage_4;
            break;
            case "ExtraStage_5":
                sceneName = SceneName.extraStage_5;
            break;
            case "ExtraStage_6":
                sceneName = SceneName.extraStage_6;
            break;
            case "ExtraStage_7":
                sceneName = SceneName.extraStage_7;
            break;
            case "ExtraStage_8":
                sceneName = SceneName.extraStage_8;
            break;
            case "ExtraStage_9":
                sceneName = SceneName.extraStage_9;
            break;
            case "ExtraStage_10":
                sceneName = SceneName.extraStage_10;
            break;
            case "ExtraStage_11":
                sceneName = SceneName.extraStage_11;
            break;
            case "ExtraStage_12":
                sceneName = SceneName.extraStage_12;
            break;
            case "ExtraStage_13":
                sceneName = SceneName.extraStage_13;
            break;
            case "ExtraStage_14":
                sceneName = SceneName.extraStage_14;
            break;
            case "ExtraStage_15":
                sceneName = SceneName.extraStage_15;
            break;
            case "ExtraStage_16":
                sceneName = SceneName.extraStage_16;
            break;
            case "ExtraStage_17":
                sceneName = SceneName.extraStage_17;
            break;
            case "ExtraStage_18":
                sceneName = SceneName.extraStage_18;
            break;
            case "ExtraStage_19":
                sceneName = SceneName.extraStage_19;
            break;
            case "Develop":
                sceneName = SceneName.develop;
            break;
        }
        return sceneName;
    }
}
