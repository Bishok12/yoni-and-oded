using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    public Text progressCount;

    private bool _isRunning;

    public void Init() {
        _isRunning = true;
    }

    public void DeInit() {
        _isRunning = false;
    }

    private void Update() {
        if (!_isRunning)
            return;

        var current = GameManager.Instance.timePassed.ToString("F2");
        progressCount.text = string.Format("{0}", current);
    }
}
