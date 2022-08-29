using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int m_levelIndex;
    public int m_lastLevelIndex;

    public int m_totalBlocks = 0;

    public float m_loadDelay = 5.0f;

    public GameObject m_playerObj;

    public GameObject m_levelFinishLayer;

    public bool m_debugMode = false;

    private int m_completedBlocks = 0;

    private float timeLeft = 120; //secs
    private float minutesLeft = 2;
    private float secondsLeft = 0;
    public Text timeDisplay;

    public void Update() {
        if (timeLeft > 0) {
            timeLeft = timeLeft - Time.deltaTime;
            minutesLeft = Mathf.FloorToInt(timeLeft / 60);
            secondsLeft = Mathf.FloorToInt(timeLeft % 60);
            timeDisplay.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
        } else if (timeLeft < 0) {
            this.SwitchPlayerControl(false);
            timeDisplay.text = "Over!";
        }
    }

    public void Start() {
        if (m_debugMode) {
            StartCoroutine(this.RunDelayed(() => {
                this.OnLevelCompleted();
            }, this.m_loadDelay));
        }
    }

    public void IncrementBlock() {
        this.m_completedBlocks++;

        if (m_completedBlocks == this.m_totalBlocks) {
            // Level complete!
            this.OnLevelCompleted();
        }
    }

    public void DecrementBlock() {
        if (this.m_completedBlocks > 0) {
            this.m_completedBlocks--;
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