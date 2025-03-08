using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnitityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header(" Flash Info")]
    [SerializeField] private  float flashTime;
    public Material hatMat;
    private Material originalMat;

    [Header("Aliment Color")]
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] shockColor;


    //private Color currentColor;
    void Start()
    {
        sr = this.GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    //等待几秒 过后变成默认材质
    private IEnumerator FlashFx()
    {
        sr.material = hatMat;

        Color currentColor  = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(flashTime);

        sr.color = currentColor;
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if(sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }
    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

    /// <summary>
    /// 执行点燃效果
    /// </summary>
    /// <param name="_second"></param>
    public void IgniteFxFor(float _second)
    {
        InvokeRepeating("IgniteColorFor", 0, 0.3f); // 立即执行 每隔0.3秒执行一次

        Invoke("CancelColorChange", _second);
    }

    /// <summary>
    /// 执行冰冻效果
    /// </summary>
    /// <param name="_second"></param>
    public void ChillFxFor(float _second)
    {
        InvokeRepeating("ChillColorFor", 0, 0.3f); // 立即执行 每隔0.3秒执行一次

        Invoke("CancelColorChange", _second);
    }

    /// <summary>
    /// 执行震撼效果
    /// </summary>
    /// <param name="_second"></param>
    public void ShockFxFor(float _second)
    {
        InvokeRepeating("ShockColorFor", 0, 0.3f); // 立即执行 每隔0.3秒执行一次

        Invoke("CancelColorChange", _second);
    }

    /// <summary>
    /// 点燃颜色变化
    /// </summary>
    private void IgniteColorFor()
    {
        if(sr.color != igniteColor[0])
        {
            sr.color = igniteColor[0];
        }
        else
        {
            sr.color = igniteColor[1];
        }
    }
    /// <summary>
    /// 冰冻颜色变换
    /// </summary>
    private void ChillColorFor()
    {
        if (sr.color != chillColor[0])
        {
            sr.color = chillColor[0];
        }
        else
        {
            sr.color = chillColor[1];
        }
    }
    /// <summary>
    /// 震撼颜色变化
    /// </summary>
    private void ShockColorFor()
    {
        if(sr.color != shockColor[0])
        {
            sr.color = shockColor[0];
        }
        else
        {
            sr.color = shockColor[1];
        }
    }
}
