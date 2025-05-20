using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

public class LRBlock : MonoBehaviour
{
    [SerializeField] LRBlockManager lrBlockManager;
    [SerializeField] LRBlockView lrBlockView;
    [SerializeField] SplineAnimate splineAnimate;

    private Tweener _currentAnimation;

    private void Awake()
    {
        lrBlockManager.Register(this);
    }

    public void PlayAnimate(InputKind inputKind)
    {
        float currentTime = splineAnimate.NormalizedTime;
        float terminalTime, duration;

        if (inputKind == InputKind.L2) terminalTime = 0;
        else terminalTime = 1;

        duration = lrBlockManager.DURATION * Mathf.Abs( terminalTime - currentTime );

        if (_currentAnimation != null) _currentAnimation.Kill();
        _currentAnimation = DOVirtual.Float(currentTime, terminalTime, duration, v => splineAnimate.NormalizedTime = v).SetEase(Ease.OutSine);

        lrBlockView.SpriteChange(inputKind);
    }

    public void Initialize()
    {
        _currentAnimation.Kill();
        splineAnimate.NormalizedTime = 0;
        lrBlockView.SpriteChange(InputKind.L2);
    }
}
