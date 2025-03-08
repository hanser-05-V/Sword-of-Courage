using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    //�ƶ��ľ���
    private float distanceToMove;
    //�Ӳ�Ч��������Ӳ���ƶ��ٶȣ�
    [SerializeField] private float parallaxEffect;
    //��¼��ʼ��X�������������ͼƬλ��
    private float xPosition;

    //�������ޱ���
    //ͼƬ���������� �ƶ��ľ���
    private float distanceMoved;
    //ͼƬ�ĳ���
    private float length;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        xPosition = this.transform.position.x;
        //bounds.size.x �õ� ����ռ��еĳߴ�
        length = this.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //ͼƬ�ƶ������� ������� parallaxEffect ����1Ϊ��ͬ
        distanceToMove = cam.transform.position.x *  parallaxEffect;
        //����ͼƬλ��
        this.transform.position = new Vector2(xPosition + distanceToMove, this.transform.position.y);
        //�����������ƶ�����
        distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        //�����߽�͸��� ��ͼλ��
        if(distanceMoved > xPosition + length)
            xPosition = distanceMoved + length;
        else if(distanceMoved < xPosition - length)
            xPosition = distanceMoved - length;
    }
}
