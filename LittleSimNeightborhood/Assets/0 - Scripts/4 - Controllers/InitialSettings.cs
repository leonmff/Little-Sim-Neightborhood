using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSettings : MonoBehaviour
{
    [SerializeField]
    int _targetFramerate = 0;

    private void Awake()
    {
        Application.targetFrameRate = _targetFramerate;
        SaveLoad.Load();
    }
}
