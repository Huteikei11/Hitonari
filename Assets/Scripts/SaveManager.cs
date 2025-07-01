using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーの全パラメータとアイテムの所持数を管理するクラスです。
/// </summary>
[Serializable]
public class PlayerData
{
    public int Day = 1; // 現在の日数（1～60）
    public float EjaculationAmount = 3f; // 1日あたりの射精量（ml/L）
    public float BaseEjaculation = 3f; // 基本射精量（初期値3ml）
    public float DickPowerEjaculation = 0f; // ちんぽ強化による射精量増加
    public float BallPowerEjaculation = 0f; // 金玉強化による射精量増加
    public float AddictionEjaculation = 0f; // 射精中毒による射精量増加
    public float FacilityMultiplier = 1.0f; // 設備強化倍率
    public int DickMilk = 0; // 現在のちんぽミルク（L）
    public int TotalDickMilk = 0; // 今まで出したちんぽミルク総量（L）
    public string TodayFacilityUpgrade = ""; // その日行った設備強化（メッセージ用）

    public int DickLength = 10; // ちんぽの長さ（cm）
    public int BallLevel = 0; // 金玉の段階（0:小豆, 1:ビー玉, ...）
    public float SpermSize = 0.06f; // 精子サイズ（mm）
    public int SemenDensity = 1; // 精液濃度（レベル）
    public int PleasureAddiction = 0; // 快楽中毒度（レベル）
    public int PussyLevel = 0; // おまんこレベル
    public int AnalLevel = 0; // あなるレベル
    public int BoobLevel = 0; // おっぱいレベル

    public bool Mosaic = true; // モザイク切り替え
    public bool FranRemilia = false; // フラン・レミリア切り替え

    // 各アイテムの所持数を保存する辞書。購入したらここが増える。
    public Dictionary<string, int> EienteiItemCounts = new Dictionary<string, int>();
    public Dictionary<string, int> WorkshopItemCounts = new Dictionary<string, int>();
    public Dictionary<string, int> LaboratoryItemCounts = new Dictionary<string, int>();

    // コンストラクタで辞書を初期化
    public PlayerData()
    {
        // PlayerDataがインスタンス化される際に辞書も初期化されることを保証
        // 各アイテムの初期化はSaveManagerで行うため、ここでは空の辞書を生成
    }
}



/// <summary>
/// アイテムの定義（効果、コスト、最大レベル）と、プレイヤーがそのアイテムを何回使用したかの状態を管理します。
/// </summary>
[Serializable]
public class ItemEffect
{
    public int PurchaseCost = 0;      // 購入に必要なちんぽミルク
    public int CurrentLevel = 0;      // 現在の効果レベル（使用回数）
    public int MaxLevel = 5;          // 最大効果レベル（最大使用回数）

    // 各パラメータへの効果値（1レベルアップあたりの加算値）
    public float BaseEjaculation = 0f;
    public float DickPowerEjaculation = 0f;
    public float BallPowerEjaculation = 0f;
    public float FacilityMultiplier = 0f;
    public int DickLength = 0;
    public int BallLevel = 0;
    public float SpermSize = 0f;
    public int SemenDensity = 0;
    public int PleasureAddiction = 0;
    public int PussyLevel = 0;
    public int AnalLevel = 0;
    public int BoobLevel = 0;

    public ItemEffect() { } // デフォルトコンストラクタ

    /// <summary>
    /// アイテムの定義データを設定します。
    /// </summary>
    public void Initialize(int maxLevel, int purchaseCost,
                           float baseEjac, float dickPowerEjac, float ballPowerEjac,
                           float facilityMult, int dickLen, int ballLvl,
                           float spermSz, int semenDens, int pleasureAdd,
                           int pussyLvl, int analLvl, int boobLvl)
    {
        MaxLevel = maxLevel;
        PurchaseCost = purchaseCost;
        BaseEjaculation = baseEjac;
        DickPowerEjaculation = dickPowerEjac;
        BallPowerEjaculation = ballPowerEjac;
        FacilityMultiplier = facilityMult;
        DickLength = dickLen;
        BallLevel = ballLvl;
        SpermSize = spermSz;
        SemenDensity = semenDens;
        PleasureAddiction = pleasureAdd;
        PussyLevel = pussyLvl;
        AnalLevel = analLvl;
        BoobLevel = boobLvl;
    }

