using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartFade : MonoBehaviour
{
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        image.DOFade(0, 3).SetDelay(2);
    }
}
