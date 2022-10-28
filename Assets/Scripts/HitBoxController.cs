using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public KeyCode key;
    private UIManager UI;

    public Animator burst_anim;
    public Animator A_anim;
    public Animator S_anim;

    // Start is called before the first frame update
    void Start()
    {
        burst_anim = GameObject.Find("burstBG").GetComponent<Animator>();
        A_anim = GameObject.Find("A Box").GetComponent<Animator>();
        S_anim = GameObject.Find("S Box").GetComponent<Animator>();
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
                Debug.Log("bursting in");
                burst_anim.SetBool("burst", true);
                other.gameObject.GetComponent<burstNote>().destroy();
            } else {
                Destroy(other.gameObject);
            }
            if (Input.GetKey("a"))
            {
                A_anim.SetTrigger("hitA");
            } else if (Input.GetKey("s")) {
                S_anim.SetTrigger("hitS");
            }
            UI.AddScore(1);
        }
    }
}
