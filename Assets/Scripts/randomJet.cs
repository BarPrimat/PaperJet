using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomJet : MonoBehaviour
{
    public float spawnRate = 40f;
    public float planeYMax = 1.18f;
    public float planeYMin = -2f;
    public GameObject jetPlanePrefab;
    public float minTimeSpawnRate = 60f;
    public float maxTimeSpawnRate = 20f;

    private float m_SpawnXPosition = 10f;
    private float m_TimeSinceLastSpawn;
    private GameObject m_JetPlane;
    private Vector2 m_ObjectplanePosition = new Vector2(-10, -30f);

    void Start()
    {
        m_JetPlane = (GameObject)Instantiate(jetPlanePrefab, m_ObjectplanePosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeSinceLastSpawn += Time.deltaTime;
        if(Time.deltaTime > 60 )
        {
            spawnRate = Random.Range(minTimeSpawnRate, maxTimeSpawnRate);
        }
        if (GameManager.Instance.gameIsOver) return;
        if (spawnRate <= m_TimeSinceLastSpawn)
        {
            m_TimeSinceLastSpawn = 0;
            float spawnYPosition = Random.Range(planeYMin, planeYMax);

            Destroy(m_JetPlane);
            m_JetPlane = (GameObject)Instantiate(jetPlanePrefab, m_ObjectplanePosition, Quaternion.identity);

            m_JetPlane.transform.position = new Vector2(m_SpawnXPosition, spawnYPosition);
        }
    }
}
