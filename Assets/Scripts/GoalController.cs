using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {
    public LevelController m_levelController;

    public Color m_emptyColor;
    public Color m_inhabitedColor;
    
    private GameObject m_target = null;
    private Renderer m_renderer = null;

    public void Start() {
        this.m_renderer = this.GetComponent<Renderer>();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTags.PICKUP) && this.m_target == null) {
            this.m_target = other.gameObject;
            this.m_levelController.IncrementBlock();

            this.m_renderer.material.SetColor("_Color", this.m_inhabitedColor);
        }
    }

    public void OnTriggerExit(Collider other) {
        if (this.m_target == other.gameObject) {
            this.m_target = null;
            this.m_levelController.DecrementBlock();

            this.m_renderer.material.SetColor("_Color", this.m_emptyColor);
        }
    }
}
