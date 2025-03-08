using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ice And Fire", menuName = "Data/Item Effect/Ice And Fire")]
public class IceAndFire_Effect : ItemEffect
{

    [SerializeField] private GameObject iceAndFirePrefabs;
    [SerializeField] private float xVelocity;

    private Player player;
    public override void ApplyEffect(Transform _targetPos)
    {
    
    
        player = PlayerManager.Instance.player;

        bool isThirdAttack = player.attackState.attackCounter == 2;

        if(isThirdAttack)
        {

            GameObject newIceAndFire = GameObject.Instantiate(iceAndFirePrefabs, _targetPos.transform.position, player.transform.rotation);

            newIceAndFire.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.faceDir, 0);
            Destroy(newIceAndFire,10);
           
        }

    }
}
