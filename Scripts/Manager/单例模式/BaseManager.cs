using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BaseManager<T> where T : class //new()
{
    private static T instance;

    public static T Instance
    {

        get
        {
            if (instance == null)
            {
                 // instance = new T();
                //限定继承BaseManager的类必须显示实现私有构造函数
                Type type = typeof(T);
                ConstructorInfo  info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                             null,
                                                             Type.EmptyTypes,
                                                             null);
                instance = info.Invoke(null) as T; 
            }

            return instance;
        }
        
    }
}
