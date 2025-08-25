using UnityEngine;
using TMPro;

public class DayTextUpdater : MonoBehaviour
{
    public SaveManager saveManager; // SaveManagerへの参照（Inspectorでセット）
    public TextMeshProUGUI dayText; // 表示用TextMeshProUGUI（Inspectorでセット）
    private int lastDay;

    void Start()
    {
        if (saveManager == null)
            saveManager = FindObjectOfType<SaveManager>();
        UpdateText();
        lastDay = GetDay();
    }

    void Update()
    {
        int currentDay = GetDay();
        if (currentDay != lastDay)
        {
            UpdateText();
            lastDay = currentDay;
        }
    }

    private int GetDay()
    {
        return saveManager != null && saveManager.playerData != null ? saveManager.playerData.Day : 0;
    }

    private void UpdateText()
    {
        if (dayText != null)
            dayText.text = GetDay().ToString();
    }
}
