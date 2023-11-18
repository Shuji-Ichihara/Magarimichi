using System;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                Type t = typeof(T);
                _instance = (T)FindObjectOfType(t);
                if(_instance == null)
                {
                    Debug.LogError($"{t} をアタッチしているゲームオブジェクトはありません。");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているかを調べる。
        // アタッチされている場合は破棄する。
        if(this != Instance)
        {
            Destroy(this);
            Debug.LogError(
                $"{typeof(T)} は既に他のゲームオブジェクトにアタッチされている為、コンポーネントを破棄しました。" +
                $"現在アタッチされているゲームオブジェクトは、{Instance.gameObject.name} です。");
            return;
        }
    }
}
