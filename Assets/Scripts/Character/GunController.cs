using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GunController : MonoBehaviour
{
    private Camera _camera;
    public LayerMask mask;
    PlayerController _player;
    private Animator _animator;
    public ParticleSystem muzzleFlash;

    public float ammo = 120;
    public float clip = 30;
    public float clipCap = 30;
    private float nextTimeToFire = 0f;
    public float fireRate = 5f;
    
    private AudioSource _audio;
    public AudioClip shotClip;
    public AudioClip ReloadClip;
    public AudioClip CollectClip;
    
    public UIController _ui;
    public AudioClip emptyClip;
    
    public TextMeshProUGUI warningText;
    private Tween blinkingTween;
    
    private void Start()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerController>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_player.isDead)
        {
            return;
        }

        if (_ui.isPaused)
        {
            return;
        }
        
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            if (clip > 0)
            {
                _animator.SetBool("Shoot", true);
                nextTimeToFire = Time.time + 1f / fireRate;
                Fire();
            }
            if (clip <= 0)
            {
                //_audio.PlayOneShot(emptyClip);
                //_animator.SetBool("Shoot", false);
            }
            if (clip <= 0 && ammo > 0)
            {
                muzzleFlash.Stop();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (clip <= 0)
            {
                _audio.PlayOneShot(emptyClip);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _animator.SetBool("Shoot", false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _animator.SetBool("Reload", true);
            //StopBlinking();
        }
        
        if (clip <= 0 && (blinkingTween == null || !blinkingTween.IsActive()))
        {
            StartBlinking();
        }

    }

    void StartBlinking()
    {
        warningText.gameObject.SetActive(true);
        blinkingTween = warningText.DOFade(0, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void StopBlinking()
    {
        if (blinkingTween != null)
        {
            warningText.alpha = 1f;
            blinkingTween.Kill();
            blinkingTween = null;
        }
        warningText.gameObject.SetActive(false);
    }
    
    public void ClipChanger()
    {
        float a = clipCap - clip;
        ammo -= a;
        clip += a;
        _animator.SetBool("Reload", false);
    }
    
    
    public void ClipChangeSound()
    {
        _audio.PlayOneShot(ReloadClip);
    }
    
    private void Fire()
    {
        
        if (clip > 0)
        {
            muzzleFlash.Play();
            _audio.PlayOneShot(shotClip);
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                // Debug.Log(hit.collider.gameObject.name);
                // Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            
                ZombieController zombieController = hit.collider.gameObject.GetComponent<ZombieController>();
                if (zombieController != null)
                {
                    zombieController.GetDamageZombie();
                }
                else
                {
                    Debug.Log("ray didnt hit");
                }
            }
        }
        clip--;
        
    }

    public float Clip()
    {
        return clip;
    }
    public float Ammo()
    {
        return ammo;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            ammo += 20;
            if (ammo > 120) ammo = 120;
            _audio.PlayOneShot(CollectClip);
            Destroy(other.gameObject);
        }
    }
}