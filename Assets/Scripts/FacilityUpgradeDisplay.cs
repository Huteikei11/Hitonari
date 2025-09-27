using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FacilityUpgradeDisplay : MonoBehaviour
{
    [Header("UI References")]
    public GameObject upgradePanel; // 施設アップグレード表示パネル
    public TextMeshProUGUI upgradeText; // アップグレード内容を表示するテキスト
    public TextMeshProUGUI playerInfoText; // プレイヤー情報を表示するテキスト
    public Button continueButton; // 続行ボタン
    public ScrollRect scrollRect; // スクロール用（長い情報対応）

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
        ShowFacilityUpgradeWithPlayerInfo(upgradeInfo, null, onContinue);
    }

    /// <summary>
    /// 施設アップグレード情報とプレイヤー情報を表示します
    /// </summary>
    /// <param name="upgradeInfo">アップグレード情報</param>
    /// <param name="playerInfo">プレイヤー情報（nullの場合は表示しない）</param>
    /// <param name="onContinue">続行時のコールバック</param>
    public void ShowFacilityUpgradeWithPlayerInfo(string upgradeInfo, string playerInfo = null, System.Action onContinue = null)
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
            upgradeText.text = "<color=#FFD700>=== 一日終了 ===</color>\n\n今日は特に施設のアップグレードはありませんでした。";
        }
        else
        {
            upgradeText.text = $"<color=#FFD700>=== 一日終了 ===</color>\n\n<color=#87CEEB>今日の施設アップグレード:</color>\n{upgradeInfo}";
        }

        // プレイヤー情報を表示（設定されている場合）
        if (playerInfoText != null)
        {
            if (!string.IsNullOrEmpty(playerInfo))
            {
                playerInfoText.text = playerInfo;
                playerInfoText.gameObject.SetActive(true);
            }
            else
            {
                playerInfoText.gameObject.SetActive(false);
            }
        }

        // スクロールを一番上に戻す
        if (scrollRect != null)
            scrollRect.verticalNormalizedPosition = 1f;

        // パネルを表示
        upgradePanel.SetActive(true);
    }

    /// <summary>
    /// プレイヤー情報のテキストを生成します
    /// </summary>
    /// <param name="playerData">プレイヤーデータ</param>
    /// <returns>フォーマットされたプレイヤー情報テキスト</returns>
    public static string GeneratePlayerInfoText(PlayerData playerData)
    {
        if (playerData == null) return "";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        // ヘッダー
        sb.AppendLine("<color=#FFB6C1>=== 現在のステータス ===</color>");
        //sb.AppendLine();

        // 基本情報
        sb.AppendLine($"<color=#FFFF80>Day:</color> {playerData.Day}");
        sb.AppendLine($"<color=#FFFF80>ちんぽミルク:</color> {playerData.DickMilk}");
        sb.AppendLine($"<color=#FFFF80>累計ちんぽミルク:</color> {playerData.TotalDickMilk}");
        sb.AppendLine($"<color=#FFFF80>射精量:</color> {playerData.EjaculationAmount:F2}");
        //sb.AppendLine();

        // 身体パラメータ
        sb.AppendLine("<color=#FFB6C1>--- 身体パラメータ ---</color>");
        sb.AppendLine($"ちんぽの長さ: {playerData.DickLength}cm");
        sb.AppendLine($"金玉レベル: {playerData.BallLevel}");
        sb.AppendLine($"精子サイズ: {playerData.SpermSize:F3}");
        sb.AppendLine($"精液密度: {playerData.SemenDensity}");
        sb.AppendLine($"快楽中毒度: {playerData.PleasureAddiction}");
        //sb.AppendLine();

        // 性感帯レベル
        sb.AppendLine("<color=#FF69B4>--- 性感帯レベル ---</color>");
        sb.AppendLine($"おまんこレベル: {playerData.PussyLevel}");
        sb.AppendLine($"アナルレベル: {playerData.AnalLevel}");
        sb.AppendLine($"おっぱいレベル: {playerData.BoobLevel}");
        //sb.AppendLine();

        // 射精量の内訳
        sb.AppendLine("<color=#87CEEB>--- 射精量の内訳 ---</color>");
        sb.AppendLine($"基本射精量: {playerData.BaseEjaculation:F2}");
        sb.AppendLine($"ちんぽパワー: {playerData.DickPowerEjaculation:F2}");
        sb.AppendLine($"金玉パワー: {playerData.BallPowerEjaculation:F2}");
        sb.AppendLine($"快楽中毒: {playerData.AddictionEjaculation:F2}");
        sb.AppendLine($"施設倍率: x{playerData.FacilityMultiplier:F2}");

        return sb.ToString();
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

    void OnDestroy()
    {
        // イベントリスナーをクリーンアップ
        if (continueButton != null)
            continueButton.onClick.RemoveListener(OnContinueClicked);
    }
}