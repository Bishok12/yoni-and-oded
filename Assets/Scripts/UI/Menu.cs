using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Text title;
    public Text end;
    public Text Summary;

    public enum MenuType {
        Start,
        End,
    }

    public void Init(MenuType type) {
        title.gameObject.SetActive(type == MenuType.Start);
        end.gameObject.SetActive(type == MenuType.End);
        
        Summary.gameObject.SetActive(type != MenuType.Start);
        Summary.text = string.Format("Time Passed: {0} seconds\nHigh Score: {1} seconds",
            GameManager.Instance.timePassed.ToString("F2"),
            GameManager.Instance.highScore.ToString("F2"));
    }

    public void StartGame() {
        GameManager.Instance.StartGame();
    }

    public void DeInit() {

    }
}
