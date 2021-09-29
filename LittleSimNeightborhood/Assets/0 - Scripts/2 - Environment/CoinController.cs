using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    int _quantity = 0;
    [SerializeField]
    float _flyToPlayerDuration = 0f;

    [SerializeField]
    CircleCollider2D _collider = null;

    bool _moving;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_moving)
            return;

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveToTransformPosition(collision.transform));
            _collider.enabled = false;
            _moving = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _collider.enabled = false;
            AddCoin(collision.gameObject);
        }
    }

    void AddCoin(GameObject pPlayer)
    {
        PlayerGoldController t_playerGold = pPlayer.GetComponent<PlayerGoldController>();
        if (t_playerGold)
        {
            t_playerGold.AddGold(_quantity);
            Destroy(gameObject);
        }
    }

    IEnumerator MoveToTransformPosition(Transform pTransform)
    {
        float t_elapsedTime = 0f;
        Vector3 t_initialPosition = transform.position;

        while (t_elapsedTime < _flyToPlayerDuration)
        {
            transform.position = Vector3.Lerp(t_initialPosition, pTransform.position, t_elapsedTime / _flyToPlayerDuration);
            t_elapsedTime += Time.deltaTime;
            yield return null;
        }

        AddCoin(pTransform.gameObject);
    }
}
