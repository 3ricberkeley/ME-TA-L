using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public string key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void onCollisionStay(Collision2D other)
    {
        if (Input.GetKey(key))
        {
            Destroy(other.gameObject);
        }
    }
}
