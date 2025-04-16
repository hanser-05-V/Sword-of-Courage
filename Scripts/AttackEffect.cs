using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    private Enemy moster;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            moster = collision.GetComponent<Enemy>();
            
            if(moster!=null)
            {
                moster.GetHit(0.5f);
                
                vcam.gameObject.SetActive(false);
                //震动时间，震动强度，持续时间为5秒，震动随机，是否抖动为false，是否恢复到原位为true
                Camera.main.transform.DOShakePosition(1,1,5,90,false,true).OnComplete(()=>
                {
                    vcam.gameObject.SetActive(true);
                }); 
                
                
            }
            
        }    
    }
}
