using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAttacher : MonoBehaviour
{
    [SerializeField] protected Rigidbody AttachTarget;
    [SerializeField] protected float OffsetY = 60;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(AttachTarget.transform.position);
        screenPos.y += OffsetY;
        //screenPos.x = Mathf.Min(Screen.width - (_rectTransform.sizeDelta.x / 2), screenPos.x);

        _rectTransform.position = screenPos;
    }
}