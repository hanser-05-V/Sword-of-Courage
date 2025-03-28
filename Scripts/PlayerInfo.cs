using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    [Header ("移动相关")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float jumpTime = 0.5f;
    public float jumpMinHeight = 0.5f;

    public float jumpDuration = 1f ; // 跳跃持续时间

    public float repeatYvelocityMin ; //播放重复片段的最小y速度
    public float downYvelocityMin ; //播放下落片段的最小y速度


    [Header("冲刺相关")]
    public float dashSpeed = 10f;
}
