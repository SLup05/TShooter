using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Text ScoreText = null;
    [SerializeField] private Text HighScoreText = null;

    private GameManager gameManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        ScoreText.text = string.Format("{0}", PlayerPrefs.GetInt("SCORE"));
        HighScoreText.text = string.Format("{0}", PlayerPrefs.GetInt("HIGHSCORE"));
    }

    public void ChangeScence()
    {
        SceneManager.LoadScene("Main");
    }

    
}
