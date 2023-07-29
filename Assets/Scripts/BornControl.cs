using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornControl : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefab;

    public bool createPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("bornTank", 0.8f);
        Destroy(gameObject, 0.8f);
    }
    private void bornTank()
    {
        if (createPlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[num], transform.position, Quaternion.identity);
        }
        
    }
}
