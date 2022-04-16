using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    

    public int type; //enemy type


    // TYPE 0 - RAMS INTO PLAYER
    [Header("TYPE 0 VARS")]
    public float speed;

    // TYPE 1 - STATIONARY 1 GUN SHOOTER
    [Header("TYPE 1 VARS")]
    public GameObject bulletPrefab;
    public Transform[] spawners;
    public float cooldownDuration;
    float cooldownFinishTime;

    // TYPE 3 - FOLLOWS THE PLAYER UNTIL IT GETS TOO CLOSE, 1 GUN SHOOTER
    [Header("TYPE 3 VARS")]
    public float minDistance; // minimum distance from the player


    //general
    [Header("GENERAL VARS")]
    public int hp;
    int maxHp;
    public GameObject hpUIPrefab;
    Transform healthUI;
    Image healthBar;

    // PRIVATE VARIABLES
    Player playerScript;
    Transform playerTransform;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        playerTransform = playerScript.transform;
        rb = GetComponent<Rigidbody2D>();

        float cooldownFinishTime = Time.time;

        maxHp = hp;
        GameObject uiObj = Instantiate(hpUIPrefab,
            FindObjectOfType<Canvas>().transform);
        healthUI = uiObj.transform;
        healthBar = uiObj.GetComponentInChildren<Image>();
    }

    private void Update()
    {
        healthUI.position = Camera.main.WorldToScreenPoint
            (transform.position);

        healthBar.fillAmount = (float) hp / maxHp;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotate enemy towards player
        Vector3 targetDirection = playerTransform.position - 
        transform.position; 

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) 
            * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        //TYPE 0 ENEMY - RAMS INTO PLAYER
        if (type == 0)
        {
            MoveToPlayer();
        }

        // TYPE 1 ENEMY - SHOOT BULLETS AT THE PLAYER

        else if (type == 1)
        {
            FireAtPlayer();
        }
        // TYPE 2 ENEMY - SHOOT AND FOLLOW PLAYER

        else if (type == 2)
        {
            MoveToPlayer();
            FireAtPlayer();
        }
        // TYPE 3 - FOLLOWS THE PLAYER UNTIL IT GETS TOO CLOSE, 1 GUN SHOOTER
        else if (type == 3)
        {
            if (Vector3.Distance(playerTransform.position, 
                transform.position) > minDistance)
            {
                MoveToPlayer();
            }
            FireAtPlayer();
        }
        // TYPE 4 - 4 GUNS SHOOTER BUT STATIONARY
        else if (type == 4)
        {
            FireAtPlayer();
        }
        // TYPE 5 - SHOTGUN SHOOTER FOLLOWING PLAYER ENEMY
        else if (type == 5)
        {
            MoveToPlayer();
            FireAtPlayer();
        }
        // TYPE 6 - TYPE 1 ENEMY BUT HIGHER FIRE RATE AND LESS HP
        else if (type == 6)
        {
            FireAtPlayer();
        }

        //NEVER DO THIS
        //transform.position = newPosition;
    }

    void MoveToPlayer()
    {
        // move gameobject towards player transform
        Vector3 newPosition = Vector3.MoveTowards(
            transform.position, playerTransform.position, speed *
            Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
    }

    void FireAtPlayer()
    {
        if (Time.time > cooldownFinishTime)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                //print("spawning bullet " + i);
                Instantiate(bulletPrefab, spawners[i].position, spawners[i].rotation);
            }
            cooldownFinishTime = Time.time + cooldownDuration;
        }
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bulletScript = collision.GetComponent<Bullet>();

        // run script if DOES get bullet script component from object
        if (bulletScript != null && bulletScript.hurtEnemy == true)
        {
            hp -= 4;
            Destroy(bulletScript.gameObject);
            //print("Remaining hp: " + hp);

            if (hp <= 0)
            {
                FindObjectOfType<EnemySpawner>().EnemyDeath();
                Destroy(healthUI.gameObject);
                Destroy(gameObject);
   
                playerScript.hp += 2;
                if (playerScript.hp >= playerScript.maxHp)
                {
                    playerScript.hp = playerScript.maxHp;
                }
                //print("Player's remaining hp: " + playerScript.hp);
                playerScript.killsToSpend += 1;
            }

        }

    }
}
