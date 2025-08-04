using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject replayButton; // Gán trong Inspector

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // Gọi khi game kết thúc
    public void GameOver()
    {
        // Hiện nút chơi lại
        replayButton.SetActive(true);
    }

    // Gọi khi nhấn nút Replay
    public void ReplayGame()
    {
        // Tải lại scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
