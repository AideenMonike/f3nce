using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuButtons : MonoBehaviour
{
    public GameObject pg1, pg2, pg3;
    public GameObject cubePrefab, ballPrefab;
    public GameObject hall1, hall2, hall3;
    [SerializeField] private int mapIncrementer = 0;

    public void ToPageOne()
    {
        pg2.SetActive(false);
        pg3.SetActive(false);
        pg1.SetActive(true);
    }
    public void ToPageTwo()
    {
        pg1.SetActive(false);
        pg3.SetActive(false);
        pg2.SetActive(true);
    }
    public void ToPageThree()
    {
        pg1.SetActive(false);
        pg2.SetActive(false);
        pg3.SetActive(true);
    }

    public void SpawnCube()
    {
        Instantiate(cubePrefab, new Vector3(2.5f, 2, 2), Quaternion.Euler(0, 0, 0));
    }
    public void SpawnBall()
    {
        Instantiate(ballPrefab, new Vector3(2.5f, 2, 2), Quaternion.Euler(0, 0, 0));
    }
    public void DeleteObjects()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Sandbox Objects");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    public void SwapMaps()
    {
        mapIncrementer++;
        if (mapIncrementer > 2)
        {
            mapIncrementer = 0;
        }
        hall1.SetActive(mapIncrementer == 0);
        hall2.SetActive(mapIncrementer == 1);
        hall3.SetActive(mapIncrementer == 2);
    }
    public void Update()
    {
        hall1.SetActive(mapIncrementer == 0);
        hall2.SetActive(mapIncrementer == 1);
        hall3.SetActive(mapIncrementer == 2);
    }
}
