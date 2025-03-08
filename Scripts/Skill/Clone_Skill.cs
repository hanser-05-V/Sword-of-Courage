using System.Collections;
using UnityEngine;

public class Clone_Skill : SkillBase
{

    [Header("Clone Info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;
    [Space]
    [SerializeField] private bool creatCloneDashStart;
    [SerializeField] private bool creatClonrDashOver;
    [SerializeField] private bool creatCloneOnCounterAttack;

    [Header("Clone Duplicate Info")]
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;

    [Header("Clone Crystal")]
    public bool crystalInsteadClone;

    public void CreatClone(Transform positon, Vector3 _offset)
    {

        GameObject newClone = GameObject.Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(positon, cloneDuration, canAttack, _offset,
                                                                   FindClosetEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate);
    }

    public void CreatCloneOnDashStart()
    {
        if (creatCloneDashStart)
        {
            CreatClone(player.transform, Vector3.zero);
        }
    }

    public void CreatCloneOnDashOver()
    {
        if(creatClonrDashOver)
        {
            CreatClone(player.transform, Vector3.zero);
        }
    }

    public void CreatCloneOnCounterAttack(Transform _transform)
    {
        if (creatCloneOnCounterAttack)
        {
            StartCoroutine(CreatCloneOnCounterAttack(_transform, new Vector3(2 * player.faceDir, 0)));
        }
    }

    private IEnumerator CreatCloneOnCounterAttack(Transform _transform,Vector3 _offset)
    {
        yield return new WaitForSeconds(0.4f);

        CreatClone(_transform, _offset);
    }

}
