using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム定義の管理を専門に行うクラス
/// SaveManagerから分離してコードの可読性を向上させる
/// </summary>
public class ItemDefinitions : MonoBehaviour
{
    [Header("Item Sprites")]
    public Sprite futanalinSprite;
    public Sprite futanaruSprite;
    public Sprite dekaTamanSprite;
    public Sprite kyotamarinSprite;
    public Sprite gotterinSprite;
    public Sprite sazukaruSprite;
    public Sprite biryakuSprite;
    public Sprite onaholeSprite;
    public Sprite denDounaholeSprite;
    public Sprite kairaKunSprite;
    public Sprite kairaKunEXSprite;
    public Sprite shiboriToolSprite;
    public Sprite keihou175Sprite;
    public Sprite furanChanSprite;
    public Sprite omankoShokushuSprite;
    public Sprite analShokushuSprite;
    public Sprite oppaiShokushuSprite;
    public Sprite biryakuShokushuSprite;
    public Sprite chinpoNiEsaYariSprite;

    /// <summary>
    /// 永遠亭アイテムの定義を初期化します
    /// </summary>
    public void InitializeEienteiItems(Dictionary<string, ItemEffect> eienteiItems)
    {
        // 永遠亭アイテムの定義
        eienteiItems["フタナリン"] = new ItemEffect();
        eienteiItems["フタナリン"].Initialize(maxLevel: 5, purchaseCost: 100, baseEjac: 0.5f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 1, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                            sprite: futanalinSprite,
                                            description: "男の娘になれる魔法の薬。ちんぽが少し伸びます。");

        eienteiItems["フタナール"] = new ItemEffect();
        eienteiItems["フタナール"].Initialize(maxLevel: 5, purchaseCost: 200, baseEjac: 1.0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 2, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                            sprite: futanaruSprite,
                                            description: "究極のフタナ薬。ちんぽが大きく育ちます。");

        eienteiItems["でかたまん"] = new ItemEffect();
        eienteiItems["でかたまん"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0.8f, facilityMult: 0f, dickLen: 0, ballLvl: 1, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                        sprite: dekaTamanSprite,
                                        description: "金玉を大きくする薬。射精量が増えます。");

        eienteiItems["きょたまりん"] = new ItemEffect();
        eienteiItems["きょたまりん"].Initialize(maxLevel: 5, purchaseCost: 250, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 1.5f, facilityMult: 0f, dickLen: 0, ballLvl: 2, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                            sprite: kyotamarinSprite,
                                            description: "超でかたまになる薬。射精量がさらに増加します。");

        eienteiItems["ごってりん"] = new ItemEffect();
        eienteiItems["ごってりん"].Initialize(maxLevel: 5, purchaseCost: 300, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0.01f, semenDens: 1, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                        sprite: gotterinSprite,
                                        description: "精子を巨大化させる薬。");

        eienteiItems["さずかーる"] = new ItemEffect();
        eienteiItems["さずかーる"].Initialize(maxLevel: 5, purchaseCost: 400, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0.02f, semenDens: 2, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                        sprite: sazukaruSprite,
                                        description: "精液の濃度を高める薬。");

        eienteiItems["媚薬"] = new ItemEffect();
        eienteiItems["媚薬"].Initialize(maxLevel: 5, purchaseCost: 50, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 5, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                        sprite: biryakuSprite,
                                        description: "快楽中毒度を上げる媚薬。");
    }

    /// <summary>
    /// 工房アイテムの定義を初期化します
    /// </summary>
    public void InitializeWorkshopItems(Dictionary<string, ItemEffect> workshopItems)
    {
        workshopItems["オナホール"] = new ItemEffect();
        workshopItems["オナホール"].Initialize(maxLevel: 5, purchaseCost: 80, baseEjac: 0f, dickPowerEjac: 0.3f, ballPowerEjac: 0f, facilityMult: 0.02f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                            sprite: onaholeSprite,
                                            description: "基本的なオナホール。");

        workshopItems["電動オナホール"] = new ItemEffect();
        workshopItems["電動オナホール"].Initialize(maxLevel: 5, purchaseCost: 180, baseEjac: 0f, dickPowerEjac: 0.8f, ballPowerEjac: 0f, facilityMult: 0.05f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                                sprite: denDounaholeSprite,
                                                description: "強力な電動オナホール。");

        workshopItems["かいら君"] = new ItemEffect();
        workshopItems["かいら君"].Initialize(maxLevel: 5, purchaseCost: 120, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 10, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                            sprite: kairaKunSprite,
                                            description: "快感を高める自慰具。");

        workshopItems["かいら君EX"] = new ItemEffect();
        workshopItems["かいら君EX"].Initialize(maxLevel: 5, purchaseCost: 220, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 20, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                            sprite: kairaKunEXSprite,
                                            description: "超快感の自慰具。");

        workshopItems["絞りトール"] = new ItemEffect();
        workshopItems["絞りトール"].Initialize(maxLevel: 5, purchaseCost: 250, baseEjac: 0f, dickPowerEjac: 1.0f, ballPowerEjac: 1.0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                            sprite: shiboriToolSprite,
                                            description: "射精量を増やすための特殊な道具。");

        workshopItems["特殊迷彩グッズ「ケーホウ175」"] = new ItemEffect();
        workshopItems["特殊迷彩グッズ「ケーホウ175」"].Initialize(maxLevel: 1, purchaseCost: 500, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                                                sprite: keihou175Sprite,
                                                                description: "フタナレミリアを隠すための迷彩グッズ。");

        workshopItems["特殊迷彩グッズ「ふらんちゃん」"] = new ItemEffect();
        workshopItems["特殊迷彩グッズ「ふらんちゃん」"].Initialize(maxLevel: 1, purchaseCost: 500, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                                                sprite: furanChanSprite,
                                                                description: "フランちゃん用の特殊迷彩グッズ。");
    }

