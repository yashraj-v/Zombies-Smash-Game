using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    public bool isRising = false;
    public bool isFalling = false;


    private int activeZombieIndex = 0;
    private Vector2 startPosition;

    public int riseSpeed = 1;
    public int scoreThreshold = 5;    

    private int zombiesSmashed;
    private int livesRemaining;
    private bool gameOver;

    public Image life1, life2, life3;
    public Text scoreText;
    public Button GameOverButton;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        zombiesSmashed = 0;
        scoreText.text = zombiesSmashed.ToString();     //Converting the score from int to string to display
        livesRemaining = 3;
        pickNewZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            if(isRising)
            { 
                if(zombies[activeZombieIndex].transform.position.y - startPosition.y >= 3f)    //Zombie highest point
                {
                    //Start to bring it down

                    isRising = false;
                    isFalling = true;
                }
                else
                {
                    //zombie going up
                    zombies[activeZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
                }
            }
            else if(isFalling)
            {
                //zombie goes down
                if(zombies[activeZombieIndex].transform.position.y - startPosition.y <= 0f)
                {
                    //stop fall
                    isFalling = false;
                    isRising = false;
                    livesRemaining--;
                    UpdateLifeUI();
                   
                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
                }
            }
            else
            {
                //other stuff
                zombies[activeZombieIndex].transform.position = startPosition;
                pickNewZombie();
            }
        }    
    }

    private void UpdateLifeUI()
    {
        // Lives/hearts remaining
        if(livesRemaining == 3)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(true);
        }
            
        if(livesRemaining == 2)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(true);
            life3.gameObject.SetActive(false);                
        }

        if(livesRemaining == 1)
        {
            life1.gameObject.SetActive(true);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(false);                
        }

        if(livesRemaining == 0)
        {
            gameOver = true;
            life1.gameObject.SetActive(false);
            life2.gameObject.SetActive(false);
            life3.gameObject.SetActive(false);   
            GameOverButton.gameObject.SetActive(true);         
        }
    }
    
    private void pickNewZombie() //picks random zombie
    {
        isRising = true;
        isFalling = false;
        activeZombieIndex = UnityEngine.Random.Range(0, zombies.Length);  //Unity Random function
        startPosition = zombies[activeZombieIndex].transform.position;
    }

    public void killEnemy()
    {
        zombiesSmashed++;
        IncreaseSpawnSpeed();
        scoreText.text = zombiesSmashed.ToString();
        zombies[activeZombieIndex].transform.position = startPosition;  //if Zombie is smashed it comes back to start position
        pickNewZombie();

    }

    private void IncreaseSpawnSpeed()
    {
        if(zombiesSmashed>=scoreThreshold)
        {
            riseSpeed++;
            scoreThreshold *= 2;
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //Starts the scene over again
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
