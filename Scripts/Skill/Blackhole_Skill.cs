using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill : SkillBase
{

    [SerializeField] private int attackAmount;
    [SerializeField] private float cloneAttackCoolDown;
    [SerializeField] private float blackholeDuration;
    [Space]
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float maxScale;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrikSpeed;

    private GameObject newBlackhole;
    private Blackhole_Skill_Controller blackController;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        
        newBlackhole = GameObject.Instantiate(blackholePrefab,player.transform.position,Quaternion.identity);
        blackController = newBlackhole.GetComponent<Blackhole_Skill_Controller>();

        blackController.SetupBlackhole(maxScale, growSpeed, shrikSpeed, attackAmount, cloneAttackCoolDown,blackholeDuration);

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool BlackholeFinished()
    {
        if (!blackController)
            return false;
        if (blackController.playCanEixtBlackhole)
        {
            blackController = null;
            return true;
        }
        return false;
    }

    public float GetBlackHoleRediu()
    {
        return maxScale / 2;
    }
}
