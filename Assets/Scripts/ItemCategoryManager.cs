using UnityEngine;
using UnityEngine.UI; // Image�R���|�[�l���g���g�����߂ɕK�v
using System.Collections.Generic;
using TMPro;

public class ItemCategoryManager : MonoBehaviour
{
    // --- �C���X�y�N�^�[�Őݒ肷�鍀�� ---
    [Header("UI References")]
    public GameObject itemDetailPanel;
    public Image itemImage; // --- ������ǉ�: �A�C�e���摜�\���pImage ---
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

    // --- �����g�p ---
    private SaveManager saveManager;
    private string currentSelectedItemName;

    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager��������܂���B�V�[���ɔz�u����Ă��܂����H");
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
        currentSelectedItemName = null; // �I�����ꂽ�A�C�e���������Z�b�g
    }

    // �A�C�e���ڍׂ�\�����郁�\�b�h
    private void DisplayItemDetails(string itemName)
    {
        Dictionary<string, ItemEffect> itemDefinitions = GetItemDefinitionsDictionary(categoryName);
        Dictionary<string, int> itemCounts = GetPlayerItemCountsDictionary(categoryName);

        if (itemDefinitions == null || itemCounts == null || !itemDefinitions.ContainsKey(itemName))
        {
            Debug.LogError($"�A�C�e����񂪌�����܂���: {itemName}");
            itemDetailPanel.SetActive(false);
            return;
        }

        ItemEffect itemDef = itemDefinitions[itemName];
        int ownedCount = itemCounts.GetValueOrDefault(itemName, 0);

        // --- ��������ǉ� ---
        if (itemImage != null)
        {
            itemImage.sprite = itemDef.itemSprite; // �A�C�e����Sprite��ݒ�
            // Sprite���ݒ肳��Ă��Ȃ��ꍇ��Image���\���ɂ���i�C�Ӂj
            itemImage.enabled = itemDef.itemSprite != null;
        }
        // --- �����܂Œǉ� ---

        itemNameText.text = itemName;
        itemDescriptionText.text = itemDef.itemDescription;
        // Debug.Log(itemDef.itemDescription);
        itemEffectText.text = GetEffectDescription(itemDef);

        itemLevelText.text = $"���x��: {itemDef.CurrentLevel} / {itemDef.MaxLevel}";
        itemCostText.text = $"�w���R�X�g: {itemDef.PurchaseCost} ����ۃ~���N";
        itemOwnedCountText.text = $"������: {ownedCount}";

        buyButton.interactable = saveManager.playerData.DickMilk >= itemDef.PurchaseCost;
        useButton.interactable = ownedCount > 0 && itemDef.CurrentLevel < itemDef.MaxLevel;
    }

    // GetEffectDescription, OnBuyButtonClicked, OnUseButtonClicked, GetItemDictionary, GetPlayerItemCountsDictionary
    // �̊e���\�b�h�͕ύX����܂���B
    private string GetEffectDescription(ItemEffect itemDef)
    {
        List<string> effects = new List<string>();
        if (itemDef.BaseEjaculation > 0) effects.Add($"��{�ː��� +{itemDef.BaseEjaculation}");
        if (itemDef.DickPowerEjaculation > 0) effects.Add($"����ۋ����ː��� +{itemDef.DickPowerEjaculation}");
        if (itemDef.BallPowerEjaculation > 0) effects.Add($"���ʋ����ː��� +{itemDef.BallPowerEjaculation}");
        if (itemDef.FacilityMultiplier > 0) effects.Add($"�ݔ������{�� +{itemDef.FacilityMultiplier:F2}");
        if (itemDef.DickLength > 0) effects.Add($"����ے��� +{itemDef.DickLength}cm");
        if (itemDef.BallLevel > 0) effects.Add($"���ʃ��x�� +{itemDef.BallLevel}");
        if (itemDef.SpermSize > 0) effects.Add($"���q�T�C�Y +{itemDef.SpermSize:F2}mm");
        if (itemDef.SemenDensity > 0) effects.Add($"���t�Z�x +{itemDef.SemenDensity}");
        if (itemDef.PleasureAddiction > 0) effects.Add($"���y���œx +{itemDef.PleasureAddiction}");
        if (itemDef.PussyLevel > 0) effects.Add($"���܂񂱃��x�� +{itemDef.PussyLevel}");
        if (itemDef.AnalLevel > 0) effects.Add($"���Ȃ郌�x�� +{itemDef.AnalLevel}");
        if (itemDef.BoobLevel > 0) effects.Add($"�����ς����x�� +{itemDef.BoobLevel}");

        return effects.Count > 0 ? string.Join(", ", effects) : "���ʂȂ�";
    }

    private void OnBuyButtonClicked()
    {
        if (string.IsNullOrEmpty(currentSelectedItemName)) return;

        bool success = saveManager.BuyItem(categoryName, currentSelectedItemName);
        if (success)
        {
            Debug.Log($"{currentSelectedItemName} �̍w���ɐ����I");
            DisplayItemDetails(currentSelectedItemName); // UI���X�V
        }
    }

    private void OnUseButtonClicked()
    {
        if (string.IsNullOrEmpty(currentSelectedItemName)) return;

        bool success = saveManager.UseItem(categoryName, currentSelectedItemName);
        if (success)
        {
            Debug.Log($"{currentSelectedItemName} �̎g�p�ɐ����I");
            DisplayItemDetails(currentSelectedItemName); // UI���X�V
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
                Debug.LogError($"���m�̃A�C�e���J�e�S��: {category}");
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
                Debug.LogError($"���m�̃A�C�e���J�e�S��: {category}");
                return null;
        }
    }
}