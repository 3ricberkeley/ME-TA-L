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
        if (timingBucket > Timings.timingBuckets.Length) {
            return;
        }
        hit = true;
        UI.AddScore(Timings.bucketScore[timingBucket]);
        Destroy(this.gameObject);
    }
    public virtual void onMiss(UIManager UI) {
        if (!hit) UI.reduceHealth(1);
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
