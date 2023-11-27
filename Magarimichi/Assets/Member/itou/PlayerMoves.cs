using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private Image _Keyimage;
    [SerializeField] private float _playerMoveMax;
    private float _playerMove;
    private float colorrock = 1.0f;
    private float MoveSpeed = 1.0f;
    private int direction = 1;
    private bool _Up;
    private bool _Down;
    private bool _Left;
    private bool _Right;
    private bool _GetKey;

    private void Start()
    {
        _Keyimage.color = new Color(colorrock, colorrock, colorrock, 0.3f);
    }
    void FixedUpdate()
    {

        if (_playerMoveMax <= _playerMove)
        {
            _Up = false;
            _Down = false;
            _Left = false;
            _Right = false;
            _playerMove = 0;
        }
        if (_Up)
        {
            _playerMove++;
            direction = 1;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.z);
        }
        else if (_Down)
        {
            _playerMove++;
            direction = -1;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.z);
        }
        else if (_Right)
        {
            _playerMove++;
            direction = 1;
            this.transform.position = new Vector3(this.transform.position.x + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.y, this.transform.position.z);
        }
        else if (_Left)
        {
            _playerMove++;
            direction = -1;
            this.transform.position = new Vector3(this.transform.position.x + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _Up = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _Down = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _Right = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _Left = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            _GetKey = true;
            _Keyimage.color = new Color(colorrock, colorrock, colorrock, colorrock);
            Destroy(other.gameObject);
        }
    }
}
