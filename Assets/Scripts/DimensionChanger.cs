using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

enum DimChangerState {
    INACTIVE = 0,
    MINUS,
    PLUS,
}

public class DimensionChanger : MonoBehaviour
{
    public int m_maxDim = 0;
    public int m_minDim = 0;

    public Vector3 m_stride = new Vector3(0.0f, 0.0f, 10.0f);
    public float m_translationSpeed = 100.0f;

    public float m_epsilon = 0.001f;

    public TextMeshProUGUI m_dimText;

    private DimChangerState m_state = DimChangerState.INACTIVE;

    private int m_dimNumber = 0;

    private OverrideZPosController m_zOverride;

    public void Start() {
        this.UpdateDimensionText();
        this.m_zOverride = this.GetComponent<OverrideZPosController>();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.C) && this.m_state != DimChangerState.INACTIVE) {
            int direction = m_state == DimChangerState.MINUS
                ? -1
                :  1;

            var nextDim = m_dimNumber + direction;
            if (nextDim >= m_minDim && nextDim <= m_maxDim) {
                m_dimNumber = nextDim;

                StartCoroutine(MoveTo(this.transform.position + direction * m_stride, m_translationSpeed));

                this.UpdateDimensionText();
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTags.DIM_TRIGGER_PLUS)) {
            this.m_state = DimChangerState.PLUS;
        } else if(other.CompareTag(GameTags.DIM_TRIGGER_MINUS)) {
            this.m_state = DimChangerState.MINUS;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag(GameTags.DIM_TRIGGER_PLUS) || other.CompareTag(GameTags.DIM_TRIGGER_MINUS)) {
            this.m_state = DimChangerState.INACTIVE;
        }
    }

    private IEnumerator MoveTo(Vector3 to, float speed) {
        this.m_zOverride.SetOverride(false);

        while (Vector3.Distance(this.transform.position, to) >= this.m_epsilon) {
            var adjustedSpeed = speed * Time.deltaTime;

            this.transform.position = Vector3.MoveTowards(this.transform.position, to, adjustedSpeed);

            yield return null;
        }
        
        this.m_zOverride.ResetReference();
        this.m_zOverride.SetOverride(true);
}

    private void UpdateDimensionText() {
        this.m_dimText.text = "dim: " + m_dimNumber;
    }
}