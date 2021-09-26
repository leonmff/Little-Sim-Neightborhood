using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentZDepth : MonoBehaviour
{
    [SerializeField]
    float _offsetY = 0f;
    [SerializeField]
    Transform pParent;

    Vector3 Pos { get => transform.position; }

    private void Awake()
    {
        pParent = transform.root;
    }

    private void Update()
    {
        transform.position = new Vector3(Pos.x, Pos.y, pParent.position.y - _offsetY);
    }
}
