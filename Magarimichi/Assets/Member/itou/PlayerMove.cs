using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    [SerializeField] private float _moveMaxTime;
    [SerializeField] private Image _Keyimage;
    private float colorrock = 1.0f;
    public float MoveSpeed = 3.0f;
    private int direction = 1;
    bool _Up = false;
    bool _Down = false;
    bool _Left = false;
    bool _Right = false;
    // Start is called before the first frame update
    void Start()
    {
        _Keyimage.color = new Color(colorrock, colorrock, colorrock, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveTime >= _moveMaxTime)
        {
            _Up = false ;
            _Down = false ;
            _Left = false ;
            _Right = false ;
            _moveTime = 0 ;
        }
        if (_Up)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + MoveSpeed * Time.deltaTime, this.transform.position.z);
        }
        else if (_Right)
        {
            this.transform.position = new Vector3(this.transform.position.x + MoveSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
        else if (_Down)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - MoveSpeed * Time.deltaTime, this.transform.position.z);
        }
        else if (_Left)
        {
            this.transform.position = new Vector3(this.transform.position.x - MoveSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.W)) 
        { 
            _Up = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _Right = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _Down = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _Left = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            _Keyimage.color = new Color(colorrock,colorrock,colorrock,colorrock);
        }
    }
}
