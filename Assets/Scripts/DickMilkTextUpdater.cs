using UnityEngine;
using TMPro;

public class DickMilkTextUpdater : MonoBehaviour
{
    public SaveManager saveManager; // SaveManagerへの参照（Inspectorでセット）
    public TextMeshProUGUI dickMilkText; // 表示用TextMeshProUGUI（Inspectorでセット）
    private int lastDickMilk;

    void Start()
    {
        if (saveManager == null)
            saveManager = FindObjectOfType<SaveManager>();
        UpdateText();
        lastDickMilk = GetDickMilk();
    }

    void Update()
    {
        int currentDickMilk = GetDickMilk();
        if (currentDickMilk != lastDickMilk)
        {
            UpdateText();
            lastDickMilk = currentDickMilk;
        }
    }

    private int GetDickMilk()
    {
        return saveManager != null && saveManager.playerData != null ? saveManager.playerData.DickMilk : 0;
    }

    private void UpdateText()
    {
        if (dickMilkText != null)
            dickMilkText.text = GetDickMilk().ToString();
    }
}
