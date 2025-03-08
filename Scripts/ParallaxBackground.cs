using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    //移动的距离
    private float distanceToMove;
    //视差效果（造成视差的移动速度）
    [SerializeField] private float parallaxEffect;
    //记录开始的X，方便后续更新图片位置
    private float xPosition;

    //创建无限背景
    //图片相对于摄像机 移动的距离
    private float distanceMoved;
    //图片的长度
    private float length;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        xPosition = this.transform.position.x;
        //bounds.size.x 得到 世界空间中的尺寸
        length = this.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //图片移动距离是 摄像机的 parallaxEffect 倍，1为相同
        distanceToMove = cam.transform.position.x *  parallaxEffect;
        //更新图片位置
        this.transform.position = new Vector2(xPosition + distanceToMove, this.transform.position.y);
        //相对摄像机的移动距离
        distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        //超出边界就更新 主图位置
        if(distanceMoved > xPosition + length)
            xPosition = distanceMoved + length;
        else if(distanceMoved < xPosition - length)
            xPosition = distanceMoved - length;
    }
}
