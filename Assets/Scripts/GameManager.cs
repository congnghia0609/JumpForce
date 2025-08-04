using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton; // Gán trong Inspector
    [SerializeField] private GameObject replayButton; // Gán trong Inspector
    [SerializeField] private GameObject guideText; // Gán trong Inspector
    public bool gameOver = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Gọi khi để bắt đầu game.
    public void StartGame()
    {
        // Ẩn nút chơi lại.
        replayButton.SetActive(false);
        playButton.SetActive(false);
        guideText.SetActive(false);
        gameOver = false;
    }

    // Gọi khi game kết thúc.
    public void GameOver()
    {
        gameOver = true;
        // Hiện nút chơi lại
        replayButton.SetActive(true);
        playButton.SetActive(false);
        guideText.SetActive(false);
    }

    // Gọi khi nhấn nút Replay
    public void ReplayGame()
    {
        // Tải lại scene hiện tại ngoại trừ lần tải game đầu tiên.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
