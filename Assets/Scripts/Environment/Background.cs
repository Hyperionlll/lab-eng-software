using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Transform cameraTransform;
    private GameObject head, tail, center, aux;

    public GameObject[] backgroundObjects;

    private void Start()
    {
        cameraTransform = GameObject.Find("Virtual Camera").GetComponent<Transform>();
        head = backgroundObjects[0];
        center = backgroundObjects[1];
        tail = backgroundObjects[2];
    }

    private void Update()
    {
        float resetPosForward = (head.transform.position.x + center.transform.position.x) / 2; // the center between head and center images
        float resetPosBackward = (tail.transform.position.x + center.transform.position.x) / 2; // the center between tail and center images
        if (cameraTransform != null && cameraTransform.transform.position.x >= resetPosForward) // if the position of the camera reaches the center between head and center images
        {
            tail.transform.position = new Vector3(head.transform.position.x+31.5f, -0.5f, 99f); // reposition tail image in front of current head image
            tail.transform.Rotate(0, 180, 0); // flip the repositioned image
            aux = head; // old head stored in aux variable
            head = tail; // tail becomes new head
            tail = center; // center becomes new tail
            center = aux; // old head becomes new center
        }
        else if (cameraTransform != null && cameraTransform.transform.position.x <= resetPosBackward) // if the position of the camera reaches the center between tail and center images
        {
            head.transform.position = new Vector3(tail.transform.position.x-31.5f, -0.5f, 99f); // reposition head image behind current tail image
            head.transform.Rotate(0, 180, 0); // flip the repositioned image
            aux = tail; // old tail stored in aux variable
            tail = head; // head becomes new tail
            head = center; // center becomes new head
            center = aux; // old tail becomes new center
        }
    }
}
