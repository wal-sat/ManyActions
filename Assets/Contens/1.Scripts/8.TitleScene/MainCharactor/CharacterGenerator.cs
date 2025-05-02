using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class ActionInfo 
{
    public float InitTime;
    public float DurationTime;
    public InputKind inputKind;
    [HideInInspector] public bool on;
    [HideInInspector] public bool onPast;
}

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] GameObject Character;
    [SerializeField] float DIE_TIME;
    [SerializeField] bool isFacingRight;
    [SerializeField] List<ActionInfo> actionList = new List<ActionInfo>();

    public Action DestroyCallBack;

    public void InstantiateCharacter()
    {
        GameObject generatedObject = Instantiate(Character, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        generatedObject.SetActive(true);
        generatedObject.GetComponent<CharacterManager>().Init(DestroyCallBack, actionList, DIE_TIME, isFacingRight);
    }
}

