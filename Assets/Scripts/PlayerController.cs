using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public float jumpForce = 700.0f;
    public float gravityModifier = 2.0f;
    private static bool gravityInitialized = false;
    public bool isOnGround = true;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        // Dùng biến tĩnh static bool để giữ gravityInitialized tồn tại qua các lần tải lại scene.
        if (!gravityInitialized)
        {
            Physics.gravity *= gravityModifier;
            gravityInitialized = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTap() && isOnGround && !gameManager.gameOver)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    public bool IsTap()
    {
        // Kiểm tra nếu có ít nhất một ngón tay chạm vào màn hình.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Khi người dùng chạm (tap) bắt đầu.
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        // (Tùy chọn) Hỗ trợ nhảy khi nhấn chuột hoặc phím Space khi test trên PC.
        if (Application.isEditor && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (!gameManager.gameOver)
            {
                dirtParticle.Play();
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Debug.Log("Game Over!");
            // Tránh hiện tượng lặp lại sự kiện Game Over nhiều lần.
            if (!gameManager.gameOver)
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound, 1.0f);
                dirtParticle.Stop();
                gameManager.GameOver();
            }
        }
    }
}
