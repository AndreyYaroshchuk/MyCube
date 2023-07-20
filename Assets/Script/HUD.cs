using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
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
    }
    private void Update()
    {
        Score(); 
    }
    private void GameOver_OnGameOver(object sender, System.EventArgs e)
    {
        ShowGameOverUI();
        HideTouchpadUI();
        Score();
        buttonRestart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
    private void ButtonStart()
    {
        buttonStart.onClick.AddListener(() =>
        {
            Debug.Log("Нажал кнопку");
            HideMainMenuUI();
            ShowTouchpadUI();
        });
    }
    private void Score()
    {
        int score = (int)Player.Instance.transform.position.z - (int)transformPosiyionToStartPlayer;
        scoreTextGameOverUI.text = $"SCORE: {score.ToString()}";
        scoreText.text = $"SCORE: {score.ToString()}";
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
        Invoke("PausedGame",2f);
    
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
