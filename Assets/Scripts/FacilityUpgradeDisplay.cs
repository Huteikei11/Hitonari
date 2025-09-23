using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FacilityUpgradeDisplay : MonoBehaviour
{
    [Header("UI References")]
    public GameObject upgradePanel; // 施設アップグレード表示パネル
    public TextMeshProUGUI upgradeText; // アップグレード内容を表示するテキスト
    public Button continueButton; // 続行ボタン

    private System.Action onContinueCallback; // 続行時のコールバック

    void Start()
    {
        // 初期状態では非表示
        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        // ボタンのイベント設定
        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinueClicked);
    }

    /// <summary>
    /// 施設アップグレード情報を表示します
    /// </summary>
    /// <param name="upgradeInfo">アップグレード情報</param>
    /// <param name="onContinue">続行時のコールバック</param>
    public void ShowFacilityUpgrade(string upgradeInfo, System.Action onContinue = null)
    {
        if (upgradePanel == null || upgradeText == null)
        {
            Debug.LogError("UI要素が設定されていません");
            onContinue?.Invoke();
            return;
        }

        onContinueCallback = onContinue;

        // アップグレード情報を表示
        if (string.IsNullOrEmpty(upgradeInfo))
        {
            upgradeText.text = "今日は特に施設のアップグレードはありませんでした。";
        }
        else
        {
            upgradeText.text = $"今日の施設アップグレード:\n{upgradeInfo}";
        }

        // パネルを表示
        upgradePanel.SetActive(true);
    }

    /// <summary>
    /// 続行ボタンがクリックされた時の処理
    /// </summary>
    private void OnContinueClicked()
    {
        // パネルを非表示
        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        // コールバックを実行
        onContinueCallback?.Invoke();
        onContinueCallback = null;
    }

    /// <summary>
    /// 外部からパネルを閉じる場合
    /// </summary>
    public void HidePanel()
    {
        if (upgradePanel != null)
            upgradePanel.SetActive(false);
        onContinueCallback = null;
    }
}