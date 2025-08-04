using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> listObstacles = new List<GameObject>();
    // [SerializeField] private Vector3 spawnPos = new Vector3(30, 0, 0);
    [SerializeField] private float xBase = 20.0f;
    [SerializeField] private float xRange = 10.0f;
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (!gameManager.gameOver)
        {
            int index = Random.Range(0, listObstacles.Count);
            float x = xBase + Random.Range(0, xRange);
            // spawnPos = new Vector3(x, 0, 0);
            Vector3 spawnPos = new Vector3(x, 0, 0);
            Instantiate(listObstacles[index], spawnPos, listObstacles[index].transform.rotation);
        }
    }
}
