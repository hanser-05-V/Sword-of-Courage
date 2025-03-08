using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{

    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList = new List<KeyCode>(); //�趨���ֵİ���   

    private float maxScale;
    private float growSpeed;
    private float shrikSpeed;

    private bool canGrow =true;
    private bool canShrink;
    private bool canCreateHotkeys =true;
    private bool canAttack;
    public bool playCanEixtBlackhole { get; private set; }
    private bool playerCanDispear = true;

    private float attackAmount =4;
    private float cloneAttackCoolDown = 0.3f;
    private float cloneAttackTimer;

    private float blackholeTimer;

    private int cloneRandomIndex;
    private float xOffset;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> creatKeyHotList = new List<GameObject>();

    public void SetupBlackhole(float _maxScale, float _growSpeed, float _shrikSpeed, int _attackAmount, float _cloneAttackCoolDown,float _blackholeDuration)
    {
        this.maxScale = _maxScale;
        this.growSpeed = _growSpeed;
        this.shrikSpeed = _shrikSpeed;
        this.attackAmount = _attackAmount;
        this.cloneAttackCoolDown = _cloneAttackCoolDown;
        this.blackholeTimer = _blackholeDuration;

        if (SkillManager.Instance.clone.crystalInsteadClone)
        {
            playerCanDispear = false;
            PlayerManager.Instance.player.fx.MakeTransprent(false);
        }
    }

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;

        if(blackholeTimer < 0 )
        {
            blackholeTimer = Mathf.Infinity;
            if(targets.Count > 0)
            {
                CloneAttack();
            }
            else
            {
             
                FinishCloneAttack();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CloneAttack();
        }

        CloneAttackLogic();

        if (canGrow && !canShrink)
        {
            this.transform.localScale = Vector2.Lerp(this.transform.localScale, new Vector2(maxScale, maxScale), growSpeed * Time.deltaTime);
        }
        if (canShrink)
        {
            this.transform.localScale = Vector2.Lerp(this.transform.localScale, new Vector2(-1, -1), shrikSpeed * Time.deltaTime);
            if (this.transform.localScale.x < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void CloneAttack()
    {
        if (targets.Count <= 0)
            return;
        if (playerCanDispear)
        {
            playerCanDispear = false;
            PlayerManager.Instance.player.fx.MakeTransprent(true);
        }
        canAttack = true;
        canCreateHotkeys = false;
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && canAttack && attackAmount>=0)
        {
            cloneAttackTimer = cloneAttackCoolDown;

            cloneRandomIndex = Random.Range(0, targets.Count);
            xOffset = Random.Range(0, 100) > 50 ? 1 : -1;

            //���ˮ���ʹ���ˮ��

            if (SkillManager.Instance.clone.crystalInsteadClone)
            {
                // SkillManager.Instance.crystal.CreatCrystal();
                // SkillManager.Instance.crystal.CrystalChooseRandomEnemy();
                // //SkillManager.Instance.     
            }
            else
            {
                 SkillManager.Instance.clone.CreatClone(targets[cloneRandomIndex], new Vector3(xOffset, 0));
            }
            attackAmount--;
            if (attackAmount <= 0)
            {
                Invoke("FinishCloneAttack", 0.8f);
            }
        }
    }

    private void FinishCloneAttack()
    {
        DestoryHotKeys();
        playCanEixtBlackhole = true;
        //PlayerManager.Instance.player.ExitBlackholeState();
        canAttack = false;
        canShrink = true;
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.GetComponent<Enemy>()!= null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            //�����ȼ�
            CreateHotKey(collision);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision.GetComponent<Enemy>()!= null )
        {
            collision.GetComponent <Enemy>().FreezeTime(false);
        }
    }

    public void AddEnemyToList(Transform _enemy)
    {
        targets.Add(_enemy);
    }
    private void CreateHotKey(Collider2D collision)
    {
        if(keyCodeList.Count < 0)
        {
            Debug.LogError("û�м�¼ �� �ȼ�");
            return;
        }

        if (!canCreateHotkeys)
            return;

        GameObject newHotKey = GameObject.Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        creatKeyHotList.Add(newHotKey);


        KeyCode chooseKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(chooseKey);

        Blackhole_HotKey_Controller newHotKeyController = newHotKey.GetComponent<Blackhole_HotKey_Controller>();

        newHotKeyController.SetupHotKey(chooseKey, collision.transform, this);
    }
    private void DestoryHotKeys()
    {
        if (creatKeyHotList.Count < 0)
            return;
        for(int i = 0; i < creatKeyHotList.Count; i++)
        {
            Destroy(creatKeyHotList[i]);
            //creatKeyHotList.Remove(creatKeyHotList[i]);
        }
    }
}
