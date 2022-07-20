using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreUI : MonoBehaviour
{
    public AnimationClip[] scoreClips;

    public Animation scoreAnimation;

    public TMP_Text scoreText;

    



    private void Start()
    {
        Destroy(this.gameObject, 1f);
    }
    
    public void SetScoreUIAnimation(bool isHarmful , int score)
    {
        if (isHarmful)
        {
            scoreAnimation.Play("IncreasedScoreAnim");
            scoreText.text = score.ToString();
        }
        else
        {
            scoreAnimation.Play("DecreasedScoreAnim");
            scoreText.text= score.ToString();
        }
            
        

        

    }
    



}
