using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text textScore;
    [SerializeField] TMP_Text deadScore;
    [SerializeField] TMP_Text highscoreText;
    [SerializeField] Animator anim;
    public float score = 0;
    float highscore = 0;
    bool beatHigh;

    void Start(){
        if(PlayerPrefs.HasKey("SavedHighscore")){
            highscore = PlayerPrefs.GetFloat("SavedHighscore");
            highscoreText.text = "Highscore: " + highscore.ToString();
        }
        beatHigh = false;
    }

    
    void Update()
    {
        score = Mathf.Clamp(transform.position.x / 2, score, score+50);
        textScore.text = Mathf.Round(score).ToString();
        deadScore.text = "Score: " + Mathf.Round(score).ToString();

        if(score > highscore){
            highscore = score;
            highscoreText.text = "Highscore: " + highscore.ToString();
            PlayerPrefs.SetFloat("SavedHighscore", highscore);
            if(!beatHigh){
                anim.SetTrigger("Score");
                beatHigh = true;
            }
            
        }
    }
}
