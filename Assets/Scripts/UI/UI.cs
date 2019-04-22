using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Menu Menu;
    public HUD HUD;

    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _highScoreText;


    public void SetMenuMode(Menu.MenuType menuType)
    {
        Menu.gameObject.SetActive(true);
        Menu.Init(menuType);

        HUD.gameObject.SetActive(false);
        HUD.DeInit();
    }

    public void SetHUDMode()
    {
        Menu.gameObject.SetActive(false);
        Menu.DeInit();

        HUD.gameObject.SetActive(true);
        HUD.Init();
    }

    public void SetMoneyText(int money)
    {
        _moneyText.text = "Money: " + money;
    }

    public void SetHighScoreText(float score)
    {
        _highScoreText.text = "HighScore: " + score.ToString("F2");
    }

    public void UpdateData(int money, float score)
    {
        SetMoneyText(money);
        SetHighScoreText(score);
    }
}