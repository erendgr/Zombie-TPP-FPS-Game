using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float delay = 5f;
    public float radius = 5f;
    public float force = 200f;
    public GameObject explosionEffect;
    float countDown;
    bool hasExploded = false;

    public AudioClip explodeSound;
    
    void Start()
    {
        countDown = delay;
    }


    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0f && !hasExploded)
        {
            Debug.Log(hasExploded);
            
            Explode();
            hasExploded = true;
        }
        
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        
        GameObject audioObject = new GameObject("ExplosionSound");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = explodeSound;
        audioSource.Play();
        Destroy(audioObject, 3);
        
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in collidersToDestroy)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if(dest != null)
            {
                dest.Destroy();
            }
        }   

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

        }

        Destroy(gameObject);
    }
}
