using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //�Ăяo�����̗�ł��B
    //�Q�[���N���A���͕ς��Ǝv���܂����A�d�g�݂͑�̈ꏏ�Ȃ̂ŁA���v���Ǝv���܂��B
    //�킩��Ȃ���΁A���߂̘A����

    GameObject Scene;

    public int Noumber;

    public void SceneMoving()
    {
        Fade_Controller.IsFadeOut = true;
        StartCoroutine("SceneMove");
    }

    public void Finish()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    IEnumerator SceneMove()
    {
        yield return new WaitForSeconds(3);

        Scene = GameObject.FindGameObjectWithTag("ScneManger");
        switch (Noumber)
        {
            case 0:
                Scene.GetComponent<SceneManger>()._LoadScene(Name.TitleScene);
                break;
            case 1:
                Scene.GetComponent<SceneManger>()._LoadScene(Name.StageSelect);
                break;
            case 2:
                Scene.GetComponent<SceneManger>()._LoadScene(Name.Main1);
                break;
            case 3:
                Scene.GetComponent<SceneManger>()._LoadScene(Name.Main2);
                break;
            case 4:
                Scene.GetComponent<SceneManger>()._LoadScene(Name.Main3);
                break;
            case 5:
                Scene.GetComponent<SceneManger>()._LoadScene(Name.ResultScene);
                break;
        }
    }
}
