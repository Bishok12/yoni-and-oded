using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataAccessService : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    private List<SkinData> _skinsData;

    private void Awake()
    {
        _skinsData = new List<SkinData>();
        _skinsData = Resources.LoadAll<SkinData>("Skins").ToList();
        Debug.Log(_skinsData.Count);
    }

    public void SaveData(float score, int money, int skinId)
    {
        _playerData.Money = money;
        _playerData.HighScore = score > _playerData.HighScore? score : _playerData.HighScore;
        _playerData.SelectedSkin = GetSkinById(skinId);
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
        return _skinsData[0];
    }

    public SkinData GetSkinById(int skinId)
    {
        Debug.Log("Loading");
        return _skinsData.Find(skin => skin.Id == skinId);
    }

    public void SaveSkin(int skinId)
    {
        _playerData.SelectedSkin = GetSkinById(skinId);
    }
}
