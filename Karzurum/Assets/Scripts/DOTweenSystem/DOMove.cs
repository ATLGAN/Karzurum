using DG.Tweening;
using UnityEngine;

public class DOMove : DOBase
{
    public bool useStartTarget;
    [ConditionalField("useStartTarget", true)]
    public RectTransform startTarget;

    public bool useEndTarget;
    [ConditionalField("useEndTarget", true)]
    public RectTransform endTarget;

    [ConditionalField("useStartTarget", false)]
    public Vector3 startValue;
    [ConditionalField("useEndTarget", false)]
    public Vector3 endValue = Vector3.one;

    public bool local;

    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            if (useEndTarget)
            {
                if (!local)
                    c_Transform.DOMove(endTarget.position, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                else
                    c_Transform.DOLocalMove(endTarget.localPosition, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            }
            else
            {
                if (!local)
                    c_Transform.DOMove(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                else
                    c_Transform.DOLocalMove(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            }
        }
        else
        {
            if (useEndTarget)
            {
                if (!local)
                    transform.position = endTarget.position;
                else
                    transform.localPosition = endTarget.localPosition;
            }
            else
            {
                if (!local)
                    transform.position = endValue;
                else
                    transform.localPosition = endValue;
            }
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            if (useStartTarget)
            {
                if (!local)
                    c_Transform.DOMove(startTarget.position, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                else
                    c_Transform.DOLocalMove(startTarget.localPosition, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            }
            else
            {
                if (!local)
                    c_Transform.DOMove(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                else
                    c_Transform.DOLocalMove(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            }
        }
        else
        {
            if (useStartTarget)
            {
                if (!local)
                    transform.position = startTarget.position;
                else
                    transform.localPosition = startTarget.localPosition;
            }
            else
            {
                if (!local)
                    transform.position = startValue;
                else
                    transform.localPosition = startValue;
            }
        }
    }
    public override void ResetDO()
    {
        transform.DOKill(true);
        if (useStartTarget)
        {
            if (!local)
                transform.position = startTarget.position;
            else
                transform.localPosition = startTarget.localPosition;
        }
        else
        {
            if (!local)
                transform.position = startValue;
            else
                transform.localPosition = startValue;
        }
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            if (useEndTarget)
            {
                if (!local)
                    c_Transform.DOMove(endTarget.position, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                else
                    c_Transform.DOLocalMove(endTarget.localPosition, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            }
            else
            {
                if (!local)
                    c_Transform.DOMove(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                else
                    c_Transform.DOLocalMove(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            }
        }
        else
        {
            if (useEndTarget)
            {
                if (!local)
                    transform.position = endTarget.position;
                else
                    transform.localPosition = endTarget.localPosition;
            }
            else
            {
                if (!local)
                    transform.position = endValue;
                else
                    transform.localPosition = endValue;
            }
        }
    }
}
