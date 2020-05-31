using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuilding : MonoBehaviour
{
    public int buildingRandomSize = 6;
    public int spawnRate = 10;
    public float buildingY = -2.394042f;
    public GameObject buildingPrefab;

    public float m_SpawnXPosition = 20f;
    private int m_CurrentBuilding = 0;
    private float m_TimeSinceLastSpawn;
    private GameObject[] m_BuildingArr;
    private Vector2 m_ObjectplanePosition = new Vector2(-10, -30f);

    void Start()
    {
        GameManager.Instance.SetChangeSpawnRateBulding(spawnRate);
        m_BuildingArr = new GameObject[buildingRandomSize];
        for (int i = 0; i < buildingRandomSize; i++)
        {
            m_BuildingArr[i] = (GameObject)Instantiate(buildingPrefab, m_ObjectplanePosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.timeLeft <= 0) return;
        spawnRate = GameManager.Instance.ChangeSpawnRateBulding;
        m_TimeSinceLastSpawn += Time.deltaTime;
        if (GameManager.Instance.gameIsOver) return;
        if (spawnRate <= m_TimeSinceLastSpawn)
        {
            m_TimeSinceLastSpawn = 0;

            Destroy(m_BuildingArr[m_CurrentBuilding]);
            m_BuildingArr[m_CurrentBuilding] = (GameObject)Instantiate(buildingPrefab, m_ObjectplanePosition, Quaternion.identity);

            m_BuildingArr[m_CurrentBuilding].transform.position = new Vector2(m_SpawnXPosition, buildingY);
            m_CurrentBuilding++;

            if (buildingRandomSize <= m_CurrentBuilding)
            {
                m_CurrentBuilding = 0;
            }
        }
    }
}