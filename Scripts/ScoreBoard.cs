using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    
    int score;
    Text scoreText;

	// Use this for initialization. Need to make a UI and Start menu still
	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
	}
    public void ScoreHit(int scoreIncrease)
    {
        score = score + scoreIncrease;
        scoreText.text = score.ToString();
    }

}
