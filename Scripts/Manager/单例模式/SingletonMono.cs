using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T instance;
    public static T Instance 
    {
        get
        {
            return instance;
        }
    
    }
    protected virtual void Awake()
    {
        ////避免重复挂载
        //if (instance != null)
        //    Destroy(this);
        instance = this as T;
        //保证唯一性，确保继承脚本的对象过场景不删除
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
