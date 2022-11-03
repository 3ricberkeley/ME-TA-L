using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burstNote : NoteBehavior
{
    public string text { get; set; }
    public float burstLength { get; set; }
    protected Animator burst_anim;

    public void Start() {
        burst_anim = GameObject.Find("burstBG").GetComponent<Animator>();
    }

    public override void onHit(UIManager UI) {
        UI.textBurst(burstLength, text);
        Debug.Log("bursting in");
        burst_anim.SetBool("burst", true);
        base.onHit(UI);
    }
}
