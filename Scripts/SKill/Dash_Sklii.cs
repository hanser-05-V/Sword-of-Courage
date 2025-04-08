using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Sklii : SkillBase
{
     
    [SerializeField] private GameObject dashEffect;

    private GameObject newEffect;
    public override void UseSkill()
    {
       

    }

    public void CreateEfffect(Transform targetPos)
    {
        newEffect = GameObject.Instantiate(dashEffect, transform.position, Quaternion.identity);
        
    }
}
