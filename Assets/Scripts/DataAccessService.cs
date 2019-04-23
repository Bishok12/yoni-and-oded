using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataAccessService : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    public List<SkinData> SkinsData;

    private void Awake()
    {
        SkinsData = new List<SkinData>();
        SkinsData = Resources.LoadAll<SkinData>("Skins").ToList();
    }

    public void SaveData(float score, int money, int skinId)
    {
        _playerData.Money = money;
        _playerData.HighScore = score > _playerData.HighScore? score : _playerData.HighScore;
        _playerData.SelectedSkin = GetSkinById(skinId);
    }

    public void SaveMoney(int money)
    {
        _playerData.Money = money;
    }
    
    public float LoadHighScore()
    {
        return _playerData.HighScore;
    }
    
    public int LoadMoney()
    {
        return _playerData.Money;
    }

    public SkinData LoadSelectedSkin()
    {
        if (_playerData.SelectedSkin != null)
        {
            return _playerData.SelectedSkin;
        }
        
        SaveSkin(0);
        return SkinsData[0];
    }

    public SkinData GetSkinById(int skinId)
    {
        return SkinsData.Find(skin => skin.Id == skinId);
    }

    public void SaveSkin(int skinId)
    {
        _playerData.SelectedSkin = GetSkinById(skinId);
    }
}
