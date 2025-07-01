using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの全パラメータを管理するクラスです。
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
}


/// <summary>
/// アイテムの効果・状態を管理するクラスです。
/// </summary>
[Serializable]
public class ItemEffect
{
    public int CurrentLevel = 0;          // 現在のレベル
    public int MaxLevel = 5;              // 最大レベル
    public int RequiredDickMilk = 0;      // レベルアップに必要なちんぽミルク

    // 各パラメータへの効果値
    public float BaseEjaculation = 0f;      // 基本射精量への加算
    public float DickPowerEjaculation = 0f; // ちんぽ強化射精量への加算
    public float BallPowerEjaculation = 0f; // たまたま強化射精量への加算
    public float FacilityMultiplier = 0f;   // 設備強化倍率への加算
    public int DickLength = 0;              // ちんぽ長さへの加算
    public int BallLevel = 0;               // 金玉レベルへの加算
    public float SpermSize = 0f;            // 精子サイズへの加算
    public int SemenDensity = 0;            // 精液濃度への加算
    public int PleasureAddiction = 0;       // 快楽中毒度への加算
    public int PussyLevel = 0;              // おまんこレベルへの加算
    public int AnalLevel = 0;               // あなるレベルへの加算
    public int BoobLevel = 0;               // おっぱいレベルへの加算

    // デフォルトコンストラクタ（初期化用）
    public ItemEffect() { }

    /// <summary>
    /// アイテムの初期値を設定します。
    /// </summary>
    public void Initialize(int maxLevel, int initialRequiredMilk,
                           float baseEjac, float dickPowerEjac, float ballPowerEjac,
                           float facilityMult, int dickLen, int ballLvl,
                           float spermSz, int semenDens, int pleasureAdd,
                           int pussyLvl, int analLvl, int boobLvl)
    {
        MaxLevel = maxLevel;
        RequiredDickMilk = initialRequiredMilk;
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
    /// アイテムをレベルアップさせ、PlayerDataに効果を適用します。
    /// このメソッドはSaveManagerから呼び出されることを想定しています。
    /// </summary>
    /// <param name="playerData">プレイヤーデータ</param>
    /// <returns>レベルアップが成功したかどうか</returns>
    public bool LevelUp(PlayerData playerData)
    {
        if (CurrentLevel >= MaxLevel)
        {
            Debug.Log($"アイテムは最大レベルです: CurrentLevel={CurrentLevel}, MaxLevel={MaxLevel}");
            return false;
        }

        if (playerData.DickMilk < RequiredDickMilk)
        {
            Debug.Log($"ちんぽミルクが足りません。必要量: {RequiredDickMilk}, 現在量: {playerData.DickMilk}");
            return false;
        }

        // コストを消費
        playerData.DickMilk -= RequiredDickMilk;
        CurrentLevel++;

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

        // 次のレベルアップに必要なちんぽミルクを増やす（例: 現在のレベルに応じて増加）
        // これはゲームのバランスによって調整してください。
        RequiredDickMilk = (int)(RequiredDickMilk * 1.5f) + 10; // 例: 1.5倍にして10L追加

        Debug.Log($"アイテムがレベルアップしました！現在のレベル: {CurrentLevel}");
        return true;
    }
}

/// <summary>
/// セーブ・ロード・計算を管理するクラスです。
/// </summary>
public class SaveManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData(); // プレイヤーデータ

    // 永遠亭アイテム
    public Dictionary<string, ItemEffect> EienteiItems = new Dictionary<string, ItemEffect>();
    // 工房アイテム
    public Dictionary<string, ItemEffect> WorkshopItems = new Dictionary<string, ItemEffect>();
    // 研究室アイテム
    public Dictionary<string, ItemEffect> LaboratoryItems = new Dictionary<string, ItemEffect>();

    private string saveKey = "playerData"; // セーブデータのキー
    private string eienteiKey = "eienteiItems";
    private string workshopKey = "workshopItems";
    private string laboratoryKey = "laboratoryItems";

    void Awake()
    {
        InitializeItemData();
        Load(); // シーン開始時にロードを試みる
    }

