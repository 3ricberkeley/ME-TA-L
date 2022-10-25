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
        if (Input.GetKey(key))
        {
            if (other.gameObject.CompareTag("burst")) {
                other.gameObject.GetComponent<burstNote>().destroy();
            } else {
                Destroy(other.gameObject);
            }
            UI.AddScore(1);
        }
    }
}
