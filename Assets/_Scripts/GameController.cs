using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private int lives;
    private int score;
    [Header("Game Objects")]
    public Image[] liveImages;
    public Text scoreLabel;
    public int cherryCount = 10;

    public int Lives
    {
        get { return lives; }
        set
        {

            lives = value;
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreLabel.text = score.ToString();
        }
    }

    void Start()
    {
        Lives = 2;
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GotHit()
    {
        this.liveImages[lives].enabled = false;
    }
}
