using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Bubble : MonoBehaviour
{
    private const string EnemyTag = "Enemy";
    private bool isEnemyCatched;
    [SerializeField] private float bubbleDurationTime = 2.5f;

    public async void InitializeBubble()
    {
        CorrectedPosition();

<<<<<<< Updated upstream
        transform.DOLocalMoveY(transform.position.y + 2.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //lifetime
        await UniTask.WaitForSeconds(4f);
        DOTween.Kill(transform);
        if (isEnemyCatched) return;
=======
        if (transform != null)
        {
            transform.DOLocalMoveY(transform.position.y + 2.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        // Lifetime (4 saniye bekleme)
        await UniTask.WaitForSeconds(bubbleDurationTime);

        if (this == null || gameObject == null)
        {
            return;
        }


        if (isEnemyCatched) return;
        DOTween.Kill(transform);

>>>>>>> Stashed changes
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

            if (other.transform != null)
            {
                other.transform.DOLocalMove(Vector3.zero, 1f);
            }

            if (transform != null)
            {
                transform.DOScale(0, 1f).SetEase(Ease.InBounce).OnComplete(() =>
                {
                    isEnemyCatched = false;
                    Destroy(gameObject);
                });
            }
        }
    }
}
