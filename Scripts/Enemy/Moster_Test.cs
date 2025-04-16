using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster_Test : MonoBehaviour
{
    [Header("组件相关")]
    [SerializeField] private Rigidbody2D rb;
    private Vector2 direction ; //被击退的方向
    private bool isHit;

    public float hitSpeed = 5; //被击退的速度
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected void  Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        rb.velocity = direction * hitSpeed; 
    }

    IEnumerator GetHit(Vector2 direction)
    {
        rb.velocity = new Vector2(-direction.x * hitSpeed, rb.velocity.y);
        isHit = true;
        yield return new WaitForSeconds(0.5f); // 击退0.5秒后回复速度
        isHit = false;


    }
    public void DamageEffect(Vector2 _direction)
    {
        StartCoroutine(GetHit(_direction));
    }
   
}
