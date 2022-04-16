using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform[] spawners;

 

    public float cooldownDuration;
    float cooldownFinishTime;

    // Start is called before the first frame update
    void Start()
    {
        //print(spawners.Length);
    }

    // Update is called once per frame
    void Update()
    {
        // when player left clicks
        if (Input.GetMouseButton(0) && Time.time > cooldownFinishTime)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                //print("spawning bullet " + i);
                Instantiate(bulletPrefab, spawners[i].position, spawners[i].rotation);
            }
            cooldownFinishTime = Time.time + cooldownDuration;
        }

        //if (Input.GetMouseButtonDown(0))
        //{

        //    Instantiate(bulletPrefab, spawners[0].position, spawners[0].rotation);
        //    Instantiate(bulletPrefab, spawners[1].position, spawners[1].rotation);
        //    Instantiate(bulletPrefab, spawners[2].position, spawners[2].rotation);
        //}

       
    }
}

