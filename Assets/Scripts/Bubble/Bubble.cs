using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private const string EnemyTag = "Enemy";
    private bool isEnemyCatched;

    public async void InitializeBubble()
    {
        // Nesne yoksa, işleme devam etme
        if (this == null) return;

        CorrectedPosition();

        // Tween hareketini başlat
        var tween = transform.DOLocalMoveY(transform.position.y + 2.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        // Süreyi bekle
        await UniTask.WaitForSeconds(1f);

        // Nesne yoksa, işleme devam etme
        if (this == null) return;

        // Tween'i durdur
        tween.Kill();

        // Eğer düşman yakalanmamışsa, nesneyi yok et
        if (!isEnemyCatched)
        {
            Vector3 l= gameObject.transform.position;
            Destroy(gameObject);
        }
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
                KillCountManager.Instance?.IncrementKillCount();
                isEnemyCatched = false;
            });
        }
    }
}