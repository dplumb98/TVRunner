using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodLibrary : MonoBehaviour
{
    public static void RotateObject(Transform enemy, float rotationSpeed)
    {
        enemy.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Make our GameObject rotate
    }

    public static void SpinCheck(Transform enemy, bool spinObject, bool spinRight, float rotationSpeed)
    {
        if (spinObject == true && spinRight == true)
        {
            enemy.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime); // Make our GameObject rotate
        }
        else if (spinObject == true && spinRight == false)
        {
            enemy.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Make our GameObject rotate
        }
    }

    public static bool TriggerCollisionCheck(Collider2D collision, bool hitTrigger)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {
            if (hitTrigger == false)
            {
                return hitTrigger = true;
            }
            else
            {
                return hitTrigger = false;
            }
        }
        else
        {
            return false;
        }
    }

    public static void TriggerMovementCheck(Transform transf, bool moveObject, bool moveRight, bool hitTrigger, float speed)
    {
        if (moveObject == true && moveRight == false)
        {
            if (hitTrigger == true)
            {
                transf.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                transf.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }
        else if (moveObject == true && moveRight == true)
        {
            if (hitTrigger == true)
            {
                transf.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transf.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
    }
}
