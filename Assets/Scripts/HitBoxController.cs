using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public KeyCode key;
    private UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindWithTag("Score").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("hit");
        if (Input.GetKey(key))
        {
            Destroy(other.gameObject);
            UI.AddScore(1);
        }
    }
}
