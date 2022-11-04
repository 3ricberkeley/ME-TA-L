using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    private bool hit = false;
    public virtual void onHit(UIManager UI) {
        hit = true;
        Destroy(this.gameObject);
    }
    public virtual void onMiss(UIManager UI) {
        if (!hit) UI.reduceHealth(1);
    }
}
