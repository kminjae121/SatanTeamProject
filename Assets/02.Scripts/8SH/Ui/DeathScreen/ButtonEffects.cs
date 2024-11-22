using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    private Color originColor = Color.gray;
    [SerializeField] private Color tweenColor = Color.white;


    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.To(() => originColor, x => text.color = x, tweenColor, 0.35f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.To(() => text.color, x => text.color = x, originColor, 0.35f);
    }
}
