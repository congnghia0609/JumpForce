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
    public bool gameOver = false;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("ReplayButton").GetComponent<GameManager>();
        gameManager.gameObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (!gameOver)
            {
                dirtParticle.Play();
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Debug.Log("Game Over!");
            // Tránh hiện tượng lặp lại sự kiện Game Over nhiều lần.
            if (!gameOver)
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound, 1.0f);
                dirtParticle.Stop();
                gameManager.GameOver();
            }
            gameOver = true;
        }
    }
}
