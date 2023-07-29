using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int heath;
    public int playerScore;
    public bool isDead;
    public GameObject born;
    public bool isDefeat;
    public TMP_Text playerScore_UI;
    public TMP_Text playerHealth_UI;
    public GameObject gameOver;
    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        if(playerScore == 15)
        {
            Invoke("loadGame", 2);
        }
        if (isDefeat)
        {
            gameOver.SetActive(true);
            Invoke("returnTotitle", 2);
        }
        if (isDead)
        {
            Recover();
        }
        playerScore_UI.text = playerScore.ToString();
        playerHealth_UI.text = heath.ToString();
    }

    private void Recover()
    {
        if(heath <= 0)
        {
            isDefeat = true;
            Invoke("returnTotitle", 2);
        }
        else
        {
            heath--;
            GameObject go = Instantiate(born, new Vector3(02, -8, 0), Quaternion.identity);
            go.GetComponent<BornControl>().createPlayer = true;
            isDead = false;
        }
    }

    private void returnTotitle()
    {
        SceneManager.LoadScene(0);
    }
    private void loadGame()
    {
        SceneManager.LoadScene(1);
    }
}
