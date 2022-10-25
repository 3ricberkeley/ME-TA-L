using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burstNote : MonoBehaviour
{
    public string text;
    public int burstLength;
    private UIManager UI;
    void Start() {
        UI = GameObject.FindWithTag("Score").GetComponent<UIManager>();
    }

    public void destroy() {
        UI.textBurst(burstLength, text);
    }
}
