using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    [SerializeField]
    List<string> SceneNumber = new List<string>();

    public void _LoadScene(Name name)
    {
        SceneManager.LoadScene(SceneNumber[(int)name]);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroy();
    }

    void DontDestroy()
    {
        //�I�u�W�F�N�g���r���Ŕj������Ȃ��p�ɂ��鏈��
        //(�����^�O�̃I�u�W�F�N�g����������j������p�ɂȂ��Ă�B)
        GameObject ScneManger = CheckOtherObject();
        bool checkObject = ScneManger != null && ScneManger != gameObject;

        if (checkObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //�V�[�����ɁA�����^�O�̃I�u�W�F�N�g���������𒲂ׂ郁�\�b�h
    GameObject CheckOtherObject()
    {
        return GameObject.FindGameObjectWithTag("ScneManger");
    }
}
