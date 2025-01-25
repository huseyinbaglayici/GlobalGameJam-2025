using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private const string EnemyTag = "Enemy";
    private bool isEnemyCatched;

    public async void InitializeBubble()
    {
        CorrectedPosition();

        transform.DOLocalMoveY(transform.position.y + 2.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //lifetime
        awa it UniTask.WaitForSeconds(4f);
        DOTween.Kill(transform);
        if (isEnemyCatched) return;
        Destroy(gameObject);
    }

    private void CorrectedPosition()
    {
        Vector3 correctedPosition = transform.position;
        correctedPosition.z = 0;
        transform.position = correctedPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EnemyTag))
        {
            isEnemyCatched = true;
            other.transform.SetParent(transform);
            other.transform.GetComponent<BoxCollider2D>().enabled = false;
            other.transform.DOLocalMove(Vector3.zero, 1f);
            transform.DOScale(0, 1f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                Destroy(gameObject);
                isEnemyCatched = false;
            });
        }
    }
}