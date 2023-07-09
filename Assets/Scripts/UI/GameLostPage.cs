using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLostPage : Page
{
    [SerializeField] private Image background;
    [SerializeField] private Transform cardTransform;
    [SerializeField] private Transform cardEntryStartPosition;
    [SerializeField] private Transform cardEntryEndPosition;
    [SerializeField] private Ease cardEntryEase = Ease.OutBounce;

    public override void OpenPage(PageController pageController)
    {
        gameObject.SetActive(true);
        StartCoroutine(OpenTransition());
    }

    public override void ClosePage(PageController pageController)
    {
        gameObject.SetActive(false);
        background.DOFade(0f, 0.5f);
    }

    private IEnumerator OpenTransition()
    {
        cardTransform.position = cardEntryStartPosition.position;

        background.DOFade(1f, 0.5f);

        yield return new WaitForSecondsRealtime(0.5f);

        cardTransform.DOMoveY(cardEntryEndPosition.position.y, 0.5f).SetEase(cardEntryEase);
    }
}
