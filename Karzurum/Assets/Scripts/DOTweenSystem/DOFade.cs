using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DOFade : DOBase
{
    public enum FadeSource
    {
        Image,
        CanvasGroup,
        SpriteRenderer
    }

    public float startValue;
    public float endvalue = 1;

    public FadeSource fadeSource;

    Image sourceImage;
    SpriteRenderer sourceSprite;
    CanvasGroup sourceCanvasGroup;

    internal override void VirtualEnable()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                sourceImage = GetComponent<Image>();
                break;
            case FadeSource.CanvasGroup:
                sourceCanvasGroup = GetComponent<CanvasGroup>();
                break;
            case FadeSource.SpriteRenderer:
                sourceSprite = GetComponent<SpriteRenderer>();
                break;
        }
    }
    public override void DO()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
                sourceImage.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                break;
            case FadeSource.CanvasGroup:
                if (DOTween.IsTweening(sourceCanvasGroup))
                    sourceCanvasGroup.DOKill(true);
                sourceCanvasGroup.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                break;
            case FadeSource.SpriteRenderer:
                if (DOTween.IsTweening(sourceSprite))
                    sourceSprite.DOKill(true);
                sourceSprite.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).OnComplete(() => doComplete.Invoke());
                break;
        }
    }
    public override void DORevert()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
                sourceImage.DOFade(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                break;
            case FadeSource.CanvasGroup:
                if (DOTween.IsTweening(sourceCanvasGroup))
                    sourceCanvasGroup.DOKill(true);
                sourceCanvasGroup.DOFade(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                break;
            case FadeSource.SpriteRenderer:
                if (DOTween.IsTweening(sourceSprite))
                    sourceSprite.DOKill(true);
                sourceSprite.DOFade(startValue, duration).SetDelay(revertDelay).SetEase(ease).OnComplete(() => doRevertComplete.Invoke());
                break;
        }
    }
    public override void ResetDO()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                sourceImage.DOKill(true);
                Color color = sourceImage.color;
                color.a = startValue;
                sourceImage.color = color;
                break;
            case FadeSource.CanvasGroup:
                sourceCanvasGroup.DOKill(true);
                sourceCanvasGroup.alpha = startValue;
                break;
            case FadeSource.SpriteRenderer:
                sourceSprite.DOKill(true);
                Color color2 = sourceSprite.color;
                color2.a = startValue;
                sourceSprite.color = color2;
                break;
        }
    }
    public override void DOLoop()
    {
        switch (fadeSource)
        {
            case FadeSource.Image:
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill(true);
                sourceImage.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                break;
            case FadeSource.CanvasGroup:
                if (DOTween.IsTweening(sourceImage))
                    sourceCanvasGroup.DOKill(true);
                sourceCanvasGroup.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                break;
            case FadeSource.SpriteRenderer:
                if (DOTween.IsTweening(sourceImage))
                    sourceSprite.DOKill(true);
                sourceSprite.DOFade(endvalue, duration).SetDelay(doDelay).SetEase(ease).SetLoops(-1, loopType);
                break;
        }
    }
}
