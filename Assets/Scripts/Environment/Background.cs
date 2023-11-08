using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform player;
    public float offset;

    private void Start()
    {
        cameraTransform = GameObject.Find("Virtual Camera").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float resetPos = Mathf.Abs(player.transform.position.x % 63f);
        if (resetPos < 0.5f)
        {
            transform.position = new Vector3(player.transform.position.x, -0.2f, 0.5f);
        }
    }
}
