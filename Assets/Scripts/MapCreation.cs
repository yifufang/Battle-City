using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    [Header("prefabs of maps: 0 heart, 1 wall, 2 barrier,\n 3 born, 4 river, 5 grass, 6 airbarrier")]
    public GameObject[] items;
    HashSet<Vector3> uniqueVectors = new HashSet<Vector3>();

    private void Awake()
    {
        CreateItem(items[0], new Vector3(0, -8, 0), Quaternion.identity);
        CreateItem(items[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(items[1], new Vector3(1, -8, 0), Quaternion.identity);

        for(int i=-1; i<2; i++)
        {
            CreateItem(items[1], new Vector3(i, -7, 0), Quaternion.identity);
            uniqueVectors.Add(new Vector3(i, -7, 0));
        }
        uniqueVectors.Add(new Vector3(0, -8, 0));
        uniqueVectors.Add(new Vector3(-1, -8, 0));
        uniqueVectors.Add(new Vector3(1, -8, 0));

        CreateOuterWall();

        for(int i =0; i<30; i++)
        {
            CreateItem(items[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 30; i++)
        {
            CreateItem(items[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 10; i++)
        {
            CreateItem(items[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(items[5], CreateRandomPosition(), Quaternion.identity);
        }

        //Initialize Player
        Vector3 vec = new Vector3(-2, -8, 0);
        GameObject go = Instantiate(items[3], vec, Quaternion.identity);
        go.GetComponent<BornControl>().createPlayer = true;

        //initialize enemies
        Vector3 enemiespoint1 = new Vector3(-10, 8, 0);
        Vector3 enemiespoint2 = new Vector3(0, 8, 0);
        Vector3 enemiespoint3 = new Vector3(10, 8, 0);
        CreateItem(items[3], enemiespoint1, Quaternion.identity);
        CreateItem(items[3], enemiespoint2, Quaternion.identity);
        CreateItem(items[3], enemiespoint3, Quaternion.identity);

        //create enemy for time interval
        InvokeRepeating("createEnemy", 4, 4);
    }
    private void createEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 enemypos = new Vector3();
        if(num == 0)
        {
            enemypos = new Vector3(-10, 8, 0);
        }
        if (num == 1)
        {
            enemypos = new Vector3(0, 8, 0);
        }
        if (num == 2)
        {
            enemypos = new Vector3(10, 8, 0);
        }
        CreateItem(items[3], enemypos, Quaternion.identity);

    }
    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        Instantiate(createGameObject, createPosition, createRotation, this.transform);
    }

    private Vector3 CreateRandomPosition()
    {
        Vector3 CreateRandomPosition;
        do
        {
            CreateRandomPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
        } while (hasDuplicateVectors(CreateRandomPosition));
        return CreateRandomPosition;
    }

    private bool hasDuplicateVectors(Vector3 randposition)
    {
        if (!uniqueVectors.Add(randposition))
        {
            return true;
        }
        return false;
    }

    private void CreateOuterWall()
    {
        for(int i = -11; i<12; i++)
        {
            Vector3 vec = new Vector3(i, 9, 0);
            CreateItem(items[6], vec, Quaternion.identity);
            uniqueVectors.Add(vec);
        }
        for (int i = -11; i < 12; i++)
        {
            Vector3 vec = new Vector3(i, -9, 0);
            CreateItem(items[6], vec, Quaternion.identity);
            uniqueVectors.Add(vec);
        }
        for (int i = -8; i < 9; i++)
        {
            Vector3 vec = new Vector3(-11, i, 0);
            CreateItem(items[6], vec, Quaternion.identity);
            uniqueVectors.Add(vec);
        }
        for (int i = -8; i < 9; i++)
        {
            Vector3 vec = new Vector3(11, i, 0);
            CreateItem(items[6],vec, Quaternion.identity);
            uniqueVectors.Add(vec);
        }
    }
}
