using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    public float m_speed;

    private Rigidbody m_rigidBody;

    void Start() {
        this.m_rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var adjustedSpeed = this.m_speed * Time.deltaTime * Input.GetAxis("Horizontal");

        this.m_rigidBody.AddForce(Vector3.right * adjustedSpeed);
    }
}
