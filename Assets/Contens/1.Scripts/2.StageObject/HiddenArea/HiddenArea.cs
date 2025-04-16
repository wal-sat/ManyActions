using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class HiddenArea : MonoBehaviour
{
    [SerializeField] HiddenAreaManager hiddenAreaManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] Tilemap hiddenArea;

    private FirstCallChecker firstCallChecker;
    private Tween tween;

    private void Awake()
    {
        hiddenAreaManager.Register(this);
        stageObjectCollisionArea.triggerEnter = TriggerEnter;
        stageObjectCollisionArea.triggerExit = TriggerExit;

        firstCallChecker = new FirstCallChecker(); 
    }

    private void TriggerEnter()
    {
        if (tween != null) tween.Kill();
        tween = DOVirtual.Float(hiddenArea.color.a, 0, 0.5f, v => hiddenArea.color = new Color(255, 255, 255, v))
            .OnComplete( () => tween = null );

        if (firstCallChecker.Check()) Debug.Log("sound");
    }
    private void TriggerExit()
    {
        if (tween != null) tween.Kill();
        tween = DOVirtual.Float(hiddenArea.color.a, 1, 0.5f, v => hiddenArea.color = new Color(255, 255, 255, v))
            .OnComplete( () => tween = null );
    }

    public void Initialize()
    {
        firstCallChecker.Reset();
    }
}
