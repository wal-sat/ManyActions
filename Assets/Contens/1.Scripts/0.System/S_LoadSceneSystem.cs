using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneKind
{ 
    plain_1_A, plain_2_A, plain_3_A, plain_4_A, plain_5_A, 
    plain_1_B, plain_2_B, plain_3_B, plain_4_B, plain_5_B, 
    blue_F1_A, blue_F2_A, blue_F3_A, blue_F4_A, blue_F5_A, blue_B1_A, blue_B2_A, blue_B3_A, blue_B4_A, blue_B5_A,
    blue_F1_B, blue_F2_B, blue_F3_B, blue_F4_B, blue_F5_B, blue_B1_B, blue_B2_B, blue_B3_B, blue_B4_B, blue_B5_B,
    green_F1_A, green_F2_A, green_F3_A, green_F4_A, green_F5_A, green_B1_A, green_B2_A, green_B3_A, green_B4_A, green_B5_A,
    green_F1_B, green_F2_B, green_F3_B, green_F4_B, green_F5_B, green_B1_B, green_B2_B, green_B3_B, green_B4_B, green_B5_B,
    yellow_F1_A, yellow_F2_A, yellow_F3_A, yellow_F4_A, yellow_F5_A, yellow_B1_A, yellow_B2_A, yellow_B3_A, yellow_B4_A, yellow_B5_A,
    yellow_F1_B, yellow_F2_B, yellow_F3_B, yellow_F4_B, yellow_F5_B, yellow_B1_B, yellow_B2_B, yellow_B3_B, yellow_B4_B, yellow_B5_B,
    purple_F1_A, purple_F2_A, purple_F3_A, purple_F4_A, purple_F5_A, purple_B1_A, purple_B2_A, purple_B3_A, purple_B4_A, purple_B5_A,
    purple_F1_B, purple_F2_B, purple_F3_B, purple_F4_B, purple_F5_B, purple_B1_B, purple_B2_B, purple_B3_B, purple_B4_B, purple_B5_B,
    red_1_A, red_2_A, red_3_A, red_4_A, red_5_A,
    red_1_B, red_2_B, red_3_B, red_4_B, red_5_B,
    title, stageSelect,
    develop ,selectDifficulty, 
}

public class S_LoadSceneSystem : Singleton<S_LoadSceneSystem>
{
    public void LoadScene(SceneKind sceneKind)
    {
        S_InputSystem._instance.canInput = false;
        S_FadeManager._instance.Fade(() => SceneManager.LoadScene(sceneKind.ToString()), () => S_InputSystem._instance.canInput = true, FadeType.Black, 0.5f,2f,0.5f);
    }
}
