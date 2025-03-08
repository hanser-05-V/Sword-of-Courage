using UnityEngine;

public enum E_SwordType
{
    Regular,
    Bounce,
    Pierce,//穿刺
    Spin,//旋转
}

public class Sword_Skill : SkillBase
{
    public E_SwordType swordType;
    [Header("Bounce Info")]
    [SerializeField] private int amountofBounce;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;

    [Header("Peirce Info")]
    [SerializeField] private int periceAmount;
    [SerializeField] private float periceGravity;

    [Header("Spin Info")]
    [SerializeField] private float hitCoolDown = 0.35f;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float spinGravity = 1;


    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir;//投掷方向 (力的大小)
    [SerializeField] private float swordGravity; //武器重力
    [SerializeField] private float returnSpeed;
    [SerializeField] private float freeTimeDuration;

    GameObject newSword;
    Sword_Skill_Controller swordController;

    //最终的方向
    private Vector2 finalDir;

    [Header("Aim Dots")]
    [SerializeField] private int dotsNumber;
    [SerializeField] private float spaceBetWeenDots;//间隔距离
    [SerializeField] private GameObject dotPrefabs;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenereateDots();

        SetupGravity();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(1))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchDir.x, AimDirection().normalized.y * launchDir.y);
        }

        //创建点
        if (Input.GetMouseButton(1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetWeenDots);
            }
        }
    }
    public void CreatSword()
    {
        newSword = GameObject.Instantiate(swordPrefab, player.transform.position, transform.rotation);
        swordController = newSword.GetComponent<Sword_Skill_Controller>();

        if (swordType == E_SwordType.Bounce)
        {
            swordController.SetupBounce(true, amountofBounce, bounceSpeed);
        }
        else if (swordType == E_SwordType.Pierce)
        {
            swordController.SetupPerice(periceAmount);
        }
        else if (swordType == E_SwordType.Spin)
        {
            swordController.SetupSpin(true, maxTravelDistance, spinDuration, hitCoolDown);
        }
        swordController.SetupSword(finalDir, swordGravity, player, freeTimeDuration, returnSpeed);
        player.AssignNewSword(newSword);
        DotsActive(false);
    }

    private void SetupGravity()
    {
        switch (swordType)
        {
            case E_SwordType.Regular:
                break;
            case E_SwordType.Bounce:
                swordGravity = bounceGravity;
                break;
            case E_SwordType.Pierce:
                swordGravity = periceGravity;
                break;
            case E_SwordType.Spin:
                swordGravity = spinGravity;
                break;
        }

    }
    #region Aim Direction
    //根据开始位置 结束位置 确定方向
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }
    //生成点
    private void GenereateDots()
    {
        dots = new GameObject[dotsNumber];
        for (int i = 0; i < dotsNumber; i++)
        {
            dots[i] = Instantiate(dotPrefabs, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);//创建好先隐藏
        }
    }
    //抛物线公式 x = vt + (1/2) a * t * t
    private Vector2 DotsPosition(float t)
    {
        Vector2 positon = (Vector2)player.transform.position + new Vector2(
                           AimDirection().normalized.x * launchDir.x,
                           AimDirection().normalized.y * launchDir.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);
        return positon;
    }
    #endregion
}
