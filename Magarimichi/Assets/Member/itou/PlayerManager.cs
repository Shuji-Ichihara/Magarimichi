using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Image _Keyimage;
    [SerializeField] private float _playerMoveMax;
    [SerializeField] private float _movestartMax;
    private SpriteRenderer _mapSprite;
    private float _playerMove;
    private float _movestart;
    private float colorrock = 1.0f;
    private float MoveSpeed = 1.0f;
    private int direction = 1;
    public bool _Up;
    public bool _Down;
    public bool _Left;
    public bool _Right;
    public bool _UpStart;
    public bool _DownStart;
    public bool _LeftStart;
    public bool _RightStart;
    private bool _GetKey;

    private void Start()
    {
        _Keyimage = GameObject.Find("Key").GetComponent<Image>();
        _Keyimage.color = new Color(colorrock, colorrock, colorrock, 0.3f);
    }
    void FixedUpdate()
    {
        _movestart++;
        if (_playerMoveMax <= _playerMove)
        {
            _UpStart = false;
            _DownStart = false;
            _LeftStart = false;
            _RightStart = false;
            _Up = false;
            _Down = false;
            _Left = false;
            _Right = false;
            _playerMove = 0;
        }
        if (_UpStart)
        {
            _playerMove++;
            direction = 1;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.z);
        }
        else if (_DownStart)
        {
            _playerMove++;
            direction = -1;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.z);
        }
        else if (_RightStart)
        {
            _playerMove++;
            direction = 1;
            this.transform.position = new Vector3(this.transform.position.x + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.y, this.transform.position.z);
        }
        else if (_LeftStart)
        {
            _playerMove++;
            direction = -1;
            this.transform.position = new Vector3(this.transform.position.x + MoveSpeed * Time.fixedDeltaTime * direction, this.transform.position.y, this.transform.position.z);
        }
        if (_Right)
        {
            _RightStart = true;
            _UpStart = false;
            _DownStart = false;
            _LeftStart = false;
        }
        else if (_Down)
        {
            _DownStart = true;
            _UpStart = false;
            _LeftStart = false;
        }
        else if (_Left)
        {
            _LeftStart = true;
            _UpStart = false;
        }
        else if (_Up)
        {
            _UpStart = true;
        }
        if(_movestartMax < _movestart)
        {
            _movestart = 0;
            switch (_mapSprite.sprite.name)
            {
                case "Magarimichi_MapChip_Start_Down_Right":
                    _Right = true;
                    _Down = true;
                    break;
                case "Magarimichi_MapChip_Down_Left":
                    _Down = true;
                    _Left = true;
                    break;
                case "Magarimichi_MapChip_Down_Left_Right":
                    _Right = true;
                    _Down = true;
                    _Left = true;
                    break;
                case "Magarimichi_MapChip_Key_Up_Down_Left_Right":
                    _Up = true;
                    _Down = true;
                    _Left = true;
                    _Right = true;
                    break;
                case "Magarimichi_MapChip_Left_Right":
                    _Right = true;
                    _Left = true;
                    break;
                case "Magarimichi_MapChip_Up_Down":
                    _Down = true;
                    _Up = true;
                    break;
                case "Magarimichi_MapChip_Up_Down_Left":
                    _Down = true;
                    _Left = true;
                    _Up = true;
                    break;
                case "Magarimichi_MapChip_Up_Down_Right":
                    _Right = true;
                    _Down = true;
                    _Up = true;
                    break;
                case "Magarimichi_MapChip_Up_Left":
                    _Left = true;
                    _Up = true;
                    break;
                case "Magarimichi_MapChip_Up_Left_Right":
                    _Right = true;
                    _Left = true;
                    _Up = true;
                    break;
                case "Magarimichi_MapChip_Up_RIght":
                    _Right = true;
                    _Up = true;
                    break;
                case "Magarimichi_MapChip_Goal_Up":
                    break;
                default:
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            _GetKey = true;
            _Keyimage.color = new Color(colorrock, colorrock, colorrock, colorrock);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Lock") && _GetKey == true)
        {
            _GetKey = false;
            _Keyimage.color = new Color(colorrock, colorrock, colorrock, 0.3f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Lock") && _GetKey == false)
        {
            _UpStart = false;
            _DownStart = false;
            _LeftStart = false;
            _RightStart = false;
            _Up = false;
            _Down = false;
            _Left = false;
            _Right = false;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Map"))
        {
            _mapSprite = other.gameObject.GetComponent<SpriteRenderer>();
        }
    }
}
