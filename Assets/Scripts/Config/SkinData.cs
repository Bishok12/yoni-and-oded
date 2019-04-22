using UnityEngine;

[CreateAssetMenu(menuName = "EndlessRunner/Skin")]
public class SkinData : ScriptableObject
{
    public int Id;
    public int Price;
    public string Name;
    public Color LightColor;
    public bool ShowHead;
    public bool ShowTorso;
    public bool ShowLegs;
    public bool ShowScarf;
}