using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMono<SkillManager> //这里负责声明和得到 技能组件
{

    public Dash_Sklii dash {get ; private set;}

    void Start()
    {
        dash = this.GetComponent<Dash_Sklii>(); //这里负责实例化 技能组件
    }
}
