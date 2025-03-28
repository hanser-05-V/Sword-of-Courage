using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    /// <summary>
    /// typeof(T).Name.ToString() 为挂在这个脚本对象的名字
    /// typeof(T).ToString() 为继承对象的脚本名字
    /// </summary>
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                //创建对象
                GameObject obj = new GameObject();
                //obj.name = typeof(T).Name.ToString();
                obj.name = typeof(T).ToString();
                instance = obj.AddComponent<T>();
                //防止切换场景过后再切回来，场景有多个挂载单例模式对象
                //破坏唯一性原则
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

}
