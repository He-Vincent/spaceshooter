using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public float moveSpeed = 1;
    public float unkillDuration = 20;
    float unkillOver;

    Rigidbody2D rb;

    public int hp;
    public int maxHp;
    public Transform bound;
    public GameObject deathScreen;

    public GameObject PlayerHP;
    Image healthBar;

    bool unkillable;
    //int maxHP = 40;

    SpriteRenderer sprite;

    public GameObject shotgun;
    public GameObject spread;
    public GameObject normal;

    public int killsToSpend = 0;

    Gun gunScript;




    void Start()
    {
        deathScreen.SetActive(false);

        // if time.time round to even, change to green, round to change change ship to red
        sprite = GetComponent<SpriteRenderer>();    
        maxHp = hp;
        rb = GetComponent<Rigidbody2D>();

        healthBar = PlayerHP.GetComponentInChildren<Image>();
    }

    void Update()
    {




        print("Kills To Spend: " + killsToSpend);
        healthBar.fillAmount = (float)hp / maxHp;

        if (killsToSpend >= 15 && Input.GetKeyDown(KeyCode.C))
        { 
            normal.SetActive(false);
            spread.SetActive(false);
            shotgun.SetActive(true);
            killsToSpend -= 15;
            gunScript.cooldownDuration = 0.1F;
            
        }
        if (killsToSpend >= 30 && Input.GetKeyDown(KeyCode.V))
        {
            normal.SetActive(false);
            shotgun.SetActive(false);
            spread.SetActive(true);
            killsToSpend -= 30;
            gunScript.cooldownDuration = 0.06F;
        }
        if (killsToSpend >= 1 && Input.GetKeyDown(KeyCode.B))
        {
            spread.SetActive(false);
            shotgun.SetActive(false);
            normal.SetActive(true);
            killsToSpend -= 1;
            gunScript.cooldownDuration = 0.16F;
        }


        if (hp > 20 && Input.GetMouseButtonDown(1))
        {
            hp -= 20;
            unkillable = true;
            unkillOver = Time.time + unkillDuration;

        }
        if (unkillable == true)
        {
            if (Mathf.RoundToInt(Time.time) % 2 == 0)
            {
                //do random change to orange or green or purple

                //change ship to red
                gameObject.GetComponent<Renderer>().material.color = new Color(143, 0, 255);
            }
            else if (Mathf.RoundToInt(Time.time) % 2 == 1)
            {

                //do random change to blue or pink or red

                gameObject.GetComponent<Renderer>().material.color = new Color(255, 215, 0);
            }
        }
       
      
        if (Time.time > unkillOver)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            unkillable = false;
        }

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(
            clampedPos.x,
            bound.position.x - bound.localScale.x / 2,
            bound.position.x + bound.localScale.x / 2);
        clampedPos.y = Mathf.Clamp(
            clampedPos.y,
            bound.position.y - bound.localScale.y / 2,
            bound.position.y + bound.localScale.y / 2);
        transform.position = clampedPos;

        PlayerHP.transform.position = Camera.main.WorldToScreenPoint
            (transform.position);

    }

    void FixedUpdate()
    {


        // PLAYER ROTATION
        Vector3 mousePos =
      Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;

        
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // PLAYER MOVEMENT
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 velocityChange = new Vector2(inputX, inputY);
        velocityChange.x *= 0.4f;
        velocityChange.y = Mathf.Clamp(velocityChange.y, -0.4f, 1);
        velocityChange = transform.TransformVector(velocityChange);

        velocityChange *= Time.fixedDeltaTime * moveSpeed;

        rb.velocity += velocityChange;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
            deathScreen.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
        
        if (enemyScript != null && unkillable == false)
        {
            hp -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bulletScript = collision.GetComponent<Bullet>();

        // run script if DOES get bullet script component from object
        if (bulletScript != null && bulletScript.hurtPlayer == true)
        {
         
            if (unkillable == false)
            {
                hp -= 4;
            }
            
            Destroy(bulletScript.gameObject);
            

        }

    }
}
