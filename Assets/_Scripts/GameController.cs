/* Filename: GameController.cs
 * Author: Vladislav Ostrikov
 * Last modified by: Vladislav Ostrikov
 * Last modifed: Nov 3, 2019
 * This script is used to manage a game, game settings and sounds
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private int lives; // Is used to store healthImage index
    private int score; // Is used to store score of the player
    [Header("Game Objects")]
    public Image[] liveImages; // Is used to store heatlImages
    public Text scoreLabel; // Is used to store Text object for score
    public int cherryCount = 10; // Amount of cherries in the game
    public GameObject highScore; // Is used to manage score appearance in the End scene 
    public Text highScoreLabel; // Is used to store Text object for score in the End scene

    /// <summary>
    /// Is used to manage lives variable. Contain getter and setter for lives variable
    /// </summary>
    public int Lives
    {
        get { return lives; } // Getter to get lives value
        set // Setter to set lives value. Also responsible for loading End scene when the player runs out of health
        {
            lives = value;
            if(lives <= -1)
            {
                SceneManager.LoadScene("Start");
            }
        }
    }
    /// <summary>
    /// Is used to manage score variable. Containt getter and setter for score variable
    /// </summary>
    public int Score
    {
        get { return score; } // Getter to get score value
        set // Setter to set score variable. Also responsible for setting highScore value and setting scoreLabel text to score value
        {
            score = value;
            if (highScore.GetComponent<HighScore>().score < this.score)
            {
                highScore.GetComponent<HighScore>().score = this.score;
            }
            scoreLabel.text = score.ToString() + "/" + cherryCount.ToString();
        }
    }

    /// <summary>
    /// Start method runs when the scene is loaded
    /// </summary>
    void Start()
    {
        // This switch method will determine what to do when a particular scene is loaded
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                highScore = GameObject.FindGameObjectWithTag("HighScore");
                break;
            case "End":
                highScore = GameObject.FindGameObjectWithTag("HighScore");
                highScoreLabel.text = highScore.GetComponent<HighScore>().score.ToString() + "/" + cherryCount.ToString();
                break;
        }
        Lives = 2; // Set the lives value to 2, however the player has 3 lives
        Score = 0; // Sets the score value to 0
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// This method will disable livesImage at index that is equal to lives value
    /// Perspective: this method will run when the player gets hurt
    /// </summary>
    public void GotHit()
    {
        this.liveImages[lives].enabled = false;
    }
    /// <summary>
    /// This method will save highScore object before it loads the Main scene.
    /// Perspective: this method will run when the user clicks Start button
    /// </summary>
    public void OnStartButtonClick()
    {
        DontDestroyOnLoad(highScore);
        SceneManager.LoadScene("Main");
    }
    /// <summary>
    /// This method will load Start scene
    /// Perspective: this method will run when the user clicks Restart button
    /// </summary>
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Start");
    }
}
