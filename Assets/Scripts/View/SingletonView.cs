using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonView<T> : ViewBase where T : SingletonView<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            // Debug.Log(typeof(T).Name);
            if (_instance == null)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/View/" + typeof(T).Name));
            }
            return _instance;


            //齐老师的办法是单例就是一个管理类，只需把脚本挂在一个物体上即可；但是这里却是单例是一套物体，包含父子各种关系，所以就不适用齐老师的manager管理的单例。这是界面的单例。我这还是头一回这么用，因为实际上unity是单独加载了场景，所以我想正确的做法可能就是给loading界面放在一个场景里，单独加载。但是怎么传入进度条呢？mike的案例是做一个渐入渐出的动画，这显然是不是真实项目的做法。
        }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        _instance = null;
    }

}
