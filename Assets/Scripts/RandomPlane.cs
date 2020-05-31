using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlane : MonoBehaviour
{
    public int planeRandomSize = 6;
    public int spawnRate = 16;
    public float planeYMax = 1.18f;
    public float planeYMin = -2f;
    public GameObject planePrefab;

    public float m_SpawnXPosition = 10f;
    private int m_Currentplane = 0;
    private float m_TimeSinceLastSpawn;
    private GameObject[] m_PlaneArr;
    private Vector2 m_ObjectplanePosition = new Vector2(-10, -30f);

    void Start()
    {
        GameManager.Instance.SetChangeSpawnRatePlane(spawnRate);
        m_PlaneArr = new GameObject[planeRandomSize];
        for (int i = 0; i < planeRandomSize; i++)
        {
            m_PlaneArr[i] = (GameObject)Instantiate(planePrefab, m_ObjectplanePosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.timeLeft <= 0) return;
        spawnRate = GameManager.Instance.ChangeSpawnRatePlane;
        m_TimeSinceLastSpawn += Time.deltaTime;
        if (GameManager.Instance.gameIsOver) return;
        if (spawnRate <= m_TimeSinceLastSpawn)
        {
            m_TimeSinceLastSpawn = 0;
            float spawnYPosition = Random.Range(planeYMin, planeYMax);

            Destroy(m_PlaneArr[m_Currentplane]);
            m_PlaneArr[m_Currentplane] = (GameObject)Instantiate(planePrefab, m_ObjectplanePosition, Quaternion.identity);

            m_PlaneArr[m_Currentplane].transform.position = new Vector2(m_SpawnXPosition, spawnYPosition);
            m_Currentplane++;

            if (planeRandomSize <= m_Currentplane)
            {
                m_Currentplane = 0;
            }
        }
    }
}
