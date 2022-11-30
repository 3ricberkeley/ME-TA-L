using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    private bool hit = false;
    internal float timeStamp { private get; set; }
    internal NoteSpawner noteSpawner { private get; set; }

    public virtual void onHit(UIManager UI) {
        int timingBucket = getTimingBucket();
        if (timingBucket >= Timings.bucketScore.Length) {
            return;
        }
        hit = true;
        UI.AddScore(Timings.bucketScore[timingBucket]);
        UI.displayHitQualityIndicator(timingBucket);
        if (timingBucket < Timings.bucketScore.Length - 1) UI.combo++;
        if (!gameObject.name.Equals("hold(Clone)"))
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void onMiss(UIManager UI) {
        if (!hit) {
            UI.reduceHealth(1);
            UI.combo = 0;

            if (gameObject.name.Equals("hold(Clone)"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .75f);
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(37, 147, 219, 125);
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .75f);
            }
        }
    }

    private int getTimingBucket() {
        float diff = Math.Abs(noteSpawner.getSongPos() - timeStamp);
        int bucketIndex = 0;
        for (;bucketIndex < Timings.timingBuckets.Length; bucketIndex++) {
            if (diff < Timings.timingBuckets[bucketIndex]) {
                return bucketIndex;
            }
        }
        return bucketIndex;
    }
}
