using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private const string EnemyTag = "Enemy";

    public async void InitializeBubble()
    {
        CorrectedPosition();
        
        transform.DOLocalMoveY(transform.position.y + 2.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //lifetime
        await UniTask.WaitForSeconds(5f);
        DOTween.Kill(transform);
        Destroy(gameObject);
    }

    private void CorrectedPosition()
    {
        Vector3 correctedPosition =transform.position;
        correctedPosition.z = 0;
        transform.position = correctedPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EnemyTag))
        {
            Debug.Log("enemy ile etkilesime girdi");
            //enemy'i parent olarak bubble a alacaksın setparent
            // bu bubble'ı yukarı dogru transform position ++ ekeleyeceksin kıi mantıken yukarı dogru gidiyor anamaı versin
            //sonra bir patlama aniamasoyun bu da dotween doscale 0* yparak scale'ini 0 yapparak ilk bastaki an im asiyopnsu yapsaiblrisin
            
        }
    }
}
