using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnscriptedLogic.MathUtils;

public class EndGameCard : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI wittyTextsTMP;
    [SerializeField] private TextMeshProUGUI diamondsCounterTMP;
    [SerializeField] private TextMeshProUGUI distanceCounterTMP;
    [SerializeField] private Button tryAgainBtn;
    [SerializeField] private Button returnToMainScreenBtn;
    [SerializeField] private Button quitGameBtn;

    [Space(10)]
    [SerializeField] private string[] wittyResponses;

    private void OnEnable()
    {
        diamondsCounterTMP.text = Score.instance.diamonds.ToString();
        distanceCounterTMP.text = Score.instance.distance.ToString("0.0");

        wittyTextsTMP.text = RandomLogic.FromArray(wittyResponses);

        tryAgainBtn.onClick.AddListener(() =>
        {
            SceneController.ToGameLevel();
        });

        returnToMainScreenBtn.onClick.AddListener(() => 
        { 
            SceneController.ToMainScreenLevel();
        });

        quitGameBtn.onClick.AddListener(() => 
        {
            SceneController.instance.QuitGame();
        });
    }
}