    /// <summary>
    /// アイテムの効果をPlayerDataに適用します。（1回使用するたびに呼び出される）
    /// </summary>
    /// <param name="playerData">プレイヤーデータ</param>
    /// <returns>効果の適用が成功したかどうか</returns>
    public bool ApplyEffect(PlayerData playerData)
    {
        if (CurrentLevel >= MaxLevel)
        {
            Debug.Log($"このアイテムはこれ以上効果を適用できません（最大レベルに到達）: CurrentLevel={CurrentLevel}, MaxLevel={MaxLevel}");
            return false;
        }

        CurrentLevel++; // 効果を適用したので使用回数を増やす

        // パラメータをPlayerdataに加算
        playerData.BaseEjaculation += BaseEjaculation;
        playerData.DickPowerEjaculation += DickPowerEjaculation;
        playerData.BallPowerEjaculation += BallPowerEjaculation;
        playerData.FacilityMultiplier += FacilityMultiplier;
        playerData.DickLength += DickLength;
        playerData.BallLevel += BallLevel;
        playerData.SpermSize += SpermSize;
        playerData.SemenDensity += SemenDensity;
        playerData.PleasureAddiction += PleasureAddiction;
        playerData.PussyLevel += PussyLevel;
        playerData.AnalLevel += AnalLevel;
        playerData.BoobLevel += BoobLevel;

        Debug.Log($"アイテムの効果が適用されました！現在の効果レベル: {CurrentLevel}");
        return true;
    }
}



