using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandoBalloon : MonoBehaviour
{
    public int ballonRandomSize = 10;
    public float spawnRate = 4f;
    public float ballonYMax = 1.18f;
    public float ballonYMin = -2f;
    public int rangeOfSpecial = 50;
    public GameObject ballonPrefab;
    public GameObject finishLinePrefab;
    public GameObject specialBallonPrefab;
    private float lastSpawnRate = 1.5f;

    private float m_SpawnXPosition = 23f;
    private int m_CurrentBallon = 0;
    private float m_TimeSinceLastSpawn;
    private GameObject[] m_BalloonArr;
    private bool m_FinishLineBool = false;

    private Vector2 objectBallonPosition = new Vector2(-10, -30f);

    void Start()
    {
        lastSpawnRate = spawnRate;
        m_BalloonArr = new GameObject[ballonRandomSize];
        for (int i = 0; i < ballonRandomSize; i++)
        {
            m_BalloonArr[i] = (GameObject)Instantiate(ballonPrefab, objectBallonPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
     {     
        if(GameManager.Instance.timeLeft <= 0 && !m_FinishLineBool && !GameManager.Instance.gameIsOver && !GameManager.Instance.unlimitedGame)
        {
            Destroy(m_BalloonArr[m_CurrentBallon]);
            m_BalloonArr[m_CurrentBallon] = (GameObject)Instantiate(finishLinePrefab, objectBallonPosition, Quaternion.identity);
            m_BalloonArr[m_CurrentBallon].transform.position = new Vector2(11, 0);
            m_FinishLineBool = true;
        }
        if (GameManager.Instance.gameIsOver || m_FinishLineBool) return;

        m_TimeSinceLastSpawn += Time.deltaTime;
        if (GameManager.Instance.isJet)
        {
            spawnRate = 0.5f;
        }
        else
        {
            spawnRate = lastSpawnRate;
        }
        if (spawnRate <= m_TimeSinceLastSpawn)
        {
            m_TimeSinceLastSpawn = 0;
            float spawnYPosition = Random.Range(ballonYMin, ballonYMax);

            Destroy(m_BalloonArr[m_CurrentBallon]);
            if (Random.Range(0, rangeOfSpecial) == 1) // In average after n times will be a special ballon (probability: 1/range)
            {
                m_BalloonArr[m_CurrentBallon] = (GameObject)Instantiate(specialBallonPrefab, objectBallonPosition, Quaternion.identity);
                m_BalloonArr[m_CurrentBallon].transform.Rotate(m_SpawnXPosition, spawnYPosition, Random.Range(0, 180), Space.World);
            }
            else m_BalloonArr[m_CurrentBallon] = (GameObject)Instantiate(ballonPrefab, objectBallonPosition, Quaternion.identity);
            m_BalloonArr[m_CurrentBallon].transform.position = new Vector2(m_SpawnXPosition, spawnYPosition);
            m_CurrentBallon++;
            
            if (ballonRandomSize <= m_CurrentBallon)
            {
                m_CurrentBallon = 0;
            }
        }
    }
}
