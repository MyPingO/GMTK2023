using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Page : MonoBehaviour
{
    [SerializeField] protected bool hidesPageBelow;

    protected RectTransform rectTransform;
    protected CanvasGroup canvasGroup;
    protected bool initialized;

    public bool HidesPageBelow => hidesPageBelow;

    public virtual void Initialize()
    {
        if (initialized) return;

        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        initialized = true;
    }

    public virtual void OpenPage(PageController pageController)
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1f, 0.5f);
    }

    public virtual void ClosePage(PageController pageController)
    {
        canvasGroup.DOFade(0f, 0.5f);
        gameObject.SetActive(false);
    }
}
