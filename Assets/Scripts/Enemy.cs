using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 bulletEulerAngles;
    private float attackCooltime = 0;
    public float cooldown;
    private float changDirectionTime;
    public float DirectionChangeRate;
    private SpriteRenderer sr;
    public GameObject bulletfrefab;
    public Sprite[] TankSprite; // up, right, down, left
    public GameObject explosionPrefab;

    float v = -1;
    float h = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (attackCooltime <= 0)
        {
            attack();
        }
        else
        {
            attackCooltime -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        move();
    }
    private void attack()
    {
        Instantiate(bulletfrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        attackCooltime = cooldown;
    }
    private void move()
    {
        if(changDirectionTime >= DirectionChangeRate)
        {
            int num = Random.Range(0, 8);
            if(num > 5)
            {
                v = -1;
                h = 0;
            }
            else if(num == 0)
            {
                v = 1;
                h = 0;
            }
            else if(num >0 && num <= 2)
            {
                h = -1;
                v = 0;
            }
            else if(num>2 && num <= 4)
            {
                h = 1;
                v = 0;
            }
            changDirectionTime = 0;
        }
        else
        {
            changDirectionTime += Time.fixedDeltaTime;
        }

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


        // 应用运动
        
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
        transform.Translate(Vector2.right * h * speed * Time.fixedDeltaTime, Space.World);
    }
    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "static_undestructable")
        {
            changDirectionTime = 3;
        }
    }
}
