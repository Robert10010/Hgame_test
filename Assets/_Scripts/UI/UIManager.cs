using UnityEngine;
using UnityEngine.UI;
using TMPro; // 如果使用 TextMeshPro

public class UIManager : MonoBehaviour
{
    [Header("UI 參考")]
    public Text moneyText; // 或 TextMeshProUGUI
    public Text timeText;

    private void Start()
    {
        // 初始更新
        if (GameManager.Instance != null)
        {
            UpdateMoneyUI(GameManager.Instance.Money);
            GameManager.Instance.OnMoneyChanged.AddListener(UpdateMoneyUI);
        }
    }

    private void Update()
    {
        if (TimeManager.Instance != null && timeText != null)
        {
            float time = TimeManager.Instance.timeOfDay;
            // 簡單將 0-1 轉換為 24小時制字串
            int hour = Mathf.FloorToInt(time * 24f);
            int minute = Mathf.FloorToInt((time * 24f - hour) * 60f);
            timeText.text = $"時間: {hour:00}:{minute:00}";
        }
    }

    private void UpdateMoneyUI(int amount)
    {
        if (moneyText != null)
        {
            moneyText.text = $"金錢: ${amount}";
        }
    }

    // 按鈕點擊事件：選擇建造建築
    public void OnSelectBrothel()
    {
        // 這裡通常會呼叫 GridPlacementSystem 來設定當前要建造的 prefab
        // 暫時僅作 Debug Log
        Debug.Log("選擇了建造娼館");
    }
}
