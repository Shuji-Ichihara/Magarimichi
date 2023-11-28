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
        // �ŏ��̓t�F�[�h�A�E�g
        fadeFlag = true;
        a_color = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //fadeFlag��true�̊Ԏ��s����
        if (fadeFlag)
        {
            //�e�L�X�g�̓����x��ύX����
            flickingText.color = new Color(0, 0, 0, a_color);
            a_color -= Time.deltaTime;
            //�����x��0�ɂȂ�����t�F�[�h�A�E�g����
            if (a_color < 0)
            {
                a_color = 0;
                fadeFlag = false;
            }
        }
        else if(!fadeFlag)
        {
            //�e�L�X�g�̓����x��ύX����
            flickingText.color = new Color(0, 0, 0, a_color);
            a_color += Time.deltaTime;
           
            //�����x��1�ɂȂ�����t�F�[�h�C������
            if (a_color > 1)
            {
                a_color = 1;
                fadeFlag = true;
            }
        }
    }
}
