using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kay : MonoBehaviour
{
    [SerializeField]
    private GameObject _kay;
    [SerializeField]
    private Key _key;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Kay"))
        {
            _key.KeyPostion();
            Debug.Log("aaaa");
            
        }
    }
}
