using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLevelDelay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownTMP;
    [SerializeField] private Transform countdownParent;
    [SerializeField] private Transform countdownParentEndPosition;

    private void Start()
    {
        LoadingScreen.OnConfirmButtonClicked += () =>
        {
            StartCoroutine(CountDownRoutine());
        };
    }

    private void Update()
    {
        Time.timeScale = 0f;
    }

    private IEnumerator CountDownRoutine()
    {
        yield return new WaitForSecondsRealtime(2f);

        countdownTMP.text = "Ready?";

        yield return new WaitForSecondsRealtime(2f);

        countdownTMP.text = "Set...";

        yield return new WaitForSecondsRealtime(2f);

        countdownTMP.text = "Go!";
        countdownParent.DOMoveY(countdownParentEndPosition.position.y, 0.75f).SetEase(Ease.InBack).OnComplete(() =>
        {
            countdownParent.gameObject.SetActive(false);
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        });
    }
}
