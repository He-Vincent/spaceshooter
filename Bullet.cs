using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool hurtPlayer;
    public bool hurtEnemy;

    public float lifeDuration; // how long the bullet lives for
    float deathTime; // when we destroy the bullet


    // Start is called before the first frame update
    void Start()
    {
        // set when the bullet dies when spawning in
        deathTime = Time.time + lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        
        // destroy bullet if time runs out
        if (Time.time > deathTime)
        {
            Destroy(gameObject);
        }
    }
}
