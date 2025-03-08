using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockStrike_Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private CharacterStats targetStats;
    private int damage;


    private Animator animator;
    private bool isTriggered;

    private void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    public void SetUpStrike(int _damage,CharacterStats _targetStats)
    {
        damage = _damage;
        targetStats = _targetStats;
    }
    private void Update()
    {

        if (targetStats == null)
            return;

        if(isTriggered)
        {
            return;
        }


        this.transform.position = Vector2.MoveTowards(this.transform.position,targetStats.transform.position,moveSpeed * Time.deltaTime);
        this.transform.right = transform.position - targetStats.transform.position;


        if(Vector2.Distance(this.transform.position,targetStats.transform.position) < 0.15f)
        {
            //靠近重置位置 播放在上方
            animator.transform.localPosition = new Vector3(0, 0.5f);
            animator.transform.localRotation = Quaternion.identity;

            this.transform.localScale = new Vector3(3,3);
            this.transform.localRotation = Quaternion.identity;

            isTriggered = true;
            Invoke("DamageAndSelfDestory", 0.2f);
            animator.SetTrigger("Hit");
        }
    }


    private void DamageAndSelfDestory()
    {
        targetStats.ApplyShock(true);
        targetStats.TakeDamage(damage);
        Destroy(this.gameObject, 0.4f);

    }
}
