using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform m_playerTransform;

    private Vector3 m_offset;

    void Start()
    {
        // O = T - C;
        // C = T - O
        this.m_offset = m_playerTransform.position - this.transform.position;
    }

    void Update()
    {
        this.transform.position = m_playerTransform.position - this.m_offset;
    }
}
