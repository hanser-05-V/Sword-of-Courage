using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;



public enum E_StatType
{
    strength,
    agility,
    intelegence,
    vitality,

    damage,
    critChance,
    critPower,

    maxhealth,
    armor,
    evasion,
    magicResistance,

    fireDamage,
    iceDamage,
    lightingDamage,

}
public class CharacterStats : MonoBehaviour
{
    private EnitityFX fx;

    [Header("Major stats")]
    public Stats strength; //力量
    public Stats agility;  //敏捷 %
    public Stats intelligence; // 智力 1点智力增加一点魔法伤害 3点魔抗
    public Stats vitality; //活力

    [Header("Offensive stats")]
    public Stats damage;
    public Stats critChance; // 暴击率
    public Stats critPower; //暴击伤害

    [Header("Defensive stats")]
    public Stats maxHealth;
    public Stats armor; // 盔甲
    public Stats evasion; // 闪避
    public Stats magicResistance; // 魔抗

    [Header("Magic stats")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightingDamage;
    [Space]
    

    public bool isIgnited; //点燃 持续造成伤害
    public bool isChilled; //冰冻 减低盔甲20%抗性
    public bool isShocked; //震撼 减少攻击命中率  增加玩家闪避概率

    [SerializeField] private float allAlimentDuration;
    private float igniteTimer;
    private float chilledTimer;
    private float shockedTimer;

    private float igniteDamageCoolDown = 0.3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    [SerializeField] private GameObject shockStrickPrefab;
    private int shockDamage;
    


    //当前血量和血量翻转委托
    public int currentHp;
    public UnityAction onHpChanged;

    public bool isDead { get; private set; }

    protected virtual void Awake()
    {
        fx = this.GetComponent<EnitityFX>();    
    }
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);//默认爆伤为 150%
        currentHp = GetMaxHealthValue();
    }

    protected virtual void Update()
    {
        igniteTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;

        if (igniteTimer < 0)
        {
            isIgnited = false;
        }
        if (chilledTimer < 0)
        {
            isChilled = false;
        }
        if (shockedTimer < 0)
        {
            isShocked = false;
        }

        if(isIgnited)
            ApplyIgniteDamage();
    }

    /// <summary>
    /// Buff 增加效果
    /// </summary>
    /// <param name="_modifier"> 增加量</param>
    /// <param name="_duration"> 增加时间</param>
    /// <param name="_statsToModify">增加目标状态</param>
    public void IncreaseStatsBy(int _modifier,float _duration,Stats _statsToModify)
    {
        StartCoroutine(IncreaseStats(_modifier, _duration, _statsToModify));
    }

    private IEnumerator IncreaseStats(int _modifier, float _duration, Stats _statsToModify)
    {
        _statsToModify.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statsToModify.RemoveModifier(_modifier);
    }


    public virtual void DoDamager(CharacterStats _targetStats) //伤害计算
    {
        if (TargetCanAvoidAttack(_targetStats))
        {
            return;
        }
        int totalDamage = damage.GetValue() + strength.GetValue();
   
        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);

        }

        totalDamage = CheckTragetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);  

        //魔法伤害测试
        //DoMagicalDamage(_targetStats); 如果后续武器装备附魔 可以普攻造成魔法伤害
    }

    #region Magical damage And ailements

    /// <summary>
    /// 检测魔法伤害
    /// </summary>
    /// <param name="_targetStats"></param>
    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        int totalMagicDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicDamage = CheckTargetResistance(_targetStats, totalMagicDamage);

        _targetStats.TakeDamage(totalMagicDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
        {

            Debug.Log("附加伤害小于0");
            //附加伤害小于等于0  不执行附加伤害判定
            return;
        }
      
        //处理魔法伤害特殊效果 点燃，冰冻，震撼 （取最高的元素伤害为判定）
        AttemptToApplyAilements(_targetStats, _fireDamage, _iceDamage, _lightingDamage);
    }
    /// <summary>
    /// 元素伤害相同时 随机一个状态附加
    /// </summary>
    /// <param name="_targetStats"></param>
    /// <param name="_fireDamage"></param>
    /// <param name="_iceDamage"></param>
    /// <param name="_lightingDamage"></param>
    private void AttemptToApplyAilements(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        #region Test1
        //while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        //{
        //    float randomChance = Random.value;

        //    if(randomChance < 0.33f && _fireDamage > 0)
        //    {
        //        Debug.Log("造成点燃伤害" + _fireDamage);

        //        canApplyIgnite = true;
        //        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
        //        return;
        //    }
        //    else if (randomChance< 0.66f && randomChance>=0.33f && _iceDamage > 0)
        //    {
        //        Debug.Log("造成冰冻" + _iceDamage);


        //        canApplyChill = true;
        //        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
        //        return;
        //    }
        //    else if (randomChance>=0.66f && _lightingDamage > 0)
        //    {
        //        Debug.Log("造成震撼" + _lightingDamage) ;


        //        canApplyShock = true;
        //        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
        //        return;
        //    }

        //}
        #endregion

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < 0.33f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("燃烧状态");
                return;
            }
            if (Random.value < 0.5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("冰冻状态");
                return;
            }
            if (Random.value < 1 && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("震撼状态");
                return;
            }
        }

        if (canApplyIgnite)
        {
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * 0.2f)); //火焰能力的百分之20
        }
        if (canApplyShock)
        {
            _targetStats.SetupShockStrickDamage(Mathf.RoundToInt(_lightingDamage * 0.5f));//雷电伤害的一半
        }

        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

   

    /// <summary>
    /// 应用状态变化
    /// </summary>
    /// <param name="_ignite">是否点燃</param>
    /// <param name="_chill">是否冰冻</param>
    /// <param name="_shock">是否震撼</param>
    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {

        ////当前 如果 有特殊状态 则不会进行叠加
        //if (isIgnited || isChilled || isShocked)
        //    return;
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;


        if (_ignite && canApplyIgnite)
        {
            isIgnited = _ignite;
            igniteTimer = allAlimentDuration; //燃烧时间

          
            fx.IgniteFxFor(allAlimentDuration);
        }
        if (_chill && canApplyChill)
        {
            isChilled = _chill;
            chilledTimer = allAlimentDuration;

            float slowParcentage = 0.2f;
            this.GetComponent<Entity>()?.SlowEnityBy(slowParcentage,allAlimentDuration);
            fx.ChillFxFor(allAlimentDuration);
        }
        if (_shock && canApplyShock)
        {
            if(!isShocked) //闪电会弹出攻击其他敌人
            {
                ApplyShock(_shock);
            }
            else
            {
                //Debug.Log("攻击另外目标 ，寻找最近敌人");

                if (this.GetComponent<Player>() != null)
                {
                    return;
                }

                HitNearestTargetWithShockStrike();

            }
        }

    }
    /// <summary>
    /// 闪电击中效果
    /// </summary>
    /// <param name="_shock"></param>
    public void ApplyShock(bool _shock)
    {
        if (isShocked) //已经处于震撼状态 直接退出
            return;

        isShocked = _shock;
        shockedTimer = allAlimentDuration;

        fx.ShockFxFor(allAlimentDuration);
    }

    /// <summary>
    /// 检测附近最近的敌人 产生雷电攻击
    /// </summary>
    private void HitNearestTargetWithShockStrike()
    {
        //找到最近的敌人 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 25);

        float closetDistance = Mathf.Infinity;
        Transform closetEnemy = null;

        foreach (Collider2D hitInfo in colliders)
        {
            if (hitInfo.GetComponent<Enemy>() != null && Vector2.Distance(this.transform.position, hitInfo.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(this.transform.position, hitInfo.transform.position);

                if (distanceToEnemy < closetDistance)
                {
                    closetDistance = distanceToEnemy;

                    closetEnemy = hitInfo.transform;

                    //Debug.Log("最近敌人是" + closetEnemy.gameObject.name);
                }
            }

            if (closetEnemy == null)
            {
                //Debug.Log("没有检测到敌人");
                closetEnemy = this.transform;
            }

        }

        //实例化闪电 设置闪电
        if (closetEnemy != null)
        {

            //Debug.Log("闪电攻击对象" + closetEnemy.gameObject.name);
            GameObject newShockStrike = GameObject.Instantiate(shockStrickPrefab, transform.position, Quaternion.identity);

            newShockStrike.GetComponent<ShockStrike_Controller>().SetUpStrike(shockDamage, closetEnemy.GetComponent<CharacterStats>());
        }
    }

    /// <summary>
    /// 应用火焰伤害
    /// </summary>
    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0)
        {


            currentHp -= igniteDamage;

            if (currentHp < 0 && !isDead)
            {
                Die();
            }

            igniteDamageTimer = igniteDamageCoolDown;
        }
    }

    public int SetupShockStrickDamage(int _damage)
    {
        shockDamage =_damage;
        return shockDamage;
    }

    /// <summary>
    /// 设置燃烧伤害
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    public int SetupIgniteDamage(int _damage)
    {
        return igniteDamage = _damage;
    }

    #endregion


    /// <summary>
    /// 造成伤害（物理）
    /// </summary>
    /// <param name="_damage"> 总伤害 </param>
    public virtual void TakeDamage(int _damage)
    {
       
        DecreaseHpBy(_damage);
        fx.StartCoroutine("FlashFx");

        GetComponent<Entity>().DamagEffect();
        if (currentHp < 0 && !isDead)
        {
            Die();
       
        }
    }

    /// <summary>
    ///  治疗增加血量
    /// </summary>
    /// <param name="_amount"></param>
    public  virtual void IncreaseHpBy(int _amount)
    {
        currentHp += _amount;
        if(currentHp > GetMaxHealthValue())
        {
            currentHp = GetMaxHealthValue();
        }

        if(onHpChanged != null)
            onHpChanged();
    }

    /// <summary>
    /// 血量减少 -翻转血条
    /// </summary>
    /// <param name="_damage"></param>
    protected virtual void DecreaseHpBy(int _damage)
    {
        currentHp -= _damage;
     
        if(onHpChanged != null)
        {
            onHpChanged();
        }
    }
    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void Die()
    {
        isDead = true;
    }


    #region Stats calculations  数据计算
    /// <summary>
    /// 检测 装备栏数值 减少伤害
    /// </summary>
    /// <param name="_targetStats">装备对象</param>
    /// <param name="totalDamage">总共伤害</param>
    /// <returns></returns>
    private int CheckTragetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if(_targetStats.isChilled)
        {
            //被冻结 减少20% 伤害
            totalDamage -= Mathf.RoundToInt( _targetStats.armor.GetValue() * 0.8f);
        }
        else
        {
            totalDamage -= _targetStats.armor.GetValue();
        }

        totalDamage = Mathf.Clamp(0, totalDamage, int.MaxValue);
        return totalDamage;
    }

    /// <summary>
    /// 检测 智力数值影响
    /// </summary>
    /// <param name="_targetStats"></param>
    /// <param name="totalMagicDamage"></param>
    /// <returns></returns>
    private int CheckTargetResistance(CharacterStats _targetStats, int totalMagicDamage)
    {
        totalMagicDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        // 避免伤害出现小于 0 、
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }
    /// <summary>
    /// 检测能否躲避攻击
    /// </summary>
    /// <param name="_targetStats">玩家对象</param>
    /// <returns></returns>
    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked)
        {
            totalEvasion += 20;
        }

        if (Random.Range(0, 101) < totalEvasion)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    ///   检测 能否暴击
    /// </summary>
    /// <returns></returns>
    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if(Random.Range(0,101)  < totalCriticalChance)
        {
            return true;
        }
        return false;  
    } 
    /// <summary>
    /// 计算暴击伤害
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.01f;

        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    } //暴击伤害

    /// <summary>
    /// 得到最大血量
    /// </summary>
    /// <returns></returns>
    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }

    #endregion



    public virtual Stats GetStats(E_StatType _buffType)
    {

        switch (_buffType)
        {
            case E_StatType.strength: return strength;
            case E_StatType.agility: return agility;
            case E_StatType.intelegence: return intelligence;
            case E_StatType.vitality: return vitality;
            case E_StatType.damage: return damage;
            case E_StatType.critChance: return critChance;
            case E_StatType.critPower: return critPower;
            case E_StatType.maxhealth: return maxHealth;
            case E_StatType.armor: return armor;
            case E_StatType.evasion: return evasion;
            case E_StatType.magicResistance: return magicResistance;
            case E_StatType.fireDamage: return fireDamage;
            case E_StatType.iceDamage: return iceDamage;
            case E_StatType.lightingDamage: return lightingDamage;
        }
        return null;
    }
}
