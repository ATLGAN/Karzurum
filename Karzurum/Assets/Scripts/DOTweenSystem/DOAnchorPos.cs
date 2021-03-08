using DG.Tweening;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class DOAnchorPos : DOBase
{
    public bool useStartTarget;
    [ConditionalField("useStartTarget", true)]
    public RectTransform startTarget;
    
    public bool useEndTarget;
    [ConditionalField("useEndTarget", true)]
    public RectTransform endTarget;

    [ConditionalField("useStartTarget", false)]
    public Vector2 startValue;
    [ConditionalField("useEndTarget", false)]
    public Vector2 endValue = Vector2.one;

    RectTransform  c_TransformRect;

    internal override void VirtualEnable()
    {
        c_TransformRect = GetComponent<RectTransform>();
    }
    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_TransformRect))
                c_TransformRect.DOKill();
            if(useEndTarget)
                c_TransformRect.DOAnchorPos(endTarget.anchoredPosition, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            else
                c_TransformRect.DOAnchorPos(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = endValue;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_TransformRect))
                c_TransformRect.DOKill();
            if (useStartTarget)
                c_TransformRect.DOAnchorPos(startTarget.anchoredPosition, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            else
                c_TransformRect.DOAnchorPos(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = startValue;
        }
    }
    public override void ResetDO()
    {
#if UNITY_EDITOR
        Undo.RecordObject(gameObject, name + "Changed transform");
#endif
        c_TransformRect.DOKill();
        if (useStartTarget)
            c_TransformRect.anchoredPosition = startTarget.anchoredPosition;
        else
            c_TransformRect.anchoredPosition = startValue;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_TransformRect))
                c_TransformRect.DOKill();
            if (useEndTarget)
                c_TransformRect.DOAnchorPos(endTarget.anchoredPosition, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            else
                c_TransformRect.DOAnchorPos(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            if (useEndTarget)
                GetComponent<RectTransform>().anchoredPosition = endTarget.anchoredPosition;
            else
                GetComponent<RectTransform>().anchoredPosition = endValue;
        }
    }
}
