using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG;

public class ItemBobbing : MonoBehaviour
{
    [SerializeField] private float bobbingHeight = 0.25f;
    [SerializeField] private float bobbingSpeed = 1f;
    [SerializeField] private Ease bobbingEase = Ease.InOutCubic;

    private void Start()
    {
        transform.DOMoveY(transform.position.y + bobbingHeight, bobbingSpeed).SetLoops(-1, loopType: LoopType.Yoyo).SetEase(bobbingEase);
    }
}
