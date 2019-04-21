using System;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour {

    public event Action<float> OnPlatformEnterEvent;
    public event Action OnBlackPlatformEnterEvent;

    protected GameConfig _config;

    public bool isOnBlackout { get; set; }

    protected Material m_Material;
    private float timeBlack = 0f;

    private void Update()
    {
        if (isOnBlackout)
        {
            TurnBlack();
        }
        else if (!isOnBlackout)
        {
            TurnToOriginalColor();
        }
    }

    public void Setup(GameConfig config) {
        isOnBlackout = false;
        _config = config;
    }

    protected virtual void OnPlatformEnter(float platformJumpMult) {
        if (OnPlatformEnterEvent != null) {
            OnPlatformEnterEvent(platformJumpMult);
        }
    }

    protected virtual void OnBlackPlatformEnter() {
        if (OnBlackPlatformEnterEvent != null) {
            OnBlackPlatformEnterEvent();
        }
    }

    protected abstract void OnCollisionEnter(Collision other);

    protected abstract void TurnBlack();

    protected abstract void TurnToOriginalColor();
}
