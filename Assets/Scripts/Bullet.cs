using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public bool isplayer;

    public AudioClip hitAudio;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isplayer)
        {
            AudioSource.PlayClipAtPoint(hitAudio, this.transform.position);
        }
        switch (collision.tag)
        {
            case "Tank":
                if (!isplayer)
                {
                    collision.GetComponent<Player>().Die();
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.GetComponent<Heart>().baseDestroyed();
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isplayer)
                {
                    collision.GetComponent<Enemy>().Die();
                    Destroy(gameObject);
                    PlayerManager.Instance.playerScore++;
                }
                break;
            case "static":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "airwall":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
