using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField, InspectorReadOnly]
    bool _opened = false;
    [SerializeField, InspectorReadOnly]
    bool _locked = true;

    public bool Opened { get => _opened; }
    public bool Locked { get => _locked; }

    [SerializeField, Space(7)]
    Sprite _spClosed = null;
    [SerializeField]
    Sprite _spOpened = null;

    [SerializeField, Space(7)]
    GameObject _objOpenColliders = null;

    SpriteRenderer _sr;
    Collider2D _collider;

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        OpenClose(false, _spClosed);
    }

    public void Interact()
    {
        OpenClose(true, _spOpened);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_locked)
            OpenClose(true, _spOpened);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OpenClose(false, _spClosed);
    }

    void OpenClose(bool pOpen, Sprite pDoorSprite)
    {
        _opened = pOpen;
        _sr.sprite = pDoorSprite;
        _collider.isTrigger = pOpen;
        _objOpenColliders.SetActive(pOpen);
    }
}
