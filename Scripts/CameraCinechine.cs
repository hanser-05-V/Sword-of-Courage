using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraCinechine : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingTransposer; 

    public float triggerAfterH = 0.25f;
    public float triggerAfterW = 0.25f;
    public float defolutHeight = 0.25f; //竖直方向不追踪
    public float defolutWidth = 0.25f;
    public static bool isPlayerEnter = false;
    void Start()
    {
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           
            if(!isPlayerEnter)
            {

                // framingTransposer.m_DeadZoneHeight = Mathf.Lerp(framingTransposer.m_DeadZoneHeight,defolutHeight,changeSpeed*Time.deltaTime);
                // framingTransposer.m_DeadZoneWidth = Mathf.Lerp(framingTransposer.m_DeadZoneWidth,defolutWidth,changeSpeed*Time.deltaTime);
                framingTransposer.m_DeadZoneHeight = defolutHeight;
                framingTransposer.m_DeadZoneWidth = defolutWidth;
                isPlayerEnter = true;
            }
            else //触发过后又变成初始状态
            {
                // framingTransposer.m_DeadZoneHeight = Mathf.Lerp(framingTransposer.m_DeadZoneHeight,triggerAfterH,changeSpeed*Time.deltaTime);
                // framingTransposer.m_DeadZoneWidth = Mathf.Lerp(framingTransposer.m_DeadZoneWidth,triggerAfterW,changeSpeed*Time.deltaTime);
                framingTransposer.m_DeadZoneHeight = triggerAfterH;
                framingTransposer.m_DeadZoneWidth = triggerAfterW;
                isPlayerEnter = false;
            }
        }
    }
}
