using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private int _movespeed;
    [SerializeField] private Image _Keyimage;
    [SerializeField] private GameObject playermoves;
    private Transform _movetransfrom;
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
        _movetransfrom = playermoves.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            _Up = true;
            _movetransfrom.position = new Vector3(this.transform.position.x, this.transform.position.y + _movespeed, this.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _Right = true;
            _movetransfrom.position = new Vector3(this.transform.position.x + _movespeed, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _Down = true;
            _movetransfrom.position = new Vector3(this.transform.position.x, this.transform.position.y - _movespeed, this.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _Left = true;
            _movetransfrom.position = new Vector3(this.transform.position.x - _movespeed, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.y >= _movetransfrom.position.y)
        {
            _Up = false ;
        }
        if (this.transform.position.x >= _movetransfrom.position.x)
        {
            _Right = false;
        }
        if (this.transform.position.y <= _movetransfrom.position.y)
        {
            _Down = false;
        }
        if (this.transform.position.x <= _movetransfrom.position.x)
        {
            _Left = false;
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
