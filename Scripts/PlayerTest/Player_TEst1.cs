using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TEst1 : MonoBehaviour
{

    public Player_FSM playerFSM {get; private set;} //玩家状态机
    public PlayerInfo playerInfo ; //玩家信息 
    public Player_Test playerTest; //玩家测试类
    [Space]
    public PlayerController playerController ;//玩家控制器

    #region  组件相关

    [SerializeField] public Animator animator ; //动画播放器
    [SerializeField] public Rigidbody2D rb;  //刚体组件
  

    #endregion
    void Awake()
    {
        playerFSM = new Player_FSM(); //实例化玩家状态机

        // playerFSM.stateDic.Add(StateType.Idle,new PlayerIdleState(this,playerFSM,"Idle"));
        // playerFSM.stateDic.Add(StateType.Move,new PlayerMoveTest(this,playerFSM,"Move"));

        playerFSM.InitState(StateType.Idle); //初始化状态
    }
    void Start()
    {
     
    }
    void Update()
    {
        playerFSM.currentState.Onupdate(playerInfo,null);//更新状态状态机
    }
}
