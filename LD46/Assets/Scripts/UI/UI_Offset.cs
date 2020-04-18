using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Offset : MonoBehaviour
{
    [SerializeField] private float rightOffset;
    [SerializeField] private float leftOffset;
    [SerializeField] private float upOffset;
    [SerializeField] private float downOffset;

    private Camera _mainCamera;

    public void OffsetElement(RectTransform rect)
    {
        _mainCamera = Camera.main;
        rightOffset = (rightOffset/100) * _mainCamera.pixelWidth;
        leftOffset = (leftOffset / 100) * _mainCamera.pixelWidth;
        upOffset = (upOffset / 100) * _mainCamera.pixelHeight;
        downOffset = (downOffset / 100) * _mainCamera.pixelHeight;

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - rightOffset + leftOffset, rect.anchoredPosition.y + downOffset - upOffset);
    }

}
