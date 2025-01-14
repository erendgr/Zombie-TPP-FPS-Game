using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public float zombieHP = 100f;
    private bool zombieDead = false;
    private Animator _animator;
    private GameObject _player;
    private float distance;
    private NavMeshAgent _navMesh;
    public float followDistance;
    public float attackDistance;
    public float attackRange;
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.Find("Swat");
        _navMesh = GetComponent<NavMeshAgent>();        
    }


    void Update()
    {
        if (zombieHP <= 0)
        {
            zombieDead = true;
        }

        if (zombieDead)
        {
            _animator.SetBool("Walking", false);
            _animator.SetBool("Attacking", false);
            _navMesh.isStopped = true;
            _animator.SetBool("Died", true);
            StartCoroutine(DeathSequence());
        }
        else
        {
            distance = Vector3.Distance(transform.position, _player.transform.position);
            if (distance < followDistance)
            {
                _navMesh.isStopped = false;
                _navMesh.SetDestination(_player.transform.position);
                _animator.SetBool("Walking", true);
                _animator.SetBool("Attacking", false);
                transform.LookAt(_player.transform);
            }
            else
            {
                _navMesh.isStopped = true;
                _animator.SetBool("Walking", false);
                _animator.SetBool("Attacking", false);

            }

            if (distance < attackDistance)
            {
                transform.LookAt(_player.transform);
                _navMesh.isStopped = true;
                _animator.SetBool("Walking", false);
                _animator.SetBool("Attacking", true);
            }
        }
    }

    public void HitDamage()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < attackRange)
        {
            _player.GetComponent<PlayerController>().GetDamagePlayer();
        }
        
        
    }
    
    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    public void GetDamageZombie()
    {
        zombieHP -= 20;
    }
    
}
