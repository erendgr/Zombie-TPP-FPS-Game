using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float speed;

    public float playerHP = 100;
    public bool isDead;
    public AudioClip collectClip;
    private AudioSource audioSource;
    //public bool isRunning;
    
    void Start()
    {
        isDead = false;
        //isRunning = false;
        //animator.SetBool("isRunning", false);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    
    
    void Update()
    {
        if (playerHP <= 0)
        {
            isDead = true;  
            animator.SetBool("Died", isDead);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 4;
            animator.SetBool("isRunning", true);
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2;
            animator.SetBool("isRunning", false);
        }
        
        if (!isDead)
        {
            Movement();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
    public float Healt()
    {
        return playerHP;
    }
    
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * vertical * speed * Time.deltaTime);
        
    }
    
    public void GetDamagePlayer()
    {
        playerHP -= 20;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aid"))
        {
            playerHP += 25;
            if (playerHP > 100) playerHP = 100;
            audioSource.PlayOneShot(collectClip);
            Destroy(other.gameObject);
        }
    }
}
