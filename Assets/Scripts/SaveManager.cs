using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セーブ・ロード・計算、およびアイテムの購入・使用を管理するクラスです。
/// </summary>
public class SaveManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();

    public Dictionary<string, ItemEffect> EienteiItems = new Dictionary<string, ItemEffect>();
    public Dictionary<string, ItemEffect> WorkshopItems = new Dictionary<string, ItemEffect>();
    public Dictionary<string, ItemEffect> LaboratoryItems = new Dictionary<string, ItemEffect>();

    private string saveKey = "playerData";
    private string eienteiItemsKey = "eienteiItemStates";
    private string workshopItemsKey = "workshopItemStates";
    private string laboratoryItemsKey = "laboratoryItemStates";

    [Header("References")]
    public ItemDefinitions itemDefinitions; // アイテム定義管理クラスへの参照
    public FacilityUpgradeDisplay facilityUpgradeDisplay; // 施設アップグレード表示UI

    void Awake()
    {
        InitializeItemDefinitions();
        // 最初に読み込むのを阻止
        // Load();
    }

    /// <summary>
    /// 各アイテムの定義データを設定します
    /// </summary>
    private void InitializeItemDefinitions()
    {
        if (itemDefinitions == null)
        {
            itemDefinitions = FindObjectOfType<ItemDefinitions>();
            if (itemDefinitions == null)
            {
                Debug.LogError("ItemDefinitionsコンポーネントが見つかりません！");
                return;
            }
        }

        // アイテム定義を初期化
        itemDefinitions.InitializeEienteiItems(EienteiItems);
        itemDefinitions.InitializeWorkshopItems(WorkshopItems);
        itemDefinitions.InitializeLaboratoryItems(LaboratoryItems);

        InitializePlayerItemCounts();
    }

    /// <summary>
    /// データをセーブします。
    /// PlayerDataと各アイテムの現在の状態（CurrentLevel）を保存します。
    /// </summary>
    public void Save()
    {
        ES3.Save(saveKey, playerData);
        ES3.Save(eienteiItemsKey, EienteiItems);
        ES3.Save(workshopItemsKey, WorkshopItems);
        ES3.Save(laboratoryItemsKey, LaboratoryItems);
        Debug.Log("セーブ完了");
    }

    /// <summary>
    /// データをロードします。セーブデータが存在しない場合は新規データを作成します。
    /// </summary>
    public void Load()
    {
        if (ES3.KeyExists(saveKey))
        {
            playerData = ES3.Load<PlayerData>(saveKey);
            Debug.Log("PlayerData ロード完了");
        }
        else
        {
            Debug.Log("PlayerData がありません。新規データを作成します。");
        }

        // ItemEffectの状態（CurrentLevel）をロード
        if (ES3.KeyExists(eienteiItemsKey))
            EienteiItems = ES3.Load<Dictionary<string, ItemEffect>>(eienteiItemsKey);
        if (ES3.KeyExists(workshopItemsKey))
            WorkshopItems = ES3.Load<Dictionary<string, ItemEffect>>(workshopItemsKey);
        if (ES3.KeyExists(laboratoryItemsKey))
            LaboratoryItems = ES3.Load<Dictionary<string, ItemEffect>>(laboratoryItemsKey);
        Debug.Log("ItemEffect States ロード完了");

        // ロード後にSpriteを再割り当て
        if (itemDefinitions != null)
        {
            itemDefinitions.RestoreItemSprites(EienteiItems, WorkshopItems, LaboratoryItems);
        }

        // ロード後に、新しいアイテムが追加されていた場合の対応
        InitializePlayerItemCounts();
    }

    /// <summary>
    /// 1日あたりの射精量を計算します。
    /// </summary>
    public void CalculateDailyEjaculation()
    {
        float randomRate = UnityEngine.Random.Range(0.95f, 1.05f);
        playerData.EjaculationAmount =
            (playerData.BaseEjaculation +
             playerData.DickPowerEjaculation +
             playerData.BallPowerEjaculation +
             playerData.AddictionEjaculation)
            * playerData.FacilityMultiplier
            * randomRate;
    }

    /// <summary>
    /// 指定されたアイテムを購入します。
    /// </summary>
    public bool BuyItem(string category, string itemName)
    {
        Dictionary<string, ItemEffect> itemDefinitions = GetItemDictionary(category);
        Dictionary<string, int> itemCounts = GetPlayerItemCountsDictionary(category);

        if (itemDefinitions == null || itemCounts == null)
            return false;

        if (!itemDefinitions.TryGetValue(itemName, out ItemEffect itemDef))
        {
            Debug.LogError($"アイテム定義が見つかりません: カテゴリ={category}, 名前={itemName}");
            return false;
        }

        if (playerData.DickMilk < itemDef.PurchaseCost)
        {
            Debug.Log($"ちんぽミルクが足りません。必要量: {itemDef.PurchaseCost}, 現在量: {playerData.DickMilk}");
            return false;
        }

        playerData.DickMilk -= itemDef.PurchaseCost;
        itemCounts[itemName] = itemCounts.GetValueOrDefault(itemName, 0) + 1;

        Debug.Log($"{itemName} を購入しました！ 所持数: {itemCounts[itemName]}, 残りちんぽミルク: {playerData.DickMilk}");
        Save();
        return true;
    }

    /// <summary>
    /// 指定されたアイテムを使用します。
    /// </summary>
    public bool UseItem(string category, string itemName)
    {
        Dictionary<string, ItemEffect> itemDefinitions = GetItemDictionary(category);
        Dictionary<string, int> itemCounts = GetPlayerItemCountsDictionary(category);

        if (itemDefinitions == null || itemCounts == null)
            return false;

        if (!itemDefinitions.TryGetValue(itemName, out ItemEffect itemDef))
        {
            Debug.LogError($"アイテム定義が見つかりません: カテゴリ={category}, 名前={itemName}");
            return false;
        }

        if (itemCounts.GetValueOrDefault(itemName, 0) <= 0)
        {
            Debug.Log($"{itemName} の所持数がありません。現在: {itemCounts.GetValueOrDefault(itemName, 0)}");
            return false;
        }

        itemCounts[itemName]--;
        bool effectApplied = itemDef.ApplyEffect(playerData);
        
        if (!effectApplied)
        {
            Debug.LogWarning($"{itemName} の効果適用に失敗しましたが、使用はされました。所持数を戻します。");
            itemCounts[itemName]++;
            Save();
            return false;
        }

        Debug.Log($"{itemName} を使用しました！ 残り所持数: {itemCounts[itemName]}, 現在の効果レベル: {itemDef.CurrentLevel}");
        Save();
        return true;
    }

    /// <summary>
    /// 一日を終了し、プレイヤーデータを更新します。
    /// </summary>
    public void EndDay()
    {
        Debug.Log("--- 一日終了処理開始 ---");
        
        string todayUpgrade = playerData.TodayFacilityUpgrade;
        
        // 射精量を計算
        CalculateDailyEjaculation();
        
        // 快楽中毒による追加射精量を計算
        playerData.AddictionEjaculation = playerData.PleasureAddiction * 0.1f;
        
        // 一日分のちんぽミルクを獲得
        int gainedMilk = Mathf.RoundToInt(playerData.EjaculationAmount);
        playerData.DickMilk += gainedMilk;
        playerData.TotalDickMilk += gainedMilk;
        
        // 日数を進める
        playerData.Day++;
        playerData.TodayFacilityUpgrade = "";
        
        Debug.Log($"一日終了: Day {playerData.Day - 1} → Day {playerData.Day}");
        Debug.Log($"射精量: {playerData.EjaculationAmount:F2}");
        Debug.Log($"獲得ちんぽミルク: {gainedMilk}");
        Debug.Log($"現在のちんぽミルク: {playerData.DickMilk}");
        Debug.Log($"累計ちんぽミルク: {playerData.TotalDickMilk}");
        
        // 一日の統計情報を表示
        DisplayDailyStats();
        
        // セーブ
        Save();
        
        // 30日目に達した場合はエンディングに移行
        if (playerData.Day > 30)
        {
            Debug.Log("30日目に到達しました。エンディングに移行します。");
            TriggerGameEnding();
            return;
        }
        
        // 施設アップグレード情報を表示
        ShowFacilityUpgradeInfo(todayUpgrade);
        
        Debug.Log("--- 一日終了処理完了 ---");
    }

    /// <summary>
    /// 一日の終了時にプレイヤーステータスの統計情報を表示します
    /// </summary>
    private void DisplayDailyStats()
    {
        Debug.Log("=== 本日の統計情報 ===");
        Debug.Log($"経過日数: {playerData.Day}日目");
        Debug.Log($"本日の射精量: {playerData.EjaculationAmount:F2}");
        Debug.Log($"現在のちんぽミルク: {playerData.DickMilk}");
        Debug.Log($"累計ちんぽミルク: {playerData.TotalDickMilk}");
        
        Debug.Log("--- 現在のステータス ---");
        Debug.Log($"基本射精量: {playerData.BaseEjaculation:F2}");
        Debug.Log($"ちんぽパワー射精量: {playerData.DickPowerEjaculation:F2}");
        Debug.Log($"金玉パワー射精量: {playerData.BallPowerEjaculation:F2}");
        Debug.Log($"快楽中毒射精量: {playerData.AddictionEjaculation:F2}");
        Debug.Log($"施設倍率: {playerData.FacilityMultiplier:F2}");
        
        Debug.Log("--- 身体パラメータ ---");
        Debug.Log($"ちんぽの長さ: {playerData.DickLength}cm");
        Debug.Log($"金玉レベル: {playerData.BallLevel}");
        Debug.Log($"精子サイズ: {playerData.SpermSize:F3}");
        Debug.Log($"精液密度: {playerData.SemenDensity}");
        Debug.Log($"快楽中毒度: {playerData.PleasureAddiction}");
        
        Debug.Log("--- 性感帯レベル ---");
        Debug.Log($"おまんこレベル: {playerData.PussyLevel}");
        Debug.Log($"アナルレベル: {playerData.AnalLevel}");
        Debug.Log($"おっぱいレベル: {playerData.BoobLevel}");
        
        Debug.Log("--- その他設定 ---");
        Debug.Log($"モザイク設定: {(playerData.Mosaic ? "ON" : "OFF")}");
        Debug.Log($"フランレミリア: {(playerData.FranRemilia ? "ON" : "OFF")}");
        
        DisplayItemStats();
        Debug.Log("=== 統計情報終了 ===");
    }

    /// <summary>
    /// アイテム所持数の統計情報を表示します
    /// </summary>
    private void DisplayItemStats()
    {
        Debug.Log("--- アイテム所持状況 ---");
        
        // 永遠亭アイテム
        Debug.Log("【永遠亭アイテム】");
        int eienteiTotal = 0;
        foreach (var item in playerData.EienteiItemCounts)
        {
            if (item.Value > 0)
            {
                Debug.Log($"  {item.Key}: {item.Value}個");
                eienteiTotal += item.Value;
            }
        }
        Debug.Log($"  永遠亭アイテム合計: {eienteiTotal}個");
        
        // 工房アイテム
        Debug.Log("【工房アイテム】");
        int workshopTotal = 0;
        foreach (var item in playerData.WorkshopItemCounts)
        {
            if (item.Value > 0)
            {
                Debug.Log($"  {item.Key}: {item.Value}個");
                workshopTotal += item.Value;
            }
        }
        Debug.Log($"  工房アイテム合計: {workshopTotal}個");
        
        // 研究室アイテム
        Debug.Log("【研究室アイテム】");
        int laboratoryTotal = 0;
        foreach (var item in playerData.LaboratoryItemCounts)
        {
            if (item.Value > 0)
            {
                Debug.Log($"  {item.Key}: {item.Value}個");
                laboratoryTotal += item.Value;
            }
        }
        Debug.Log($"  研究室アイテム合計: {laboratoryTotal}個");
        
        int allItemsTotal = eienteiTotal + workshopTotal + laboratoryTotal;
        Debug.Log($"全アイテム合計: {allItemsTotal}個");
        
        DisplayItemEffectLevels();
    }

    /// <summary>
    /// アイテム効果レベルの統計情報を表示します
    /// </summary>
    private void DisplayItemEffectLevels()
    {
        Debug.Log("--- アイテム効果レベル ---");
        
        // 永遠亭アイテムの効果レベル
        Debug.Log("【永遠亭アイテム効果】");
        foreach (var item in EienteiItems)
        {
            if (item.Value.CurrentLevel > 0)
            {
                Debug.Log($"  {item.Key}: Lv.{item.Value.CurrentLevel}/{item.Value.MaxLevel}");
            }
        }
        
        // 工房アイテムの効果レベル
        Debug.Log("【工房アイテム効果】");
        foreach (var item in WorkshopItems)
        {
            if (item.Value.CurrentLevel > 0)
            {
                Debug.Log($"  {item.Key}: Lv.{item.Value.CurrentLevel}/{item.Value.MaxLevel}");
            }
        }
        
        // 研究室アイテムの効果レベル
        Debug.Log("【研究室アイテム効果】");
        foreach (var item in LaboratoryItems)
        {
            if (item.Value.CurrentLevel > 0)
            {
                Debug.Log($"  {item.Key}: Lv.{item.Value.CurrentLevel}/{item.Value.MaxLevel}");
            }
        }
    }

    /// <summary>
    /// 施設アップグレード情報を表示します
    /// </summary>
    private void ShowFacilityUpgradeInfo(string upgradeInfo)
    {
        if (facilityUpgradeDisplay == null)
            facilityUpgradeDisplay = FindObjectOfType<FacilityUpgradeDisplay>();

        if (facilityUpgradeDisplay != null)
        {
            // プレイヤー情報を生成
            string playerInfo = FacilityUpgradeDisplay.GeneratePlayerInfoText(playerData);
            
            // 施設アップグレード情報とプレイヤー情報を両方表示
            facilityUpgradeDisplay.ShowFacilityUpgradeWithPlayerInfo(upgradeInfo, playerInfo, OnFacilityUpgradeDisplayComplete);
        }
        else
        {
            Debug.LogWarning("FacilityUpgradeDisplayが見つかりません。一日終了処理を続行します。");
            OnFacilityUpgradeDisplayComplete();
        }
    }

    /// <summary>
    /// 施設アップグレード表示完了時のコールバック
    /// </summary>
    private void OnFacilityUpgradeDisplayComplete()
    {
        Debug.Log("施設アップグレード表示完了。次の日に進みます。");
    }

    /// <summary>
    /// 施設アップグレード情報を設定します
    /// </summary>
    public void SetTodayFacilityUpgrade(string upgradeInfo)
    {
        playerData.TodayFacilityUpgrade = upgradeInfo;
        Debug.Log($"今日の施設アップグレードを設定: {upgradeInfo}");
    }

    /// <summary>
    /// ゲームエンディングに移行します
    /// </summary>
    private void TriggerGameEnding()
    {
        Debug.Log("=== ゲームエンディング開始 ===");
        DisplayEndingStats();
        Debug.Log("エンディング処理完了（仮実装）");
    }

    /// <summary>
    /// エンディング時の統計情報を表示します
    /// </summary>
    private void DisplayEndingStats()
    {
        Debug.Log("=== 最終統計 ===");
        Debug.Log($"総プレイ日数: 30日");
        Debug.Log($"最終ちんぽミルク: {playerData.DickMilk}");
        Debug.Log($"累計ちんぽミルク: {playerData.TotalDickMilk}");
        Debug.Log($"最終射精量: {playerData.EjaculationAmount:F2}");
        Debug.Log($"ちんぽの長さ: {playerData.DickLength}cm");
        Debug.Log($"金玉レベル: {playerData.BallLevel}");
        Debug.Log($"快楽中毒度: {playerData.PleasureAddiction}");
        Debug.Log($"おまんこレベル: {playerData.PussyLevel}");
        Debug.Log($"アナルレベル: {playerData.AnalLevel}");
        Debug.Log($"おっぱいレベル: {playerData.BoobLevel}");
        
        string endingType = DetermineEndingType();
        Debug.Log($"エンディングタイプ: {endingType}");
    }

    /// <summary>
    /// エンディングタイプを決定します
    /// </summary>
    private string DetermineEndingType()
    {
        if (playerData.TotalDickMilk >= 50000)
            return "True End - ちんぽミルク王";
        else if (playerData.DickLength >= 50)
            return "Good End - 巨根マスター";
        else if (playerData.PleasureAddiction >= 100)
            return "Bad End - 快楽の虜";
        else if (playerData.PussyLevel >= 10 && playerData.AnalLevel >= 10 && playerData.BoobLevel >= 10)
            return "Harem End - 全穴制覇";
        else
            return "Normal End - 平凡な結末";
    }

    /// <summary>
    /// ゲームをリセットしてタイトルに戻ります
    /// </summary>
    public void ReturnToTitle()
    {
        Debug.Log("タイトルに戻ります...");
        playerData = new PlayerData();
        InitializeItemDefinitions();
        Debug.Log("ゲームがリセットされました。");
    }

    // ヘルパーメソッド群
    private Dictionary<string, ItemEffect> GetItemDictionary(string category)
    {
        switch (category)
        {
            case "Eientei": return EienteiItems;
            case "Workshop": return WorkshopItems;
            case "Laboratory": return LaboratoryItems;
            default:
                Debug.LogError($"未知のアイテムカテゴリ: {category}");
                return null;
        }
    }

    private Dictionary<string, int> GetPlayerItemCountsDictionary(string category)
    {
        switch (category)
        {
            case "Eientei": return playerData.EienteiItemCounts;
            case "Workshop": return playerData.WorkshopItemCounts;
            case "Laboratory": return playerData.LaboratoryItemCounts;
            default:
                Debug.LogError($"未知のアイテムカテゴリ: {category}");
                return null;
        }
    }

    private void InitializePlayerItemCounts()
    {
        foreach (var pair in EienteiItems)
            if (!playerData.EienteiItemCounts.ContainsKey(pair.Key))
                playerData.EienteiItemCounts[pair.Key] = 0;

        foreach (var pair in WorkshopItems)
            if (!playerData.WorkshopItemCounts.ContainsKey(pair.Key))
                playerData.WorkshopItemCounts[pair.Key] = 0;

        foreach (var pair in LaboratoryItems)
            if (!playerData.LaboratoryItemCounts.ContainsKey(pair.Key))
                playerData.LaboratoryItemCounts[pair.Key] = 0;
    }
}