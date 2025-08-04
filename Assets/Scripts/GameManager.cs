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
    public float baseSpeed = 15.0f;
    public int levelNum = 10;
    public int levelSpeed = 2;
    public float speed = 15.0f;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int i)
    {
        score += i;
        UpdateScore();
        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        // Khi vượt qua 10 rào thì tăng speed lên 1 đơn vị.
        speed = baseSpeed + score / levelNum * levelSpeed;
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
