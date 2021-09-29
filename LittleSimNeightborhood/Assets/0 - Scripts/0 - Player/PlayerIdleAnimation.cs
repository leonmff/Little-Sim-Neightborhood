using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleAnimation : MonoBehaviour
{
    [SerializeField]
    Vector2 _minMaxSize = Vector2.one;
    [SerializeField]
    float _duration = 0f;

    private void Start()
    {
        StartCoroutine(PingPongAnimation());
    }

    IEnumerator PingPongAnimation()
    {
        float t_elapsedTime = 0;
        Vector3 t_initialScale = Vector3.one;

        while (true)
        {
            t_elapsedTime = 0;
            t_initialScale = transform.localScale;

            while (t_elapsedTime < _duration)
            {
                transform.localScale = Vector2.Lerp(t_initialScale, new Vector3(transform.localScale.x, _minMaxSize.y), t_elapsedTime / _duration);
                t_elapsedTime += Time.deltaTime;
                yield return null;
            }

            t_elapsedTime = 0;
            t_initialScale = transform.localScale;

            while (t_elapsedTime < _duration)
            {
                transform.localScale = Vector2.Lerp(t_initialScale, new Vector3(transform.localScale.x, _minMaxSize.x), t_elapsedTime / _duration);
                t_elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
