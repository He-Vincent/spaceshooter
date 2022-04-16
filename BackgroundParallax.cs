using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Transform cam;
    public float parallaxAmount;
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveToPos = startingPosition + cam.position * parallaxAmount;
        moveToPos.z = startingPosition.z;
        transform.position = moveToPos;

    }
}
