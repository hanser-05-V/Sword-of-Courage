using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour // 实体类的公共行为
{
    
    protected int facing = 1; // 角色的朝向，1为右，-1为左
    private bool isFacingRight = true; // 角色的默认朝向，true为右，false为左


    public float xInput; // 角色的水平方向输入值
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        FlipController();
    }
  
    #region 角色翻转
    public void Flip()
    {
        facing = facing * -1; // 角色的朝向取反
        isFacingRight =!isFacingRight; // 角色的默认朝向取反
        this.transform.Rotate(0, 180, 0); // 角色的物理朝向也要跟着改变
    }   
    public void FlipController()
    {
        if(xInput == 1 && !isFacingRight)
        {
            Flip();
        }
        else if(xInput == -1 && isFacingRight)
        {
            Flip();
        }
    }
    #endregion
}
