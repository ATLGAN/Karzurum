﻿using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class DOScale : DOBase
{
    public Vector3 startValue;
    public Vector3 endValue = Vector3.one;

    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            c_Transform.DOScale(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
        }
        else
        {
            transform.localScale = endValue;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            c_Transform.DOScale(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
        }
        else
        {
            transform.localScale = startValue;
        }
    }
    public override void ResetDO()
    {
#if UNITY_EDITOR
        Undo.RecordObject(gameObject, name + "Changed transform");
#endif
        transform.DOKill(true);
        transform.localScale = startValue;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            c_Transform.DOScale(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            transform.localScale = endValue;
        }
    }
}
