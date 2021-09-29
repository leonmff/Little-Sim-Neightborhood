using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject _gameObject;

    public void Disable()
    {
        _gameObject.SetActive(false);
    }
}
