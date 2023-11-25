using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
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
        }
    }
}
