using UnityEngine;

[CreateAssetMenu(menuName = "EndlessRunner/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int Money { get; set; }
    public float HighScore { get; set; }
    public SkinData SelectedSkin { get; set; }
}