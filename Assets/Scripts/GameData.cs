using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの定義（効果、コスト、最大レベル）と、プレイヤーがそのアイテムを何回使用したかの状態を管理します。
/// </summary>
[Serializable]
public class ItemEffect
{
    public int PurchaseCost = 0;      // 購入に必要なちんぽミルク
    public int CurrentLevel = 0;      // 現在の効果レベル（使用回数）
    public int MaxLevel = 5;          // 最大効果レベル（最大使用回数）

    [NonSerialized] // Unityのエディタでは表示されるが、Easy Saveでシリアライズしない
    public Sprite itemSprite; // このアイテムの表示用画像

    public string itemDescription; // アイテムの説明テキスト

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

    public ItemEffect() { }

    /// <summary>
    /// アイテムの定義データを設定します。
    /// </summary>
    public void Initialize(int maxLevel, int purchaseCost,
                           float baseEjac, float dickPowerEjac, float ballPowerEjac,
                           float facilityMult, int dickLen, int ballLvl,
                           float spermSz, int semenDens, int pleasureAdd,
                           int pussyLvl, int analLvl, int boobLvl,
                           Sprite sprite, string description)
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
        itemSprite = sprite;
        itemDescription = description;
    }

    /// <summary>
    /// アイテムの効果をプレイヤーデータに適用します
    /// </summary>
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
/// プレイヤーの全パラメータとアイテムの所持数を管理するクラスです。
/// </summary>
[Serializable]
public class PlayerData
{
    public int Day = 1;
    public float EjaculationAmount = 3f;
    public float BaseEjaculation = 3f;
    public float DickPowerEjaculation = 0f;
    public float BallPowerEjaculation = 0f;
    public float AddictionEjaculation = 0f;
    public float FacilityMultiplier = 1.0f;
    public int DickMilk = 300;
    public int TotalDickMilk = 300;
    public string TodayFacilityUpgrade = "";

    public int DickLength = 10;
    public int BallLevel = 0;
    public float SpermSize = 0.06f;
    public int SemenDensity = 1;
    public int PleasureAddiction = 0;
    public int PussyLevel = 0;
    public int AnalLevel = 0;
    public int BoobLevel = 0;

    public bool Mosaic = true;
    public bool FranRemilia = false;

    // 各アイテムの所持数を保存する辞書。購入したらここが増える。
    public Dictionary<string, int> EienteiItemCounts = new Dictionary<string, int>();
    public Dictionary<string, int> WorkshopItemCounts = new Dictionary<string, int>();
    public Dictionary<string, int> LaboratoryItemCounts = new Dictionary<string, int>();

    public PlayerData()
    {
        // PlayerDataがインスタンス化される際に辞書も初期化されることを保証
    }
}