using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public KeyCode key;
    private UIManager UI;

    public Animator A_anim;
    public Animator S_anim;
    public Animator K_anim;
    public Animator L_anim;

    // Start is called before the first frame update
    void Start()
    {
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
            other.gameObject.GetComponent<NoteBehavior>().onHit(UI);

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
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        other.gameObject.GetComponent<NoteBehavior>().onMiss(UI);
        Debug.Log("waaaaat");
        other.GetComponent<SpriteRenderer>().color /= 2;
    }
}
