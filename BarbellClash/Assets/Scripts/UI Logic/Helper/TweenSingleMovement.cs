using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TweenSingleMovement : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField] private Vector3[] movementVectors;
    [SerializeField] private float duration;
    
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void MoveTo(int index) => rect.DOAnchorPos(movementVectors[index], duration);
}
