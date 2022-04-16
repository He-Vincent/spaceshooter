using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float gap;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        int playerArea = Mathf.RoundToInt(player.position.y / gap);

        Vector3 newPos = transform.position;
        newPos.y = playerArea * gap;
        transform.position = newPos;
    }
}
