using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    private float score;

    [SerializeField]
    private Text displayedTextBurst;

    [SerializeField]
    private Text burstTimerText;
    private float burstTimer;

    [SerializeField]
    private Text hqIndicatorText;
    [SerializeField]
    private float hqIndicatorDecay;
    private float mostRecentHitTime = 0;

    [SerializeField]
    private Text _healthText;
    private int health;

    [SerializeField]
    private Text comboText;

    public GameObject burstBG;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        burstBG = GameObject.Find("burstBG");
        animator = burstBG.GetComponent<Animator>();
        _scoreText.text = "Score: " + 0;
        health = 20;
        _healthText.text = "Helth: " + health.ToString();
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
                animator.SetBool("burst", false);
            } else {
                burstTimerText.text = burstTimer.ToString();
            }
        } else if(burstTimer > 0) {
            AddScore((int) burstTimer);
            burstTimer = 0;
            burstTimerText.text = string.Empty;
            animator.SetBool("burst", false);
        }
        handleHqIndicatorOpacity();
    }

    private void handleHqIndicatorOpacity() {
        // Debug.Log(Color.Lerp(
        //         Timings.indicatorColor,
        //         Color.clear,
        //         (Time.time - mostRecentHitTime) / hqIndicatorDecay));
        hqIndicatorText.color = 
            Color.Lerp(
                Timings.indicatorColor, 
                Color.clear, 
                (Time.time - mostRecentHitTime) / hqIndicatorDecay);
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

    public void AddScore(float points)
    {
        score += points;
        updateScore();
    }

    public void updateScore()
    {
        _scoreText.text = "Score: " + ((int)score).ToString();
    }

    public void textBurst(float secs, string text) {
        displayedTextBurst.text = text;
        burstTimer = secs;
        burstTimerText.text = burstTimer.ToString();
    }

    public void reduceHealth(int amt) {
        health -= amt;
        _healthText.text = "Helth: " + health.ToString();
        if (health <= 0) {
            Debug.Log("game over or smt idk");
            SceneSwitcher.FailSong();
        }
    }
    public void displayHitQualityIndicator(int i) {
        hqIndicatorText.text = Timings.bucketNames[i];
        mostRecentHitTime = Time.time;
    }

    private int _combo = 0;
    internal int combo {
        get => _combo;
        set {
            _combo = value;
            comboText.text = "Combo: " + value.ToString();
        }
    }
}
