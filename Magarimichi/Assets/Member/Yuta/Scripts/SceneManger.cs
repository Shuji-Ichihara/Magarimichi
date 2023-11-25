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
        //オブジェクトが途中で破棄されない用にする処理
        //(同じタグのオブジェクトがあったら破棄する用になってる。)
        GameObject ScneManger = CheckOtherObject();
        bool checkObject = ScneManger != null && ScneManger != gameObject;

        if (checkObject)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //シーン内に、同じタグのオブジェクトが無いかを調べるメソッド
    GameObject CheckOtherObject()
    {
        return GameObject.FindGameObjectWithTag("ScneManger");
    }
}
