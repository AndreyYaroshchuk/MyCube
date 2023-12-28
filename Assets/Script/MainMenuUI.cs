
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const string SAVE_FILE_SCORE = "saveFileScore";

    [SerializeField] private TextMeshProUGUI textRecord;
    [SerializeField] private Button buttonNewGame;
    [SerializeField] private Button buttonPlay;

    private void Start()
    {
        textRecord.text = "RECORD: " + PlayerPrefs.GetInt(SAVE_FILE_SCORE).ToString();

        buttonNewGame.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt(SAVE_FILE_SCORE, 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        });
        buttonPlay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }
}
