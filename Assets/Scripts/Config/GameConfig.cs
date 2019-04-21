using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EndlessRunner/GameConfig")]
public class GameConfig : ScriptableObject {

    [Header("General")]
    public UI uiPrefab;
    public Color greenColor;
    public Color redColor;
    public Color blackColor;

    [Header("Player")]
    public Player playerPrefab;
    public float intialSpeed;
    public float speedIncPerSecond;
    public float initialJumpForce;
    public float maxJumpForce;
    public float gravityMultiplyer;
    public float playerDeathHeight;

    [Header("World")]
    public PlatformSpawner platformSpawnerPrefab;
    public float platformMinHorzDist;
    public float platformMaxHorzDist;
    public float platformMinVertDist;
    public float platformMaxVertDist;
    public float platformMinLength;
    public float platformMaxLength;
    public BasePlatformConfig platformConfig;
    public float greenPlatformJumpMult;
    public float redPlatformJumpMult;
    public float blackPlatformEffectTime;


    public Color GetColor(ColorType type) {
        switch (type) {
            case ColorType.Green:
                return greenColor;
            case ColorType.Red:
                return redColor;
            case ColorType.Black:
                return blackColor;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
    }
}

public enum ColorType {
    Green = 0,
    Red = 1,
    Black = 2,
}
