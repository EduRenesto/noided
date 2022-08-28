using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public enum PickupState {
        IDLE,
        INSIDE_TRIGGER,
        HOLDING,
    };

    private PickupState m_state = PickupState.IDLE;
    private GameObject m_target = null;
    private Vector3 m_offset;

    public void OnTriggerEnter(Collider other) {
        if (this.m_state != PickupState.IDLE) {
            return;
        }

        if (other.CompareTag(GameTags.PICKUP)) {
            this.m_target = other.gameObject;
            this.m_state = PickupState.INSIDE_TRIGGER;

            this.m_offset = other.transform.position - this.transform.position;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag(GameTags.PICKUP) && this.m_state != PickupState.HOLDING) {
            this.m_state = PickupState.IDLE;
            this.m_target = null;
        }
    }

    public void Update() {
        if (this.m_state == PickupState.HOLDING && this.m_target != null) {
            this.m_target.transform.position = this.transform.position + this.m_offset;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            switch (this.m_state) {
            case PickupState.INSIDE_TRIGGER:
                this.m_state = PickupState.HOLDING;
                break;
            case PickupState.HOLDING:
                this.m_state = PickupState.INSIDE_TRIGGER;
                break;
            default:
                break;
            }
        }
    }
}