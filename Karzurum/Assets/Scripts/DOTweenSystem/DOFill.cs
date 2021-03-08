using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DOFill : DOBase
{
    public float startValue;
    public float endValue;

    Image sourceImage;

    internal override void VirtualEnable()
    {
        sourceImage = GetComponent<Image>();
    }
    public override void DO()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(sourceImage))
                sourceImage.DOKill(true);
            sourceImage.DOFillAmount(endValue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
        }
        else
        {
            GetComponent<Image>().fillAmount = endValue;
        }
    }
    public override void DORevert()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(sourceImage))
                sourceImage.DOKill(true);
            sourceImage.DOFillAmount(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
        }
        else
        {
            GetComponent<Image>().fillAmount = startValue;
        }
    }
    public override void ResetDO()
    {
        sourceImage.DOKill(true);
        sourceImage.fillAmount = startValue;
    }
    public override void DOLoop()
    {
        if (Application.isPlaying)
        {
            if (DOTween.IsTweening(sourceImage))
                sourceImage.DOKill(true);
            sourceImage.DOFillAmount(endValue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
        }
        else
        {
            GetComponent<Image>().fillAmount = endValue;
        }
    }
}
