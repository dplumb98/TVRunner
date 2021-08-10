using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    public bool hitTrigger = false;
    public float speed = 3;

    public bool spinObject = false;
    public bool spinRight = false;
    public bool moveObject = true;
    public bool moveRight = false;
    public float rotationSpeed = 100;

    // Update is called once per frame
    void Update()
    {
        MethodLibrary.TriggerMovementCheck(transform, moveObject, moveRight, hitTrigger, speed);
        MethodLibrary.SpinCheck(transform, spinObject, spinRight, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitTrigger = MethodLibrary.TriggerCollisionCheck(collision, hitTrigger);
    }
}
