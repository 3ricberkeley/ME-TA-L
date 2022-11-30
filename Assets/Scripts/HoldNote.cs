using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : NoteBehavior
{
    public float holdLength { get; set; }

    public override void onHit(UIManager UI)
    {
        base.onHit(UI);
    }

    public void AdjustHold()
    {
        GameObject fill = gameObject.transform.GetChild(0).gameObject;
        GameObject end = gameObject.transform.GetChild(1).gameObject;
        fill.transform.position = new Vector3(
            fill.transform.position.x,
            fill.transform.position.y + (holdLength * 0.11f),
            fill.transform.position.z);
        fill.transform.localScale = new Vector3(
            3.25f, 1f + (holdLength * 0.25f), 10f);
        end.transform.position = new Vector3(
            end.transform.position.x,
            end.transform.position.y + (holdLength * 0.215f),
            end.transform.position.z);
    }
}
