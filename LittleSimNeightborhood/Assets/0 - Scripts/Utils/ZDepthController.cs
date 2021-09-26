using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDepthController : MonoBehaviour
{
    [SerializeField]
    bool _stationaryObject = true;

    Vector3 Pos { get => transform.position; }

    private void Update()
    {
        transform.position = new Vector3(Pos.x, Pos.y, Pos.y * 0.0001f);
        
        if (_stationaryObject)
            Destroy(this);
    }
}
