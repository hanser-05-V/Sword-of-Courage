using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;

    [Header("Level details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percantageModfier = 0.2f; // �ȼ����� ��ֵ���ӱ���

    protected override void Start()
    {
        ApplyLevelModifiers();

        base.Start();
        enemy = GetComponent<Enemy>();

        myDropSystem = GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifiers()
    {
        Modify(strength);
    }

    private void Modify(Stats _stat)
    {
        for (int i = 1; i < level; i++)
        {
            //ָ���� ����
            float modifier = _stat.GetValue() * percantageModfier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        enemy.DamagEffect();
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();

        myDropSystem.GenerateDrop();
    }
}
