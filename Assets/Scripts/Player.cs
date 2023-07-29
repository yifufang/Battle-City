using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 bulletEulerAngles;
    private float cooltime;
    [SerializeField]
    private float shieldtime;
    private bool isShield = true;
    private SpriteRenderer sr;
    public GameObject bulletfrefab;
    public Sprite[] TankSprite;
    public GameObject explosionPrefab;

    public AudioSource moveAudio;
    public AudioClip[] drivingClips;
    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isShield)
        {
            shieldtime -= Time.deltaTime;
            if(shieldtime <= 0)
            {
                isShield = false;
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if(cooltime >= 0.4f)
        {
            attack();
        }
        else
        {
            cooltime += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        move();
    }
    private void move()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if (v > 0)
        {
            sr.sprite = TankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        else if (v < 0)
        {
            sr.sprite = TankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }
        transform.Translate(Vector2.up * v * speed * Time.fixedDeltaTime, Space.World);

        if (Mathf.Abs(v) > 0.05f || Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = drivingClips[1];
            
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = drivingClips[0];

            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

        if (v != 0)
        {
            return;
        }

        
        // 应用运动
        transform.Translate(Vector2.right * h * speed * Time.fixedDeltaTime, Space.World);
        if (h > 0)
        {
            sr.sprite = TankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        else if (h < 0)
        {
            sr.sprite = TankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }  
    }
    
    private void attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(bulletfrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            cooltime = 0;
        }
    }

    public void Die()
    {
        //explode
        // die
        if (isShield)
        {
            return;
        }
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        PlayerManager.Instance.isDead = true;
    }
}
