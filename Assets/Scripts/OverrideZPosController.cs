using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideZPosController : MonoBehaviour
{
    private Vector3 m_lastPos;
    private bool m_enabled = true;

    void Start() {
        this.m_lastPos = this.transform.position;
    }

    void Update() {
        if (!this.m_enabled) {
            return;
        }

        var newPos = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            m_lastPos.z
        );

        this.transform.position = newPos;
    }

    public void SetOverride(bool enabled) {
        this.m_enabled = enabled;
    }

    public void ResetReference() {
        this.m_lastPos = this.transform.position;
    }
}
