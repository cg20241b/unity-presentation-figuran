using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;

    public GameObject muzzleFlash;
    public GameObject impact;

    // Start is called before the first frame update
    void Start()
    { 
        if(muzzleFlash != null){
            var muzzleVFX = Instantiate (muzzleFlash, transform.position, Quaternion.identity); //create the muzzle effect
            muzzleVFX.transform.forward = gameObject.transform.forward; //face the same direction as the projectile
            Destroy (muzzleVFX, 1f); //destroy the muzzle effect after 1 second
        } else {
            Debug.Log ("No Muzzle Prefab");
        }
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if(speed != 0){
            transform.position += transform.forward * (speed *Time.deltaTime);
        } else {
            Debug.Log("No Speed");
        }
    }
    void OnCollisionEnter (Collision co){
        speed = 0;
        ContactPoint contact = co.contacts[0]; //Retrieve the Contact Point
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal); // Calculate the Rotation
        Vector3 pos = contact.point; // Get the Position
        if(impact != null){
            var hitVFX = Instantiate (impact, pos, rot);
            Destroy (hitVFX, 1f); // Destroy the Impact Effect after 1 second
        } else {
            Debug.Log ("No Hit Prefab");
        }
        Destroy (gameObject);
    }
}
