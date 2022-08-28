using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class LevelController : MonoBehaviour
{
    public int m_levelIndex;
    public int m_lastLevelIndex;

    public float m_loadDelay = 5.0f;

    public GameObject m_playerObj;

    public GameObject m_levelFinishLayer;

    public bool m_debugMode = false;

    public void Start() {
        if (m_debugMode) {
            StartCoroutine(this.RunDelayed(() => {
                this.OnLevelCompleted();
            }, this.m_loadDelay));
        }
    }

    private void SwitchPlayerControl(bool enabled) {
        var playerCtrl = m_playerObj.GetComponent<SimpleSampleCharacterControl>();
        if (playerCtrl != null) {
            playerCtrl.enabled = enabled;
        }

        var overviewCtrl = m_playerObj.GetComponent<OverviewController>();
        if (overviewCtrl != null) {
            overviewCtrl.enabled = enabled;
        }

        var dimCtrl = m_playerObj.GetComponent<DimensionChanger>();
        if (dimCtrl != null) {
            dimCtrl.enabled = enabled;
        }
    }

    public void OnLevelCompleted() {
        this.SwitchPlayerControl(false);

        this.m_levelFinishLayer.SetActive(true);

        if (m_levelIndex != m_lastLevelIndex) {
            // Ainda temos mais níveis! Carregar a próxima
            // fase.
            StartCoroutine(this.RunDelayed(() => {
                var sceneName = "Level0_" + (this.m_levelIndex + 1);

                SceneManager.LoadScene(sceneName);
            }, this.m_loadDelay));
        }
    }

    private delegate void Delayed();

    private IEnumerator RunDelayed(Delayed fn, float delay) {
        float timer = delay;

        while (timer > 0) {
            timer -= Time.deltaTime;
            yield return null;
        }

        fn();
    }
}