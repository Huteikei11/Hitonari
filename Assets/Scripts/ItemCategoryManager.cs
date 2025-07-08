using UnityEngine;
using UnityEngine.UI; // Imageコンポーネントを使うために必要
using System.Collections.Generic;
using TMPro;

public class ItemCategoryManager : MonoBehaviour
{
    // --- インスペクターで設定する項目 ---
    [Header("UI References")]
    public GameObject itemDetailPanel;
    public Image itemImage; // --- ここを追加: アイテム画像表示用Image ---
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemEffectText;
    public TextMeshProUGUI itemLevelText;
    public TextMeshProUGUI itemCostText;
    public TextMeshProUGUI itemOwnedCountText;

    public Button buyButton;
    public Button useButton;

    [Header("Settings")]
    public string categoryName;

    // --- 内部使用 ---
    private SaveManager saveManager;
    private string currentSelectedItemName;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManagerが見つかりません。シーンに配置されていますか？");
            enabled = false;
            return;
        }

        buyButton.onClick.AddListener(OnBuyButtonClicked);
        useButton.onClick.AddListener(OnUseButtonClicked);

        itemDetailPanel.SetActive(false);
    }

    public void OnItemSelectButtonClicked(string itemName)
    {
        currentSelectedItemName = itemName;
        DisplayItemDetails(itemName);
        itemDetailPanel.SetActive(true);
    }

    public void OnCloseItemDetailPanel()
    {
        itemDetailPanel.SetActive(false);
        currentSelectedItemName = null; // 選択されたアイテム名をリセット
    }

    // アイテム詳細を表示するメソッド
    private void DisplayItemDetails(string itemName)
    {
        Dictionary<string, ItemEffect> itemDefinitions = GetItemDefinitionsDictionary(categoryName);
        Dictionary<string, int> itemCounts = GetPlayerItemCountsDictionary(categoryName);

        if (itemDefinitions == null || itemCounts == null || !itemDefinitions.ContainsKey(itemName))
        {
            Debug.LogError($"アイテム情報が見つかりません: {itemName}");
            itemDetailPanel.SetActive(false);
            return;
        }

        ItemEffect itemDef = itemDefinitions[itemName];
        int ownedCount = itemCounts.GetValueOrDefault(itemName, 0);

        // --- ここから追加 ---
        if (itemImage != null)
        {
            itemImage.sprite = itemDef.itemSprite; // アイテムのSpriteを設定
            // Spriteが設定されていない場合はImageを非表示にする（任意）
            itemImage.enabled = itemDef.itemSprite != null;
        }
        // --- ここまで追加 ---

        itemNameText.text = itemName;
        itemDescriptionText.text = itemDef.itemDescription;
        // Debug.Log(itemDef.itemDescription);
        itemEffectText.text = GetEffectDescription(itemDef);

        itemLevelText.text = $"レベル: {itemDef.CurrentLevel} / {itemDef.MaxLevel}";
        itemCostText.text = $"購入コスト: {itemDef.PurchaseCost} ちんぽミルク";
        itemOwnedCountText.text = $"所持数: {ownedCount}";

        buyButton.interactable = saveManager.playerData.DickMilk >= itemDef.PurchaseCost;
        useButton.interactable = ownedCount > 0 && itemDef.CurrentLevel < itemDef.MaxLevel;
    }

    // GetEffectDescription, OnBuyButtonClicked, OnUseButtonClicked, GetItemDictionary, GetPlayerItemCountsDictionary
    // の各メソッドは変更ありません。
    private string GetEffectDescription(ItemEffect itemDef)
    {
        List<string> effects = new List<string>();
        if (itemDef.BaseEjaculation > 0) effects.Add($"基本射精量 +{itemDef.BaseEjaculation}");
        if (itemDef.DickPowerEjaculation > 0) effects.Add($"ちんぽ強化射精量 +{itemDef.DickPowerEjaculation}");
        if (itemDef.BallPowerEjaculation > 0) effects.Add($"金玉強化射精量 +{itemDef.BallPowerEjaculation}");
        if (itemDef.FacilityMultiplier > 0) effects.Add($"設備強化倍率 +{itemDef.FacilityMultiplier:F2}");
        if (itemDef.DickLength > 0) effects.Add($"ちんぽ長さ +{itemDef.DickLength}cm");
        if (itemDef.BallLevel > 0) effects.Add($"金玉レベル +{itemDef.BallLevel}");
        if (itemDef.SpermSize > 0) effects.Add($"精子サイズ +{itemDef.SpermSize:F2}mm");
        if (itemDef.SemenDensity > 0) effects.Add($"精液濃度 +{itemDef.SemenDensity}");
        if (itemDef.PleasureAddiction > 0) effects.Add($"快楽中毒度 +{itemDef.PleasureAddiction}");
        if (itemDef.PussyLevel > 0) effects.Add($"おまんこレベル +{itemDef.PussyLevel}");
        if (itemDef.AnalLevel > 0) effects.Add($"あなるレベル +{itemDef.AnalLevel}");
        if (itemDef.BoobLevel > 0) effects.Add($"おっぱいレベル +{itemDef.BoobLevel}");

        return effects.Count > 0 ? string.Join(", ", effects) : "効果なし";
    }

    private void OnBuyButtonClicked()
    {
        if (string.IsNullOrEmpty(currentSelectedItemName)) return;

        bool success = saveManager.BuyItem(categoryName, currentSelectedItemName);
        if (success)
        {
            Debug.Log($"{currentSelectedItemName} の購入に成功！");
            DisplayItemDetails(currentSelectedItemName); // UIを更新
        }
    }

    private void OnUseButtonClicked()
    {
        if (string.IsNullOrEmpty(currentSelectedItemName)) return;

        bool success = saveManager.UseItem(categoryName, currentSelectedItemName);
        if (success)
        {
            Debug.Log($"{currentSelectedItemName} の使用に成功！");
            DisplayItemDetails(currentSelectedItemName); // UIを更新
        }
    }

    private Dictionary<string, ItemEffect> GetItemDefinitionsDictionary(string category)
    {
        switch (category)
        {
            case "Eientei": return saveManager.EienteiItems;
            case "Workshop": return saveManager.WorkshopItems;
            case "Laboratory": return saveManager.LaboratoryItems;
            default:
                Debug.LogError($"未知のアイテムカテゴリ: {category}");
                return null;
        }
    }

    private Dictionary<string, int> GetPlayerItemCountsDictionary(string category)
    {
        switch (category)
        {
            case "Eientei": return saveManager.playerData.EienteiItemCounts;
            case "Workshop": return saveManager.playerData.WorkshopItemCounts;
            case "Laboratory": return saveManager.playerData.LaboratoryItemCounts;
            default:
                Debug.LogError($"未知のアイテムカテゴリ: {category}");
                return null;
        }
    }
}