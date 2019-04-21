using UnityEngine;

public abstract class BasePlatformConfig : ScriptableObject {
    public abstract void ResetConfig();
    public abstract BasePlatform GetNextPrefab();
}
