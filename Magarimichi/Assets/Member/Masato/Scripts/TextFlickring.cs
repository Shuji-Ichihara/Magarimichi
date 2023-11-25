using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlickring : MonoBehaviour
{
    [SerializeField]
    private Text flickingText;
    private bool fadeFlag = false;
    private float a_color = 0;
    // Use this for initialization
    void Start()
    {
        // 最初はフェードアウト
        fadeFlag = true;
        a_color = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //fadeFlagがtrueの間実行する
        if (fadeFlag)
        {
            //テキストの透明度を変更する
            flickingText.color = new Color(0, 0, 0, a_color);
            a_color -= Time.deltaTime;
            //透明度が0になったらフェードアウト処理
            if (a_color < 0)
            {
                a_color = 0;
                fadeFlag = false;
            }
        }
        else if(!fadeFlag)
        {
            //テキストの透明度を変更する
            flickingText.color = new Color(0, 0, 0, a_color);
            a_color += Time.deltaTime;
           
            //透明度が1になったらフェードイン処理
            if (a_color > 1)
            {
                a_color = 1;
                fadeFlag = true;
            }
        }
    }
}
