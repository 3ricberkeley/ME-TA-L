using System;
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

    internal Vector2 hitboxSize { get; set; }

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
        if (Input.GetKeyDown(key)) {

            castForHits();

            if (Input.GetKey("a")) {
                A_anim.SetTrigger("hitA");
            }
            if (Input.GetKey("s")) {
                S_anim.SetTrigger("hitS");
            }
            if (Input.GetKey("k")) {
                K_anim.SetTrigger("hitK");
            }
            if (Input.GetKey("l")) {
                L_anim.SetTrigger("hitL");
            }
        }
    }
    private void castForHits() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, hitboxSize, 0f, Vector2.zero);
        Transform bestHit = null;
        float bestDistance = float.PositiveInfinity;
        foreach (RaycastHit2D hit in hits) {
            float distance = Vector2.Distance(hit.transform.position, transform.position);
            if (!hit.transform.CompareTag("HitBox") && distance < bestDistance) {
                bestDistance = distance;
                bestHit = hit.transform;
            }
        }
        Debug.Log(bestHit);
        if (bestHit != null) bestHit.GetComponent<NoteBehavior>().onHit(UI);
    }

    private void OnTriggerExit2D(Collider2D other) {
        other.gameObject.GetComponent<NoteBehavior>().onMiss(UI);
        other.GetComponent<SpriteRenderer>().color /= 2;
    }
}
