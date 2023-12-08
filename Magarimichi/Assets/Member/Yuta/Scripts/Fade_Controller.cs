using UnityEngine;
using UnityEngine.UI;

public class Fade_Controller : MonoBehaviour
{
    //�����x��ύX����p�l���̃C���[�W
    Image fadeImage;

    //�t�F�[�h�C���E�A�E�g�����̃t���O
    private bool IsFadeIn = false;
    public static bool IsFadeOut = false;

    //�����x���ς��X�s�[�h���Ǘ�
    private float fadeSpeed = 0.01f;
    //�p�l���̐F�A�s�����x���Ǘ�
    private float red, green, blue, alfa;

    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g���C���[�W�����擾
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;    //�ԐF
        green = fadeImage.color.g;  //�ΐF
        blue = fadeImage.color.b;   //�F
        alfa = fadeImage.color.a;   //�����x

        IsFadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //IsFadeIn��true��������
        if (IsFadeIn)
        {
            //��ʂ𖾂邭����
            StartFadeIn();
        }

        //IsFadeOut��true��������
        if (IsFadeOut)
        {
            //��ʂ��Â�����
            StartFadeOut();
        }
    }

    //��ʂ𖾂邭���鏈��
    void StartFadeIn()
    {
        alfa -= fadeSpeed;              //�s�����x�����X�ɉ�����
        SetAlpha();                     //�ύX�����s�����x�p�l���ɔ��f����
        if (alfa <= 0)                  //���S�ɓ����ɂȂ����珈���𔲂���
        {
            IsFadeIn = false;
            fadeImage.enabled = false;   //�p�l���̕\�����I�t�ɂ���
        }
    }

    //��ʂ��Â����鏈��
    void StartFadeOut()
    {
        fadeImage.enabled = true;       //�p�l���̕\�����I���ɂ��� 
        alfa += fadeSpeed;              //�s�����x�����X�ɂ�����
        SetAlpha();                     //�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)                  //���S�ɕs�����ɂȂ����珈���𔲂���
        {
            IsFadeOut = false;
        }
    }

    //�ύX�����l�����������鏈��
    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