    /// <summary>
    /// 各アイテムの初期データを設定します。
    /// </summary>
    private void InitializeItemData()
    {
        // 永遠亭アイテムの初期化
        EienteiItems["フタナリン"] = new ItemEffect();
        EienteiItems["フタナリン"].Initialize(maxLevel: 5, initialRequiredMilk: 100,
                                            baseEjac: 0.5f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 1, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["フタナール"] = new ItemEffect();
        EienteiItems["フタナール"].Initialize(maxLevel: 5, initialRequiredMilk: 200,
                                            baseEjac: 1.0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 2, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["でかたまん"] = new ItemEffect();
        EienteiItems["でかたまん"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0.8f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 1,
                                        spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["きょたまりん"] = new ItemEffect();
        EienteiItems["きょたまりん"].Initialize(maxLevel: 5, initialRequiredMilk: 250,
                                            baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 1.5f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 2,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["ごってりん"] = new ItemEffect();
        EienteiItems["ごってりん"].Initialize(maxLevel: 5, initialRequiredMilk: 300,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                        spermSz: 0.01f, semenDens: 1, pleasureAdd: 0,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["さずかーる"] = new ItemEffect();
        EienteiItems["さずかーる"].Initialize(maxLevel: 5, initialRequiredMilk: 400,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                        spermSz: 0.02f, semenDens: 2, pleasureAdd: 0,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["媚薬"] = new ItemEffect();
        EienteiItems["媚薬"].Initialize(maxLevel: 5, initialRequiredMilk: 50,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                        spermSz: 0f, semenDens: 0, pleasureAdd: 5,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);


        // 工房アイテムの初期化
        WorkshopItems["オナホール"] = new ItemEffect();
        WorkshopItems["オナホール"].Initialize(maxLevel: 5, initialRequiredMilk: 80,
                                            baseEjac: 0f, dickPowerEjac: 0.3f, ballPowerEjac: 0f,
                                            facilityMult: 0.02f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["電動オナホール"] = new ItemEffect();
        WorkshopItems["電動オナホール"].Initialize(maxLevel: 5, initialRequiredMilk: 180,
                                                baseEjac: 0f, dickPowerEjac: 0.8f, ballPowerEjac: 0f,
                                                facilityMult: 0.05f, dickLen: 0, ballLvl: 0,
                                                spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["かいら君"] = new ItemEffect();
        WorkshopItems["かいら君"].Initialize(maxLevel: 5, initialRequiredMilk: 120,
                                            baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 10,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["かいら君EX"] = new ItemEffect();
        WorkshopItems["かいら君EX"].Initialize(maxLevel: 5, initialRequiredMilk: 220,
                                            baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 20,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["絞りトール"] = new ItemEffect();
        WorkshopItems["絞りトール"].Initialize(maxLevel: 5, initialRequiredMilk: 250,
                                            baseEjac: 0f, dickPowerEjac: 1.0f, ballPowerEjac: 1.0f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["特殊迷彩グッズ「ケーホウ175」"] = new ItemEffect();
        WorkshopItems["特殊迷彩グッズ「ケーホウ175」"].Initialize(maxLevel: 1, initialRequiredMilk: 500,
                                                                baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                                facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                                spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                                pussyLvl: 0, analLvl: 0, boobLvl: 0); // フラン・レミリア切り替え用、効果はなし

        WorkshopItems["特殊迷彩グッズ「ふらんちゃん」"] = new ItemEffect();
        WorkshopItems["特殊迷彩グッズ「ふらんちゃん」"].Initialize(maxLevel: 1, initialRequiredMilk: 500,
                                                                baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                                facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                                spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                                pussyLvl: 0, analLvl: 0, boobLvl: 0); // フラン・レミリア切り替え用、効果はなし

        // 研究室アイテムの初期化
        LaboratoryItems["おまんこ触手"] = new ItemEffect();
        LaboratoryItems["おまんこ触手"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                                    baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                    facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 1, analLvl: 0, boobLvl: 0);

        LaboratoryItems["あなる触手"] = new ItemEffect();
        LaboratoryItems["あなる触手"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                                    baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                    facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 0, analLvl: 1, boobLvl: 0);

        LaboratoryItems["おっぱい触手"] = new ItemEffect();
        LaboratoryItems["おっぱい触手"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                                    baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                    facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 0, analLvl: 0, boobLvl: 1);

        LaboratoryItems["媚薬触手"] = new ItemEffect();
        LaboratoryItems["媚薬触手"].Initialize(maxLevel: 5, initialRequiredMilk: 200,
                                                baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                spermSz: 0f, semenDens: 0, pleasureAdd: 15,
                                                pussyLvl: 0, analLvl: 0, boobLvl: 0);

        LaboratoryItems["ちんぽに餌やり"] = new ItemEffect();
        LaboratoryItems["ちんぽに餌やり"].Initialize(maxLevel: 5, initialRequiredMilk: 300,
                                                    baseEjac: 0f, dickPowerEjac: 0.5f, ballPowerEjac: 0.5f,
                                                    facilityMult: 0f, dickLen: 1, ballLvl: 1,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 0, analLvl: 0, boobLvl: 0);
    }

    /// <summary>
    /// データをセーブします。
    /// </summary>
    public void Save()
    {
        ES3.Save(saveKey, playerData);
        ES3.Save(eienteiKey, EienteiItems);
        ES3.Save(workshopKey, WorkshopItems);
        ES3.Save(laboratoryKey, LaboratoryItems);
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
            if (ES3.KeyExists(eienteiKey))
                EienteiItems = ES3.Load<Dictionary<string, ItemEffect>>(eienteiKey);
            if (ES3.KeyExists(workshopKey))
                WorkshopItems = ES3.Load<Dictionary<string, ItemEffect>>(workshopKey);
            if (ES3.KeyExists(laboratoryKey))
                LaboratoryItems = ES3.Load<Dictionary<string, ItemEffect>>(laboratoryKey);
            Debug.Log("ロード完了");
        }
        else
        {
            Debug.Log("セーブデータがありません。新規データを作成します。");
            // 新規ゲームの場合、ここでplayerDataと各アイテムの初期化が適用されます。
            // InitializeItemData()はAwakeで既に呼び出されています。
        }
    }

    /// <summary>
    /// 1日あたりの射精量を計算します。
    /// </summary>
    public void CalculateDailyEjaculation()
    {
        float randomRate = UnityEngine.Random.Range(0.95f, 1.05f); // 毎日少し変動する乱数
        playerData.EjaculationAmount =
            (playerData.BaseEjaculation +
             playerData.DickPowerEjaculation +
             playerData.BallPowerEjaculation +
             playerData.AddictionEjaculation)
            * playerData.FacilityMultiplier
            * randomRate;
    }

    /// <summary>
    /// 指定されたカテゴリと名前のアイテムをレベルアップさせます。
    /// </summary>
    /// <param name="category">アイテムのカテゴリ (e.g., "Eientei", "Workshop", "Laboratory")</param>
    /// <param name="itemName">アイテムの名前</param>
    /// <returns>レベルアップが成功したかどうか</returns>
    public bool TryLevelUpItem(string category, string itemName)
    {
        Dictionary<string, ItemEffect> targetDictionary;

        switch (category)
        {
            case "Eientei":
                targetDictionary = EienteiItems;
                break;
            case "Workshop":
                targetDictionary = WorkshopItems;
                break;
            case "Laboratory":
                targetDictionary = LaboratoryItems;
                break;
            default:
                Debug.LogError($"未知のアイテムカテゴリ: {category}");
                return false;
        }

        if (targetDictionary.TryGetValue(itemName, out ItemEffect item))
        {
            return item.LevelUp(playerData);
        }
        else
        {
            Debug.LogError($"アイテムが見つかりません: カテゴリ={category}, 名前={itemName}");
            return false;
        }
    }

    /// <summary>
    /// アイテムのレベルアップ機能の使用例です。（UIからの呼び出しなどを想定）
    /// </summary>
    public void ExampleLevelUpUsage()
    {
        // 例えば「フタナリン」をレベルアップさせる場合
        playerData.DickMilk += 1000; // テスト用にちんぽミルクを増やす
        Debug.Log($"レベルアップ前ちんぽミルク: {playerData.DickMilk}");
        Debug.Log($"レベルアップ前ちんぽ長さ: {playerData.DickLength}");
        Debug.Log($"レベルアップ前基本射精量: {playerData.BaseEjaculation}");

        if (TryLevelUpItem("Eientei", "フタナリン"))
        {
            Debug.Log("フタナリンのレベルアップに成功しました！");
            Debug.Log($"レベルアップ後ちんぽミルク: {playerData.DickMilk}");
            Debug.Log($"レベルアップ後ちんぽ長さ: {playerData.DickLength}");
            Debug.Log($"レベルアップ後基本射精量: {playerData.BaseEjaculation}");
        }
        else
        {
            Debug.Log("フタナリンのレベルアップに失敗しました。");
        }

        // 別のアイテムを試す
        playerData.DickMilk += 500; // テスト用にちんぽミルクを増やす
        if (TryLevelUpItem("Workshop", "電動オナホール"))
        {
            Debug.Log("電動オナホールのレベルアップに成功しました！");
        }
        else
        {
            Debug.Log("電動オナホールのレベルアップに失敗しました。");
        }
    }
}