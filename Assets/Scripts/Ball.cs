using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    public float maxSpeed = 3;
    public float minSpeed;

    private AudioSource audioSource;
    public AudioClip bounceSound;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {

        minSpeed = maxSpeed * 0.4f;

        if (transform.parent == null)
        {

            if (Math.Abs(m_Rigidbody.velocity.x) < 0.1f)
            {
                Debug.Log("Extra impulse used");
                m_Rigidbody.AddForce(Mathf.Sign(UnityEngine.Random.Range(1,-1)), 0, 0);

            }

        }

    }

    private void OnCollisionExit(Collision other)
    {

        audioSource.PlayOneShot(bounceSound);
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.02f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Math.Abs(Vector3.Dot(velocity.normalized, Vector3.up)) < 0.1f)
        {
            Debug.Log("Stuck prevention used " + Vector3.Dot(velocity.normalized, Vector3.up));
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        //min velocity
        if (velocity.magnitude < minSpeed)
        {
            velocity = velocity.normalized * minSpeed;
        }

        //Debug.Log(velocity.magnitude);
        m_Rigidbody.velocity = velocity;
    }
}