/// <summary>
/// セーブ・ロード・計算、およびアイテムの購入・使用を管理するクラスです。
/// </summary>
public class SaveManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData(); // プレイヤーデータ

    // 各カテゴリのアイテムの定義データとその使用レベルを保持
    public Dictionary<string, ItemEffect> EienteiItems = new Dictionary<string, ItemEffect>();
    public Dictionary<string, ItemEffect> WorkshopItems = new Dictionary<string, ItemEffect>();
    public Dictionary<string, ItemEffect> LaboratoryItems = new Dictionary<string, ItemEffect>();

    private string saveKey = "playerData";
    private string eienteiItemsKey = "eienteiItemStates"; // ItemEffectのセーブキー（状態を含む）
    private string workshopItemsKey = "workshopItemStates";
    private string laboratoryItemsKey = "laboratoryItemStates";

    void Awake()
    {
        InitializeItemDefinitions(); // アイテムの定義データを初期化
        Load(); // セーブデータをロード
    }

    /// <summary>
    /// 各アイテムの定義データ（購入コスト、効果値など）を設定します。
    /// ゲーム開始時に一度だけ呼び出され、ロードデータで上書きされます。
    /// </summary>
    private void InitializeItemDefinitions()
    {
        // 永遠亭アイテムの定義
        EienteiItems["フタナリン"] = new ItemEffect();
        EienteiItems["フタナリン"].Initialize(maxLevel: 5, purchaseCost: 100, baseEjac: 0.5f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 1, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["フタナール"] = new ItemEffect();
        EienteiItems["フタナール"].Initialize(maxLevel: 5, purchaseCost: 200, baseEjac: 1.0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 2, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["でかたまん"] = new ItemEffect();
        EienteiItems["でかたまん"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0.8f, facilityMult: 0f, dickLen: 0, ballLvl: 1, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["きょたまりん"] = new ItemEffect();
        EienteiItems["きょたまりん"].Initialize(maxLevel: 5, purchaseCost: 250, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 1.5f, facilityMult: 0f, dickLen: 0, ballLvl: 2, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["ごってりん"] = new ItemEffect();
        EienteiItems["ごってりん"].Initialize(maxLevel: 5, purchaseCost: 300, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0.01f, semenDens: 1, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["さずかーる"] = new ItemEffect();
        EienteiItems["さずかーる"].Initialize(maxLevel: 5, purchaseCost: 400, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0.02f, semenDens: 2, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["媚薬"] = new ItemEffect();
        EienteiItems["媚薬"].Initialize(maxLevel: 5, purchaseCost: 50, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 5, pussyLvl: 0, analLvl: 0, boobLvl: 0);


        // 工房アイテムの定義
        WorkshopItems["オナホール"] = new ItemEffect();
        WorkshopItems["オナホール"].Initialize(maxLevel: 5, purchaseCost: 80, baseEjac: 0f, dickPowerEjac: 0.3f, ballPowerEjac: 0f, facilityMult: 0.02f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["電動オナホール"] = new ItemEffect();
        WorkshopItems["電動オナホール"].Initialize(maxLevel: 5, purchaseCost: 180, baseEjac: 0f, dickPowerEjac: 0.8f, ballPowerEjac: 0f, facilityMult: 0.05f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["かいら君"] = new ItemEffect();
        WorkshopItems["かいら君"].Initialize(maxLevel: 5, purchaseCost: 120, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 10, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["かいら君EX"] = new ItemEffect();
        WorkshopItems["かいら君EX"].Initialize(maxLevel: 5, purchaseCost: 220, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 20, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["絞りトール"] = new ItemEffect();
        WorkshopItems["絞りトール"].Initialize(maxLevel: 5, purchaseCost: 250, baseEjac: 0f, dickPowerEjac: 1.0f, ballPowerEjac: 1.0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["特殊迷彩グッズ「ケーホウ175」"] = new ItemEffect();
        WorkshopItems["特殊迷彩グッズ「ケーホウ175」"].Initialize(maxLevel: 1, purchaseCost: 500, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["特殊迷彩グッズ「ふらんちゃん」"] = new ItemEffect();
        WorkshopItems["特殊迷彩グッズ「ふらんちゃん」"].Initialize(maxLevel: 1, purchaseCost: 500, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        // 研究室アイテムの定義
        LaboratoryItems["おまんこ触手"] = new ItemEffect();
        LaboratoryItems["おまんこ触手"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 1, analLvl: 0, boobLvl: 0);

        LaboratoryItems["あなる触手"] = new ItemEffect();
        LaboratoryItems["あなる触手"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 1, boobLvl: 0);

        LaboratoryItems["おっぱい触手"] = new ItemEffect();
        LaboratoryItems["おっぱい触手"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 1);

        LaboratoryItems["媚薬触手"] = new ItemEffect();
        LaboratoryItems["媚薬触手"].Initialize(maxLevel: 5, purchaseCost: 200, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 15, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        LaboratoryItems["ちんぽに餌やり"] = new ItemEffect();
        LaboratoryItems["ちんぽに餌やり"].Initialize(maxLevel: 5, purchaseCost: 300, baseEjac: 0f, dickPowerEjac: 0.5f, ballPowerEjac: 0.5f, facilityMult: 0f, dickLen: 1, ballLvl: 1, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        // PlayerData内のアイテムカウント辞書に各アイテムのエントリを追加
        // ロード時に既存データがあればそれが優先されるため、ここでは初回起動時の初期化のみ
        InitializePlayerItemCounts();
    }

    /// <summary>
    /// PlayerData内のアイテムカウント辞書にすべてのアイテム名のエントリがあることを保証します。
    /// 主に新規ゲーム開始時や、ゲームアップデートで新しいアイテムが追加された場合に利用。
    /// </summary>
    private void InitializePlayerItemCounts()
    {
        foreach (var pair in EienteiItems)
        {
            if (!playerData.EienteiItemCounts.ContainsKey(pair.Key))
                playerData.EienteiItemCounts[pair.Key] = 0;
        }
        foreach (var pair in WorkshopItems)
        {
            if (!playerData.WorkshopItemCounts.ContainsKey(pair.Key))
                playerData.WorkshopItemCounts[pair.Key] = 0;
        }
        foreach (var pair in LaboratoryItems)
        {
            if (!playerData.LaboratoryItemCounts.ContainsKey(pair.Key))
                playerData.LaboratoryItemCounts[pair.Key] = 0;
        }
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
            // PlayerDataのコンストラクタで辞書が初期化され、InitializeItemDefinitionsでカウントエントリが追加される
        }

        // ItemEffectの状態（CurrentLevel）をロード
        // InitializeItemDefinitionsで設定された初期値は、ロードされたデータで上書きされます
        if (ES3.KeyExists(eienteiItemsKey))
            EienteiItems = ES3.Load<Dictionary<string, ItemEffect>>(eienteiItemsKey);
        if (ES3.KeyExists(workshopItemsKey))
            WorkshopItems = ES3.Load<Dictionary<string, ItemEffect>>(workshopItemsKey);
        if (ES3.KeyExists(laboratoryItemsKey))
            LaboratoryItems = ES3.Load<Dictionary<string, ItemEffect>>(laboratoryItemsKey);
        Debug.Log("ItemEffect States ロード完了");

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
    /// ちんぽミルクを消費し、プレイヤーのアイテム所持数を増やします。
    /// </summary>
    /// <param name="category">アイテムのカテゴリ (e.g., "Eientei", "Workshop", "Laboratory")</param>
    /// <param name="itemName">アイテムの名前</param>
    /// <returns>購入が成功したかどうか</returns>
    public bool BuyItem(string category, string itemName)
    {
        Dictionary<string, ItemEffect> itemDefinitions = GetItemDictionary(category);
        Dictionary<string, int> itemCounts = GetPlayerItemCountsDictionary(category);

        if (itemDefinitions == null || itemCounts == null)
            return false; // エラーはGetItemDictionary/GetPlayerItemCountsDictionaryでログ出力済み

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

        playerData.DickMilk -= itemDef.PurchaseCost; // コストを消費
        itemCounts[itemName] = itemCounts.GetValueOrDefault(itemName, 0) + 1; // 所持数を増やす

        Debug.Log($"{itemName} を購入しました！ 所持数: {itemCounts[itemName]}, 残りちんぽミルク: {playerData.DickMilk}");
        Save(); // 購入後、セーブ
        return true;
    }

    /// <summary>
    /// 指定されたアイテムを使用します。
    /// アイテムの所持数を減らし、その効果をプレイヤーデータに適用します。
    /// </summary>
    /// <param name="category">アイテムのカテゴリ</param>
    /// <param name="itemName">アイテムの名前</param>
    /// <returns>使用が成功したかどうか</returns>
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

        // 最大レベルに達しているかどうかのチェックはItemEffect.ApplyEffectに任せる
        // UseItemが成功してもApplyEffectが失敗する可能性があるため、その場合は在庫を戻す
        itemCounts[itemName]--; // 所持数を減らす

        bool effectApplied = itemDef.ApplyEffect(playerData); // 効果を適用
        if (!effectApplied)
        {
            Debug.LogWarning($"{itemName} の効果適用に失敗しましたが、使用はされました。所持数を戻します。");
            itemCounts[itemName]++; // 効果適用失敗なので所持数を戻す
            Save(); // 失敗時も状態が変わる可能性があるのでセーブ
            return false;
        }

        Debug.Log($"{itemName} を使用しました！ 残り所持数: {itemCounts[itemName]}, 現在の効果レベル: {itemDef.CurrentLevel}");
        Save(); // 使用後、セーブ
        return true;
    }

    /// <summary>
    /// カテゴリ名に基づいてItemEffect辞書を取得します。
    /// </summary>
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

    /// <summary>
    /// カテゴリ名に基づいてPlayerData内のアイテムカウント辞書を取得します。
    /// </summary>
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

    /// <summary>
    /// アイテムの購入と使用の例です。（UIからの呼び出しなどを想定）
    /// </summary>
    public void ExampleItemUsage()
    {
        Debug.Log("--- アイテム購入・使用テスト開始 ---");

        // テスト用にちんぽミルクを増やす
        playerData.DickMilk = 1000;
        Debug.Log($"初期ちんぽミルク: {playerData.DickMilk}");
        Debug.Log($"フタナリン初期所持数: {playerData.EienteiItemCounts.GetValueOrDefault("フタナリン", 0)}");
        Debug.Log($"フタナリン初期効果レベル: {EienteiItems["フタナリン"].CurrentLevel}");
        Debug.Log($"初期ちんぽ長さ: {playerData.DickLength}");

        // 1. フタナリンを2個購入
        Debug.Log("--- フタナリンを2個購入 ---");
        BuyItem("Eientei", "フタナリン");
        BuyItem("Eientei", "フタナリン");
        Debug.Log($"フタナリン購入後所持数: {playerData.EienteiItemCounts.GetValueOrDefault("フタナリン", 0)}");
        Debug.Log($"残りちんぽミルク: {playerData.DickMilk}");

        // 2. フタナリンを1個使用
        Debug.Log("--- フタナリンを1個使用 ---");
        if (UseItem("Eientei", "フタナリン"))
        {
            Debug.Log("フタナリンの使用に成功！");
        }
        else
        {
            Debug.Log("フタナリンの使用に失敗。");
        }

        Debug.Log($"フタナリン使用後所持数: {playerData.EienteiItemCounts.GetValueOrDefault("フタナリン", 0)}");
        Debug.Log($"フタナリン使用後効果レベル: {EienteiItems["フタナリン"].CurrentLevel}");
        Debug.Log($"使用後ちんぽ長さ: {playerData.DickLength}");

        // 3. 最大レベルまでフタナリンを使用する例 (足りない分は購入から)
        playerData.DickMilk += 5000; // たっぷりミルクを追加
        Debug.Log("--- フタナリンを最大レベルまで使用（必要に応じて購入） ---");
        while (EienteiItems["フタナリン"].CurrentLevel < EienteiItems["フタナリン"].MaxLevel)
        {
            if (playerData.EienteiItemCounts.GetValueOrDefault("フタナリン", 0) <= 0)
            {
                Debug.Log("フタナリンの在庫がないので購入を試みます。");
                if (!BuyItem("Eientei", "フタナリン"))
                {
                    Debug.Log("フタナリンを購入できませんでした。テスト終了。");
                    break;
                }
            }
            if (UseItem("Eientei", "フタナリン"))
            {
                Debug.Log($"フタナリン使用成功！ 効果レベル: {EienteiItems["フタナリン"].CurrentLevel}, 残り所持数: {playerData.EienteiItemCounts.GetValueOrDefault("フタナリン", 0)}");
            }
            else
            {
                Debug.Log("フタナリンの使用に失敗しました。テスト終了。");
                break;
            }
        }
        Debug.Log($"最終フタナリン効果レベル: {EienteiItems["フタナリン"].CurrentLevel}, 最終ちんぽ長さ: {playerData.DickLength}");

        Debug.Log("--- アイテム購入・使用テスト終了 ---");
    }
}