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
    public Stats strength; //����
    public Stats agility;  //���� %
    public Stats intelligence; // ���� 1����������һ��ħ���˺� 3��ħ��
    public Stats vitality; //����

    [Header("Offensive stats")]
    public Stats damage;
    public Stats critChance; // ������
    public Stats critPower; //�����˺�

    [Header("Defensive stats")]
    public Stats maxHealth;
    public Stats armor; // ����
    public Stats evasion; // ����
    public Stats magicResistance; // ħ��

    [Header("Magic stats")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightingDamage;
    [Space]
    

    public bool isIgnited; //��ȼ ��������˺�
    public bool isChilled; //���� ���Ϳ���20%����
    public bool isShocked; //�� ���ٹ���������  ����������ܸ���

    [SerializeField] private float allAlimentDuration;
    private float igniteTimer;
    private float chilledTimer;
    private float shockedTimer;

    private float igniteDamageCoolDown = 0.3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    [SerializeField] private GameObject shockStrickPrefab;
    private int shockDamage;
    


    //��ǰѪ����Ѫ����תί��
    public int currentHp;
    public UnityAction onHpChanged;

    public bool isDead { get; private set; }

    protected virtual void Awake()
    {
        fx = this.GetComponent<EnitityFX>();    
    }
    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);//Ĭ�ϱ���Ϊ 150%
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
    /// Buff ����Ч��
    /// </summary>
    /// <param name="_modifier"> ������</param>
    /// <param name="_duration"> ����ʱ��</param>
    /// <param name="_statsToModify">����Ŀ��״̬</param>
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


    public virtual void DoDamager(CharacterStats _targetStats) //�˺�����
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

        //ħ���˺�����
        //DoMagicalDamage(_targetStats); �����������װ����ħ �����չ����ħ���˺�
    }

    #region Magical damage And ailements

    /// <summary>
    /// ���ħ���˺�
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

            Debug.Log("�����˺�С��0");
            //�����˺�С�ڵ���0  ��ִ�и����˺��ж�
            return;
        }
      
        //����ħ���˺�����Ч�� ��ȼ���������� ��ȡ��ߵ�Ԫ���˺�Ϊ�ж���
        AttemptToApplyAilements(_targetStats, _fireDamage, _iceDamage, _lightingDamage);
    }
    /// <summary>
    /// Ԫ���˺���ͬʱ ���һ��״̬����
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
        //        Debug.Log("��ɵ�ȼ�˺�" + _fireDamage);

        //        canApplyIgnite = true;
        //        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
        //        return;
        //    }
        //    else if (randomChance< 0.66f && randomChance>=0.33f && _iceDamage > 0)
        //    {
        //        Debug.Log("��ɱ���" + _iceDamage);


        //        canApplyChill = true;
        //        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
        //        return;
        //    }
        //    else if (randomChance>=0.66f && _lightingDamage > 0)
        //    {
        //        Debug.Log("�����" + _lightingDamage) ;


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
                Debug.Log("ȼ��״̬");
                return;
            }
            if (Random.value < 0.5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("����״̬");
                return;
            }
            if (Random.value < 1 && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("��״̬");
                return;
            }
        }

        if (canApplyIgnite)
        {
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * 0.2f)); //���������İٷ�֮20
        }
        if (canApplyShock)
        {
            _targetStats.SetupShockStrickDamage(Mathf.RoundToInt(_lightingDamage * 0.5f));//�׵��˺���һ��
        }

        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

   

    /// <summary>
    /// Ӧ��״̬�仯
    /// </summary>
    /// <param name="_ignite">�Ƿ��ȼ</param>
    /// <param name="_chill">�Ƿ����</param>
    /// <param name="_shock">�Ƿ���</param>
    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {

        ////��ǰ ��� ������״̬ �򲻻���е���
        //if (isIgnited || isChilled || isShocked)
        //    return;
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;


        if (_ignite && canApplyIgnite)
        {
            isIgnited = _ignite;
            igniteTimer = allAlimentDuration; //ȼ��ʱ��

          
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
            if(!isShocked) //����ᵯ��������������
            {
                ApplyShock(_shock);
            }
            else
            {
                //Debug.Log("��������Ŀ�� ��Ѱ���������");

                if (this.GetComponent<Player>() != null)
                {
                    return;
                }

                HitNearestTargetWithShockStrike();

            }
        }

    }
    /// <summary>
    /// �������Ч��
    /// </summary>
    /// <param name="_shock"></param>
    public void ApplyShock(bool _shock)
    {
        if (isShocked) //�Ѿ�������״̬ ֱ���˳�
            return;

        isShocked = _shock;
        shockedTimer = allAlimentDuration;

        fx.ShockFxFor(allAlimentDuration);
    }

    /// <summary>
    /// ��⸽������ĵ��� �����׵繥��
    /// </summary>
    private void HitNearestTargetWithShockStrike()
    {
        //�ҵ�����ĵ��� 
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

                    //Debug.Log("���������" + closetEnemy.gameObject.name);
                }
            }

            if (closetEnemy == null)
            {
                //Debug.Log("û�м�⵽����");
                closetEnemy = this.transform;
            }

        }

        //ʵ�������� ��������
        if (closetEnemy != null)
        {

            //Debug.Log("���繥������" + closetEnemy.gameObject.name);
            GameObject newShockStrike = GameObject.Instantiate(shockStrickPrefab, transform.position, Quaternion.identity);

            newShockStrike.GetComponent<ShockStrike_Controller>().SetUpStrike(shockDamage, closetEnemy.GetComponent<CharacterStats>());
        }
    }

    /// <summary>
    /// Ӧ�û����˺�
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
    /// ����ȼ���˺�
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    public int SetupIgniteDamage(int _damage)
    {
        return igniteDamage = _damage;
    }

    #endregion


    /// <summary>
    /// ����˺�������
    /// </summary>
    /// <param name="_damage"> ���˺� </param>
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
    ///  ��������Ѫ��
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
    /// Ѫ������ -��תѪ��
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
    /// ����
    /// </summary>
    protected virtual void Die()
    {
        isDead = true;
    }


    #region Stats calculations  ���ݼ���
    /// <summary>
    /// ��� װ������ֵ �����˺�
    /// </summary>
    /// <param name="_targetStats">װ������</param>
    /// <param name="totalDamage">�ܹ��˺�</param>
    /// <returns></returns>
    private int CheckTragetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if(_targetStats.isChilled)
        {
            //������ ����20% �˺�
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
    /// ��� ������ֵӰ��
    /// </summary>
    /// <param name="_targetStats"></param>
    /// <param name="totalMagicDamage"></param>
    /// <returns></returns>
    private int CheckTargetResistance(CharacterStats _targetStats, int totalMagicDamage)
    {
        totalMagicDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        // �����˺�����С�� 0 ��
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }
    /// <summary>
    /// ����ܷ��ܹ���
    /// </summary>
    /// <param name="_targetStats">��Ҷ���</param>
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
    ///   ��� �ܷ񱩻�
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
    /// ���㱩���˺�
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.01f;

        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    } //�����˺�

    /// <summary>
    /// �õ����Ѫ��
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
