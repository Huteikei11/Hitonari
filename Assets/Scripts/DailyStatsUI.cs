using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// プレイヤーの日次統計情報をUIで表示するクラス
/// </summary>
public class DailyStatsUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject statsPanel; // 統計情報表示パネル
    public TextMeshProUGUI statsText; // 統計情報を表示するテキスト
    public Button closeButton; // 閉じるボタン
    public ScrollRect scrollRect; // スクロール用（長いテキスト対応）

    private System.Action onCloseCallback; // 閉じた時のコールバック

    void Start()
    {
        // 初期化
        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseClicked);
        
        // 最初は非表示
        if (statsPanel != null)
            statsPanel.SetActive(false);
    }

    /// <summary>
    /// 統計情報を表示します
    /// </summary>
    /// <param name="statsContent">表示する統計情報のテキスト</param>
    /// <param name="onClose">閉じた時のコールバック</param>
    public void ShowDailyStats(string statsContent, System.Action onClose = null)
    {
        onCloseCallback = onClose;
        
        // UIに表示
        if (statsText != null)
            statsText.text = statsContent;

        // パネルを表示
        if (statsPanel != null)
            statsPanel.SetActive(true);

        // スクロールを一番上に戻す
        if (scrollRect != null)
            scrollRect.verticalNormalizedPosition = 1f;
    }

    /// <summary>
    /// 閉じるボタンがクリックされた時の処理
    /// </summary>
    private void OnCloseClicked()
    {
        HidePanel();
    }

    /// <summary>
    /// パネルを非表示にします
    /// </summary>
    public void HidePanel()
    {
        if (statsPanel != null)
            statsPanel.SetActive(false);

        // コールバックを実行
        onCloseCallback?.Invoke();
        onCloseCallback = null;
    }

    void OnDestroy()
    {
        // イベントリスナーをクリーンアップ
        if (closeButton != null)
            closeButton.onClick.RemoveListener(OnCloseClicked);
    }
}