using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text displayedTextBurst;

    [SerializeField]
    private Text burstTimerText;
    private float burstTimer;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayedTextBurst.text.Length > 0 && burstTimer > 0) {
            if (isKeyTyped(displayedTextBurst.text[0])) { 
                displayedTextBurst.text = displayedTextBurst.text.Substring(1);
                AddScore(1);
            }
            burstTimer -= Time.deltaTime;
            if (burstTimer <= 0) {
                displayedTextBurst.text = string.Empty;
                burstTimerText.text = string.Empty;
            } else {
                burstTimerText.text = burstTimer.ToString();
            }
        } else if(burstTimer > 0) {
            AddScore((int) burstTimer);
            burstTimer = 0;
            burstTimerText.text = string.Empty;
        }

    }
    private bool isKeyTyped(char key) {
        if (key.Equals(' ')) {
            return Input.GetKey(KeyCode.Space);
        }
        if (char.IsUpper(key)) {
            return Input.GetKey(char.ToLower(key).ToString()) && (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift));
        }
        return Input.GetKey(key.ToString());
    }

    public void AddScore(int points)
    {
        score += points;
        updateScore();
    }

    public void updateScore()
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void textBurst(float secs, string text) {
        displayedTextBurst.text = text;
        burstTimer = secs;
        burstTimerText.text = burstTimer.ToString();
    }
}
