using DG.Tweening;
using UnityEngine;

public class DORotate : DOBase
{
    public Vector3 startValue;
    public Vector3 endValue = Vector3.one;

    public bool local;

    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            if (!local)
                c_Transform.DORotate(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
            else
                c_Transform.DOLocalRotate(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
        }
        else
        {
            if (!local)
                transform.rotation = Quaternion.Euler(endValue);
            else
                transform.localRotation = Quaternion.Euler(endValue);
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            if (!local)
                c_Transform.DORotate(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
            else
                c_Transform.DOLocalRotate(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
        }
        else
        {
            if (!local)
                transform.rotation = Quaternion.Euler(startValue);
            else
                transform.localRotation = Quaternion.Euler(startValue);
        }
    }
    public override void ResetDO()
    {
        transform.DOKill(true);
        if (!local)
            transform.rotation =Quaternion.Euler(startValue);
        else
            transform.localRotation = Quaternion.Euler(startValue);
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(c_Transform))
                c_Transform.DOKill(true);
            if (!local)
                c_Transform.DORotate(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
            else
                c_Transform.DOLocalRotate(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            if (!local)
                transform.rotation = Quaternion.Euler(endValue);
            else
                transform.localRotation = Quaternion.Euler(endValue);
        }
    }
}
