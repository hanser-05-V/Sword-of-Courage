using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))] // 确保挂载在Cinemachine相机上
public class CinemachineParallax : MonoBehaviour
{
    [Header("视差层配置")]
    public Transform[] parallaxLayers; // 拖入前景、中景、背景的父对象（顺序很重要！）
    public float[] parallaxStrengths; // 对应层的视差强度（0~1，越大位移越明显）

    private CinemachineVirtualCamera cinemachineCamera;
    private Vector3 _cameraPosition;

    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        _cameraPosition = cinemachineCamera.transform.position;
    }

    void Update()
    {
        // 获取相机横向偏移量（仅水平视差）
        float cameraOffsetX = cinemachineCamera.transform.localPosition.x;

        // 对每个视差层应用位移
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            Transform layer = parallaxLayers[i];
            float strength = parallaxStrengths[i];
            
            // 计算位移量（屏幕宽度 × 视差强度 × 偏移比例）
            float offsetX = cameraOffsetX * strength * (Screen.width / 1920f); 
            
            // 应用位移（Z轴方向控制远近）
            layer.transform.position += new Vector3(
                offsetX * (layer.gameObject.tag == "Foreground" ? 1 : -1), 
                0, 
                layer.gameObject.tag == "Foreground" ? -0.1f : 0
            );
        }
    }
}