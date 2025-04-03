using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraCinechine : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineFramingTransposer framingTransposer; 

    public float triggerAfterH = 0.25f;
    public float triggerAfterW = 0.25f;
    public float defolutHeight = 0.25f;
    public float defolutWidth = 0.25f;
    [SerializeField] public bool isPlayerEnter = false;
    void Start()
    {
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("玩家进入");
            if(!isPlayerEnter)
            {
                framingTransposer.m_DeadZoneHeight = defolutHeight;
                framingTransposer.m_DeadZoneWidth = defolutWidth;
                isPlayerEnter = true;
            }
            else
            {
                framingTransposer.m_DeadZoneHeight = triggerAfterH;
                framingTransposer.m_DeadZoneWidth = triggerAfterW;
            }
        }
    }
}
