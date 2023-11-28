using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private Transform _position;
    public void KeyPostion()
    {
        transform.position = _position.position;
    }
}
