using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 option1;
    private Vector3 option2;
    private int choice = 1;
    void Start()
    {
        option1 = this.transform.GetChild(0).position;
        option2 = this.transform.GetChild(1).position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = option1;
            choice = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = option2;
            choice = 2;
        }
        if(choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
