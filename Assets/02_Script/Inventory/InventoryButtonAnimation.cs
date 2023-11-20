using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class InventoryButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOLocalMoveX(0, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOLocalMoveX(100, 0.2f);
    }
}
