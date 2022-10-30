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
    public Animator K_anim;
    public Animator L_anim;

    // Start is called before the first frame update
    void Start()
    {
        burst_anim = GameObject.Find("burstBG").GetComponent<Animator>();
        A_anim = GameObject.Find("A Box").GetComponent<Animator>();
        S_anim = GameObject.Find("S Box").GetComponent<Animator>();
        K_anim = GameObject.Find("K Box").GetComponent<Animator>();
        L_anim = GameObject.Find("L Box").GetComponent<Animator>();
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
            }
            if (Input.GetKey("s")) {
                S_anim.SetTrigger("hitS");
            }
            if (Input.GetKey("k")) {
                K_anim.SetTrigger("hitK");
            }
            if (Input.GetKey("l")){
                L_anim.SetTrigger("hitL");
            }
            UI.AddScore(1);
        }
    }
}