    /// <summary>
    /// 研究室アイテムの定義を初期化します
    /// </summary>
    public void InitializeLaboratoryItems(Dictionary<string, ItemEffect> laboratoryItems)
    {
        laboratoryItems["おまんこ触手"] = new ItemEffect();
        laboratoryItems["おまんこ触手"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 1, analLvl: 0, boobLvl: 0,
                                                    sprite: omankoShokushuSprite,
                                                    description: "おまんこのレベルを上げる触手。");

        laboratoryItems["あなる触手"] = new ItemEffect();
        laboratoryItems["あなる触手"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 1, boobLvl: 0,
                                                    sprite: analShokushuSprite,
                                                    description: "あなるのレベルを上げる触手。");

        laboratoryItems["おっぱい触手"] = new ItemEffect();
        laboratoryItems["おっぱい触手"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 1,
                                                    sprite: oppaiShokushuSprite,
                                                    description: "おっぱいのレベルを上げる触手。");

        laboratoryItems["媚薬触手"] = new ItemEffect();
        laboratoryItems["媚薬触手"].Initialize(maxLevel: 5, purchaseCost: 200, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 15, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                                sprite: biryakuShokushuSprite,
                                                description: "快楽中毒度を大幅に上げる触手。");

        laboratoryItems["ちんぽに餌やり"] = new ItemEffect();
        laboratoryItems["ちんぽに餌やり"].Initialize(maxLevel: 5, purchaseCost: 300, baseEjac: 0f, dickPowerEjac: 0.5f, ballPowerEjac: 0.5f, facilityMult: 0f, dickLen: 1, ballLvl: 1, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0,
                                                    sprite: chinpoNiEsaYariSprite,
                                                    description: "ちんぽと金玉を同時に強化する。");
    }

    /// <summary>
    /// ロード後にアイテムのスプライトを再割り当てします
    /// </summary>
    public void RestoreItemSprites(Dictionary<string, ItemEffect> eienteiItems, 
                                   Dictionary<string, ItemEffect> workshopItems, 
                                   Dictionary<string, ItemEffect> laboratoryItems)
    {
        // Eientei
        if (eienteiItems.ContainsKey("フタナリン")) eienteiItems["フタナリン"].itemSprite = futanalinSprite;
        if (eienteiItems.ContainsKey("フタナール")) eienteiItems["フタナール"].itemSprite = futanaruSprite;
        if (eienteiItems.ContainsKey("でかたまん")) eienteiItems["でかたまん"].itemSprite = dekaTamanSprite;
        if (eienteiItems.ContainsKey("きょたまりん")) eienteiItems["きょたまりん"].itemSprite = kyotamarinSprite;
        if (eienteiItems.ContainsKey("ごってりん")) eienteiItems["ごってりん"].itemSprite = gotterinSprite;
        if (eienteiItems.ContainsKey("さずかーる")) eienteiItems["さずかーる"].itemSprite = sazukaruSprite;
        if (eienteiItems.ContainsKey("媚薬")) eienteiItems["媚薬"].itemSprite = biryakuSprite;

        // Workshop
        if (workshopItems.ContainsKey("オナホール")) workshopItems["オナホール"].itemSprite = onaholeSprite;
        if (workshopItems.ContainsKey("電動オナホール")) workshopItems["電動オナホール"].itemSprite = denDounaholeSprite;
        if (workshopItems.ContainsKey("かいら君")) workshopItems["かいら君"].itemSprite = kairaKunSprite;
        if (workshopItems.ContainsKey("かいら君EX")) workshopItems["かいら君EX"].itemSprite = kairaKunEXSprite;
        if (workshopItems.ContainsKey("絞りトール")) workshopItems["絞りトール"].itemSprite = shiboriToolSprite;
        if (workshopItems.ContainsKey("特殊迷彩グッズ「ケーホウ175」")) workshopItems["特殊迷彩グッズ「ケーホウ175」"].itemSprite = keihou175Sprite;
        if (workshopItems.ContainsKey("特殊迷彩グッズ「ふらんちゃん」")) workshopItems["特殊迷彩グッズ「ふらんちゃん」"].itemSprite = furanChanSprite;

        // Laboratory
        if (laboratoryItems.ContainsKey("おまんこ触手")) laboratoryItems["おまんこ触手"].itemSprite = omankoShokushuSprite;
        if (laboratoryItems.ContainsKey("あなる触手")) laboratoryItems["あなる触手"].itemSprite = analShokushuSprite;
        if (laboratoryItems.ContainsKey("おっぱい触手")) laboratoryItems["おっぱい触手"].itemSprite = oppaiShokushuSprite;
        if (laboratoryItems.ContainsKey("媚薬触手")) laboratoryItems["媚薬触手"].itemSprite = biryakuShokushuSprite;
        if (laboratoryItems.ContainsKey("ちんぽに餌やり")) laboratoryItems["ちんぽに餌やり"].itemSprite = chinpoNiEsaYariSprite;
    }
}