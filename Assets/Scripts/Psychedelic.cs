using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psychedelic : MonoBehaviour
{
    public float rotationSpeed = 50;
    private GameObject objectToDelete;

    public bool orbit = false;
    public float orbitSpeed = 100;
    public GameObject orbitTarget;

    // Start is called before the first frame update
    void Start()
    {
        objectToDelete = gameObject; // Grab the GameObject
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Make our GameObject rotate

        if (orbit == true)
        {
            transform.RotateAround(orbitTarget.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with Player
        if (collision.gameObject.CompareTag("Player"))
        {
            SpecialEffects.playerEffected = true; // Enable the special effect bool
            Destroy(objectToDelete); // Destroy ourself
        }
    }
}
