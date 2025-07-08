using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrngth = 100f;
    [SerializeField] float rotationStrngth = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem MainboostParticle;
    [SerializeField] ParticleSystem LeftboostParticle;
    [SerializeField] ParticleSystem RightboostParticle;
    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()             // 오브젝트가 켜질 때마다 호출됨 
    {                           // (씬 로드 직후           
        thrust.Enable();
        rotation.Enable();       //  OR gameObject.SetActive(true) 했을 때 
    }                           //  OR 스크립트 컴포넌트 체크박스 다시 켰을 때)

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrngth * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!MainboostParticle.isPlaying)
        {
            MainboostParticle.Play();
        }
    }
    private void StopThrusting()
    {
        MainboostParticle.Stop();
        audioSource.Stop();
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateLeft()
    {
        if (!LeftboostParticle.isPlaying)
        {
            RightboostParticle.Stop();
            LeftboostParticle.Play();
        }
        ApplyRotation(-rotationStrngth);
    }

    private void RotateRight()
    {
        if (!RightboostParticle.isPlaying)
        {
            LeftboostParticle.Stop();
            RightboostParticle.Play();
        }
        ApplyRotation(rotationStrngth);
    }

    private void StopRotating()
    {
        LeftboostParticle.Stop();
        RightboostParticle.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}

