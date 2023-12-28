using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private const string SAVE_FILE_SCORE = "saveFileScore";

    private float timerPaused = 0f;
    private float timerPlay = 1f;
    private float transformPosiyionToStartPlayer;

    [SerializeField] GameObject touchpadUI;
    [SerializeField] GameOver gameOver;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] TextMeshProUGUI scoreTextGameOverUI;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button buttonRestart;
    [SerializeField] Button buttonStart;

    private int score;
    private int scoreRes;

    private void Awake()
    {
        ShowMainMenuUI();
        HideGameOverUI();
        HideTouchpadUI();
    }
    private void Start()
    {
        gameOver.OnGameOver += GameOver_OnGameOver;
        transformPosiyionToStartPlayer = Player.Instance.gameObject.transform.position.z;
        ButtonStart();
        score = PlayerPrefs.GetInt(SAVE_FILE_SCORE);
    }
    private void Update()
    {
        Score("SCORE: ",false);
    }
    private void GameOver_OnGameOver(object sender, System.EventArgs e)
    {
        ShowGameOverUI();
        HideTouchpadUI();
        Score("RECORD: ", true);

        buttonRestart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }
    private void ButtonStart()
    {
        buttonStart.onClick.AddListener(() =>
        {
            HideMainMenuUI();
            ShowTouchpadUI();
        });
    }
    private void Score(string name, bool saveFile)
    {
        scoreRes = (int)Player.Instance.transform.position.z - (int)transformPosiyionToStartPlayer;
        if (scoreRes >= score)
        {
            scoreTextGameOverUI.text = name + scoreRes.ToString();
            scoreText.text = name + scoreRes.ToString();
            if (saveFile == true)
            {
                PlayerPrefs.SetInt(SAVE_FILE_SCORE, scoreRes);
                PlayerPrefs.Save();
            };
        }
        else
        {
            scoreTextGameOverUI.text = name + score.ToString();
            scoreText.text = name + score.ToString();
        }

    }

    private void ShowMainMenuUI()
    {
        mainMenuUI.SetActive(true);
        PausedGame();
    }
    private void HideMainMenuUI()
    {
        mainMenuUI.SetActive(false);
        PlayGame();
    }
    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        Invoke("PausedGame", 2f);

    }
    private void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
        PlayGame();
    }
    private void ShowTouchpadUI()
    {
        touchpadUI.SetActive(true);
    }
    private void HideTouchpadUI()
    {
        touchpadUI.SetActive(false);
    }
    private void PausedGame()
    {
        Time.timeScale = timerPaused;
    }
    private void PlayGame()
    {
        Time.timeScale = timerPlay;
    }

}
