using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Text _skinNameText;
    [SerializeField] private Text _skinPriceText;
    private SkinData _skinData;

    public void Setup(SkinData skinData)
    {
        _skinData = skinData;
        _skinNameText.text = skinData.Name;
        if (_skinData.Price > 0)
        {
            _skinPriceText.text = skinData.Price.ToString();
        }
        else
        {
            _skinPriceText.text = "";
        }
    }

    public void OnSelect()
    {
        var dataAccessService = GameManager.Instance.GetDataService();
        if (GameManager.Instance.GetMoney() >= _skinData.Price)
        {
            GameManager.Instance.DecreaseMoney(_skinData.Price);
            dataAccessService.SaveSkin(_skinData.Id);
            dataAccessService.GetSkinById(_skinData.Id).Price = 0;
            _skinPriceText.text = "";
        }
    }
}