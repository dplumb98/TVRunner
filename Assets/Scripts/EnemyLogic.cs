using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public bool spinObject = false;
    public bool spinRight = false;
    public bool moveObject = false;
    public bool moveRight = false;
    public bool orbit = false;
    public float rotationSpeed = 100;
    public float orbitSpeed = 100;

    public GameObject orbitTarget;

    private bool hitTrigger = false;
    public float speed = 3;

    private void Update()
    {
        MethodLibrary.TriggerMovementCheck(transform, moveObject, moveRight, hitTrigger, speed);
        MethodLibrary.SpinCheck(transform, spinObject, spinRight, rotationSpeed);
        if (orbit == true)
        {
            transform.RotateAround(orbitTarget.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitTrigger = MethodLibrary.TriggerCollisionCheck(collision, hitTrigger);
    }
}
