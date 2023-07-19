using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText ; 
    [SerializeField] TextMeshProUGUI scoresText; 
    
    int score = 0;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;         // gameSession makes sure that palyer progress is not lost during death
        if (numGameSessions > 1)                                               // We make sure there is only one gameSession object and destroy any new that is created
        {
            Destroy(gameObject);
        }
        else                                                                   // we dont destroy the gameSession so we can keep track of our lives
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoresText.text = score.ToString();
    }


    public void playerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();            
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePresist();
        SceneManager.LoadScene(0);                                            // when we reload from start we create a new gameSession and destroy the current one
        Destroy(gameObject);
    }
    public void AddtoScore(int value)
    {
        score += value ;
        scoresText.text = score.ToString();
    }
}
