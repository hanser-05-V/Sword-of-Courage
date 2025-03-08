using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;

    public static SkillManager Instance => instance;

    public Dash_Skill dash { get; private set; }

    public Clone_Skill clone { get; private set; }

    public Sword_Skill sword { get; private set; }

    public Blackhole_Skill blackhole { get; private set; }

    private void Start()
    {
        dash = this.GetComponent<Dash_Skill>();
        clone = this.GetComponent<Clone_Skill>();
        sword = this.GetComponent<Sword_Skill>();
        blackhole = this.GetComponent<Blackhole_Skill>();
      
    }

    private void Awake()
    {
       instance = this;   
    }

}
