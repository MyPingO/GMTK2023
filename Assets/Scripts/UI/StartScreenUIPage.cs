using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenUIPage : Page
{
    [Header("Components")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Button leaderboardsBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Transform subtitle2Transform;

    private void Start()
    {
        subtitle2Transform.DOScale(0.9f, 0.85f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        playBtn.onClick.AddListener(() =>
        {
            SceneController.ToGameLevel();
        });
    }
}
