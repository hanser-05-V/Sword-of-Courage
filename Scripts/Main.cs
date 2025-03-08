//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Main : MonoBehaviour
//{
//    //用于测试功能
//    public GameObject Ui;

//    public Player player;

//    private void Awake()
//    {
       
//    }
//    private void Update()
//    {
//        if(Input.GetKeyDown(KeyCode.B))
//        {
//            Ui.SetActive(true);
//            player.isBusy = true;
//        }
    
//        if(Input.GetKeyDown(KeyCode.M)) 
//        {
//            Ui.SetActive(false);
//            player.isBusy = false;
        
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            player.skill.sword.swordType = E_SwordType.Bounce;
//        }
//        if(Input.GetKeyDown (KeyCode.Alpha2))
//        {
//            player.skill.sword.swordType = E_SwordType.Pierce;
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha3))
//        {
//            player.skill.sword.swordType = E_SwordType.Spin;
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha4))
//        {
            
//            player.skill.sword.swordType = E_SwordType.Regular;
//        }


//        if(Input.GetKeyDown(KeyCode.L))
//        {
//            player.canClone = true;
//        }
//        if(Input.GetKeyDown(KeyCode.K))
//        {
//            player.canClone = false;
//        }

//    }


//}
