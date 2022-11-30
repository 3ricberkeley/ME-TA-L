using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    public KeyCode key;
    private UIManager UI;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string animatorTrigger;

    internal Vector2 hitboxSize { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindWithTag("Score").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key)) {

            castForHits();
            animator.SetTrigger(animatorTrigger);

            //if (Input.GetKey("a")) {
            //    A_anim.SetTrigger("hitA");
            //}
            //if (Input.GetKey("s")) {
            //    S_anim.SetTrigger("hitS");
            //}
            //if (Input.GetKey("k")) {
            //    K_anim.SetTrigger("hitK");
            //}
            //if (Input.GetKey("l")) {
            //    L_anim.SetTrigger("hitL");
            //}
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
        // Debug.Log(bestHit);
        if (bestHit != null) bestHit.GetComponent<NoteBehavior>().onHit(UI);
    }

    private void OnTriggerExit2D(Collider2D other) {
        other.gameObject.GetComponent<NoteBehavior>().onMiss(UI);
        other.GetComponent<SpriteRenderer>().color /= 2;
    }
}
