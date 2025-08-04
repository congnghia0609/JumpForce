using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton; // Gán trong Inspector
    [SerializeField] private GameObject replayButton; // Gán trong Inspector
    [SerializeField] private TMP_Text guideText; // Gán trong Inspector
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private TMP_Text scoreText;
    public bool gameOver = true;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        OutlineText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OutlineText()
    {
        if (bestScoreText != null)
        {
            // Tạo một bản sao material để không ảnh hưởng đến các TMP khác
            Material newMatWhite = new Material(bestScoreText.fontSharedMaterial);
            newMatWhite.EnableKeyword("OUTLINE_ON");
            newMatWhite.SetColor("_OutlineColor", Color.white);
            // newMatWhite.SetColor("_OutlineColor", Color.white.WithAlpha(0.9f));
            newMatWhite.SetFloat("_OutlineWidth", 0.1f);

            // Áp material mới có outline
            bestScoreText.fontSharedMaterial = newMatWhite;
            if (scoreText != null)
            {
                scoreText.fontSharedMaterial = newMatWhite;
            }
        }
        if (guideText != null)
        {
            // Tạo một bản sao material để không ảnh hưởng đến các TMP khác
            Material newMatBlack = new Material(guideText.fontSharedMaterial);
            newMatBlack.EnableKeyword("OUTLINE_ON");
            // newMatBlack.SetColor("_OutlineColor", Color.black);
            newMatBlack.SetColor("_OutlineColor", Color.black.WithAlpha(0.7f));
            newMatBlack.SetFloat("_OutlineWidth", 0.2f);

            // Áp material mới có outline
            guideText.fontSharedMaterial = newMatBlack;
        }
    }

    public void AddScore(int i)
    {
        score += i;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateBestScore()
    {
        bestScoreText.text = "Best Score: " + DataManager.Instance.gameData.BestScore;
    }

    // Gọi khi để bắt đầu game.
    public void StartGame()
    {
        // Ẩn nút chơi lại.
        replayButton.SetActive(false);
        playButton.SetActive(false);
        // guideText.SetActive(false);
        guideText.enabled = false;
        gameOver = false;
    }

    // Gọi khi game kết thúc.
    public void GameOver()
    {
        gameOver = true;
        // Update best score.
        if (score > DataManager.Instance.gameData.BestScore)
        {
            DataManager.Instance.gameData.BestScore = score;
            UpdateBestScore();
            DataManager.Instance.Save();
        }
        // Hiện nút chơi lại
        replayButton.SetActive(true);
        playButton.SetActive(false);
        // guideText.SetActive(false);
        guideText.enabled = false;
    }

    // Gọi khi nhấn nút Replay
    public void ReplayGame()
    {
        // Tải lại scene hiện tại ngoại trừ lần tải game đầu tiên.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
