using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CameraState {
    NORMAL,
    OVERVIEW,
}

public class OverviewController : MonoBehaviour
{
    public GameObject m_mainCamera;
    public GameObject m_overviewCamera;

    private CameraState m_state = CameraState.NORMAL;

    private bool m_insideTrigger = false;

    void Update()
    {
        if (!m_insideTrigger) {
            if (m_state == CameraState.OVERVIEW) {
                LeaveOverview();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && m_state == CameraState.NORMAL) {
            EnterOverview();
        } else if(Input.GetKeyUp(KeyCode.Tab) && m_state == CameraState.OVERVIEW) {
            LeaveOverview();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("OverviewTrigger")) {
            this.m_insideTrigger = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("OverviewTrigger")) {
            this.m_insideTrigger = false;
        }
    }

    private void EnterOverview() {
        m_mainCamera.SetActive(false);
        m_overviewCamera.SetActive(true);
        m_state = CameraState.OVERVIEW;
    }

    private void LeaveOverview() {
        m_mainCamera.SetActive(true);
        m_overviewCamera.SetActive(false);
        m_state = CameraState.NORMAL;
    }
}
