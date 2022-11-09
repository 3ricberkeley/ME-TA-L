using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public virtual void onHit(UIManager UI) {
        Destroy(this.gameObject);
    }
}
