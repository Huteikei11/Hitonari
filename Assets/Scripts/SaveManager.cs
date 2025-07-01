using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �v���C���[�̑S�p�����[�^�ƃA�C�e���̏��������Ǘ�����N���X�ł��B
/// </summary>
[Serializable]
public class PlayerData
{
    public int Day = 1; // ���݂̓����i1�`60�j
    public float EjaculationAmount = 3f; // 1��������̎ː��ʁiml/L�j
    public float BaseEjaculation = 3f; // ��{�ː��ʁi�����l3ml�j
    public float DickPowerEjaculation = 0f; // ����ۋ����ɂ��ː��ʑ���
    public float BallPowerEjaculation = 0f; // ���ʋ����ɂ��ː��ʑ���
    public float AddictionEjaculation = 0f; // �ː����łɂ��ː��ʑ���
    public float FacilityMultiplier = 1.0f; // �ݔ������{��
    public int DickMilk = 0; // ���݂̂���ۃ~���N�iL�j
    public int TotalDickMilk = 0; // ���܂ŏo��������ۃ~���N���ʁiL�j
    public string TodayFacilityUpgrade = ""; // ���̓��s�����ݔ������i���b�Z�[�W�p�j

    public int DickLength = 10; // ����ۂ̒����icm�j
    public int BallLevel = 0; // ���ʂ̒i�K�i0:����, 1:�r�[��, ...�j
    public float SpermSize = 0.06f; // ���q�T�C�Y�imm�j
    public int SemenDensity = 1; // ���t�Z�x�i���x���j
    public int PleasureAddiction = 0; // ���y���œx�i���x���j
    public int PussyLevel = 0; // ���܂񂱃��x��
    public int AnalLevel = 0; // ���Ȃ郌�x��
    public int BoobLevel = 0; // �����ς����x��

    public bool Mosaic = true; // ���U�C�N�؂�ւ�
    public bool FranRemilia = false; // �t�����E���~���A�؂�ւ�

    // �e�A�C�e���̏�������ۑ����鎫���B�w�������炱����������B
    public Dictionary<string, int> EienteiItemCounts = new Dictionary<string, int>();
    public Dictionary<string, int> WorkshopItemCounts = new Dictionary<string, int>();
    public Dictionary<string, int> LaboratoryItemCounts = new Dictionary<string, int>();

    // �R���X�g���N�^�Ŏ�����������
    public PlayerData()
    {
        // PlayerData���C���X�^���X�������ۂɎ���������������邱�Ƃ�ۏ�
        // �e�A�C�e���̏�������SaveManager�ōs�����߁A�����ł͋�̎����𐶐�
    }
}



/// <summary>
/// �A�C�e���̒�`�i���ʁA�R�X�g�A�ő僌�x���j�ƁA�v���C���[�����̃A�C�e��������g�p�������̏�Ԃ��Ǘ����܂��B
/// </summary>
[Serializable]
public class ItemEffect
{
    public int PurchaseCost = 0;      // �w���ɕK�v�Ȃ���ۃ~���N
    public int CurrentLevel = 0;      // ���݂̌��ʃ��x���i�g�p�񐔁j
    public int MaxLevel = 5;          // �ő���ʃ��x���i�ő�g�p�񐔁j

    // �e�p�����[�^�ւ̌��ʒl�i1���x���A�b�v������̉��Z�l�j
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

    public ItemEffect() { } // �f�t�H���g�R���X�g���N�^

    /// <summary>
    /// �A�C�e���̒�`�f�[�^��ݒ肵�܂��B
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
    /// �A�C�e���̌��ʂ�PlayerData�ɓK�p���܂��B�i1��g�p���邽�тɌĂяo�����j
    /// </summary>
    /// <param name="playerData">�v���C���[�f�[�^</param>
    /// <returns>���ʂ̓K�p�������������ǂ���</returns>
    public bool ApplyEffect(PlayerData playerData)
    {
        if (CurrentLevel >= MaxLevel)
        {
            Debug.Log($"���̃A�C�e���͂���ȏ���ʂ�K�p�ł��܂���i�ő僌�x���ɓ��B�j: CurrentLevel={CurrentLevel}, MaxLevel={MaxLevel}");
            return false;
        }

        CurrentLevel++; // ���ʂ�K�p�����̂Ŏg�p�񐔂𑝂₷

        // �p�����[�^��Playerdata�ɉ��Z
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

        Debug.Log($"�A�C�e���̌��ʂ��K�p����܂����I���݂̌��ʃ��x��: {CurrentLevel}");
        return true;
    }
}



/// <summary>
/// �Z�[�u�E���[�h�E�v�Z�A����уA�C�e���̍w���E�g�p���Ǘ�����N���X�ł��B
/// </summary>
public class SaveManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData(); // �v���C���[�f�[�^

    // �e�J�e�S���̃A�C�e���̒�`�f�[�^�Ƃ��̎g�p���x����ێ�
    public Dictionary<string, ItemEffect> EienteiItems = new Dictionary<string, ItemEffect>();
    public Dictionary<string, ItemEffect> WorkshopItems = new Dictionary<string, ItemEffect>();
    public Dictionary<string, ItemEffect> LaboratoryItems = new Dictionary<string, ItemEffect>();

    private string saveKey = "playerData";
    private string eienteiItemsKey = "eienteiItemStates"; // ItemEffect�̃Z�[�u�L�[�i��Ԃ��܂ށj
    private string workshopItemsKey = "workshopItemStates";
    private string laboratoryItemsKey = "laboratoryItemStates";

    void Awake()
    {
        InitializeItemDefinitions(); // �A�C�e���̒�`�f�[�^��������
        Load(); // �Z�[�u�f�[�^�����[�h
    }

    /// <summary>
    /// �e�A�C�e���̒�`�f�[�^�i�w���R�X�g�A���ʒl�Ȃǁj��ݒ肵�܂��B
    /// �Q�[���J�n���Ɉ�x�����Ăяo����A���[�h�f�[�^�ŏ㏑������܂��B
    /// </summary>
    private void InitializeItemDefinitions()
    {
        // �i�����A�C�e���̒�`
        EienteiItems["�t�^�i����"] = new ItemEffect();
        EienteiItems["�t�^�i����"].Initialize(maxLevel: 5, purchaseCost: 100, baseEjac: 0.5f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 1, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�t�^�i�[��"] = new ItemEffect();
        EienteiItems["�t�^�i�[��"].Initialize(maxLevel: 5, purchaseCost: 200, baseEjac: 1.0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 2, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�ł����܂�"] = new ItemEffect();
        EienteiItems["�ł����܂�"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0.8f, facilityMult: 0f, dickLen: 0, ballLvl: 1, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["���傽�܂��"] = new ItemEffect();
        EienteiItems["���傽�܂��"].Initialize(maxLevel: 5, purchaseCost: 250, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 1.5f, facilityMult: 0f, dickLen: 0, ballLvl: 2, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�����Ă��"] = new ItemEffect();
        EienteiItems["�����Ă��"].Initialize(maxLevel: 5, purchaseCost: 300, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0.01f, semenDens: 1, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�������[��"] = new ItemEffect();
        EienteiItems["�������[��"].Initialize(maxLevel: 5, purchaseCost: 400, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0.02f, semenDens: 2, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�Z��"] = new ItemEffect();
        EienteiItems["�Z��"].Initialize(maxLevel: 5, purchaseCost: 50, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 5, pussyLvl: 0, analLvl: 0, boobLvl: 0);


        // �H�[�A�C�e���̒�`
        WorkshopItems["�I�i�z�[��"] = new ItemEffect();
        WorkshopItems["�I�i�z�[��"].Initialize(maxLevel: 5, purchaseCost: 80, baseEjac: 0f, dickPowerEjac: 0.3f, ballPowerEjac: 0f, facilityMult: 0.02f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["�d���I�i�z�[��"] = new ItemEffect();
        WorkshopItems["�d���I�i�z�[��"].Initialize(maxLevel: 5, purchaseCost: 180, baseEjac: 0f, dickPowerEjac: 0.8f, ballPowerEjac: 0f, facilityMult: 0.05f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["������N"] = new ItemEffect();
        WorkshopItems["������N"].Initialize(maxLevel: 5, purchaseCost: 120, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 10, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["������NEX"] = new ItemEffect();
        WorkshopItems["������NEX"].Initialize(maxLevel: 5, purchaseCost: 220, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 20, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["�i��g�[��"] = new ItemEffect();
        WorkshopItems["�i��g�[��"].Initialize(maxLevel: 5, purchaseCost: 250, baseEjac: 0f, dickPowerEjac: 1.0f, ballPowerEjac: 1.0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["������ʃO�b�Y�u�P�[�z�E175�v"] = new ItemEffect();
        WorkshopItems["������ʃO�b�Y�u�P�[�z�E175�v"].Initialize(maxLevel: 1, purchaseCost: 500, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["������ʃO�b�Y�u�ӂ�񂿂��v"] = new ItemEffect();
        WorkshopItems["������ʃO�b�Y�u�ӂ�񂿂��v"].Initialize(maxLevel: 1, purchaseCost: 500, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        // �������A�C�e���̒�`
        LaboratoryItems["���܂񂱐G��"] = new ItemEffect();
        LaboratoryItems["���܂񂱐G��"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 1, analLvl: 0, boobLvl: 0);

        LaboratoryItems["���Ȃ�G��"] = new ItemEffect();
        LaboratoryItems["���Ȃ�G��"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 1, boobLvl: 0);

        LaboratoryItems["�����ς��G��"] = new ItemEffect();
        LaboratoryItems["�����ς��G��"].Initialize(maxLevel: 5, purchaseCost: 150, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 1);

        LaboratoryItems["�Z��G��"] = new ItemEffect();
        LaboratoryItems["�Z��G��"].Initialize(maxLevel: 5, purchaseCost: 200, baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f, facilityMult: 0f, dickLen: 0, ballLvl: 0, spermSz: 0f, semenDens: 0, pleasureAdd: 15, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        LaboratoryItems["����ۂɉa���"] = new ItemEffect();
        LaboratoryItems["����ۂɉa���"].Initialize(maxLevel: 5, purchaseCost: 300, baseEjac: 0f, dickPowerEjac: 0.5f, ballPowerEjac: 0.5f, facilityMult: 0f, dickLen: 1, ballLvl: 1, spermSz: 0f, semenDens: 0, pleasureAdd: 0, pussyLvl: 0, analLvl: 0, boobLvl: 0);

        // PlayerData���̃A�C�e���J�E���g�����Ɋe�A�C�e���̃G���g����ǉ�
        // ���[�h���Ɋ����f�[�^������΂��ꂪ�D�悳��邽�߁A�����ł͏���N�����̏������̂�
        InitializePlayerItemCounts();
    }

    /// <summary>
    /// PlayerData���̃A�C�e���J�E���g�����ɂ��ׂẴA�C�e�����̃G���g�������邱�Ƃ�ۏ؂��܂��B
    /// ��ɐV�K�Q�[���J�n����A�Q�[���A�b�v�f�[�g�ŐV�����A�C�e�����ǉ����ꂽ�ꍇ�ɗ��p�B
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
    /// �f�[�^���Z�[�u���܂��B
    /// PlayerData�Ɗe�A�C�e���̌��݂̏�ԁiCurrentLevel�j��ۑ����܂��B
    /// </summary>
    public void Save()
    {
        ES3.Save(saveKey, playerData);
        ES3.Save(eienteiItemsKey, EienteiItems);
        ES3.Save(workshopItemsKey, WorkshopItems);
        ES3.Save(laboratoryItemsKey, LaboratoryItems);
        Debug.Log("�Z�[�u����");
    }

    /// <summary>
    /// �f�[�^�����[�h���܂��B�Z�[�u�f�[�^�����݂��Ȃ��ꍇ�͐V�K�f�[�^���쐬���܂��B
    /// </summary>
    public void Load()
    {
        if (ES3.KeyExists(saveKey))
        {
            playerData = ES3.Load<PlayerData>(saveKey);
            Debug.Log("PlayerData ���[�h����");
        }
        else
        {
            Debug.Log("PlayerData ������܂���B�V�K�f�[�^���쐬���܂��B");
            // PlayerData�̃R���X�g���N�^�Ŏ���������������AInitializeItemDefinitions�ŃJ�E���g�G���g�����ǉ������
        }

        // ItemEffect�̏�ԁiCurrentLevel�j�����[�h
        // InitializeItemDefinitions�Őݒ肳�ꂽ�����l�́A���[�h���ꂽ�f�[�^�ŏ㏑������܂�
        if (ES3.KeyExists(eienteiItemsKey))
            EienteiItems = ES3.Load<Dictionary<string, ItemEffect>>(eienteiItemsKey);
        if (ES3.KeyExists(workshopItemsKey))
            WorkshopItems = ES3.Load<Dictionary<string, ItemEffect>>(workshopItemsKey);
        if (ES3.KeyExists(laboratoryItemsKey))
            LaboratoryItems = ES3.Load<Dictionary<string, ItemEffect>>(laboratoryItemsKey);
        Debug.Log("ItemEffect States ���[�h����");

        // ���[�h��ɁA�V�����A�C�e�����ǉ�����Ă����ꍇ�̑Ή�
        InitializePlayerItemCounts();
    }

    /// <summary>
    /// 1��������̎ː��ʂ��v�Z���܂��B
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
    /// �w�肳�ꂽ�A�C�e�����w�����܂��B
    /// ����ۃ~���N������A�v���C���[�̃A�C�e���������𑝂₵�܂��B
    /// </summary>
    /// <param name="category">�A�C�e���̃J�e�S�� (e.g., "Eientei", "Workshop", "Laboratory")</param>
    /// <param name="itemName">�A�C�e���̖��O</param>
    /// <returns>�w���������������ǂ���</returns>
    public bool BuyItem(string category, string itemName)
    {
        Dictionary<string, ItemEffect> itemDefinitions = GetItemDictionary(category);
        Dictionary<string, int> itemCounts = GetPlayerItemCountsDictionary(category);

        if (itemDefinitions == null || itemCounts == null)
            return false; // �G���[��GetItemDictionary/GetPlayerItemCountsDictionary�Ń��O�o�͍ς�

        if (!itemDefinitions.TryGetValue(itemName, out ItemEffect itemDef))
        {
            Debug.LogError($"�A�C�e����`��������܂���: �J�e�S��={category}, ���O={itemName}");
            return false;
        }

        if (playerData.DickMilk < itemDef.PurchaseCost)
        {
            Debug.Log($"����ۃ~���N������܂���B�K�v��: {itemDef.PurchaseCost}, ���ݗ�: {playerData.DickMilk}");
            return false;
        }

        playerData.DickMilk -= itemDef.PurchaseCost; // �R�X�g������
        itemCounts[itemName] = itemCounts.GetValueOrDefault(itemName, 0) + 1; // �������𑝂₷

        Debug.Log($"{itemName} ���w�����܂����I ������: {itemCounts[itemName]}, �c�肿��ۃ~���N: {playerData.DickMilk}");
        Save(); // �w����A�Z�[�u
        return true;
    }

    /// <summary>
    /// �w�肳�ꂽ�A�C�e�����g�p���܂��B
    /// �A�C�e���̏����������炵�A���̌��ʂ��v���C���[�f�[�^�ɓK�p���܂��B
    /// </summary>
    /// <param name="category">�A�C�e���̃J�e�S��</param>
    /// <param name="itemName">�A�C�e���̖��O</param>
    /// <returns>�g�p�������������ǂ���</returns>
    public bool UseItem(string category, string itemName)
    {
        Dictionary<string, ItemEffect> itemDefinitions = GetItemDictionary(category);
        Dictionary<string, int> itemCounts = GetPlayerItemCountsDictionary(category);

        if (itemDefinitions == null || itemCounts == null)
            return false;

        if (!itemDefinitions.TryGetValue(itemName, out ItemEffect itemDef))
        {
            Debug.LogError($"�A�C�e����`��������܂���: �J�e�S��={category}, ���O={itemName}");
            return false;
        }

        if (itemCounts.GetValueOrDefault(itemName, 0) <= 0)
        {
            Debug.Log($"{itemName} �̏�����������܂���B����: {itemCounts.GetValueOrDefault(itemName, 0)}");
            return false;
        }

        // �ő僌�x���ɒB���Ă��邩�ǂ����̃`�F�b�N��ItemEffect.ApplyEffect�ɔC����
        // UseItem���������Ă�ApplyEffect�����s����\�������邽�߁A���̏ꍇ�͍݌ɂ�߂�
        itemCounts[itemName]--; // �����������炷

        bool effectApplied = itemDef.ApplyEffect(playerData); // ���ʂ�K�p
        if (!effectApplied)
        {
            Debug.LogWarning($"{itemName} �̌��ʓK�p�Ɏ��s���܂������A�g�p�͂���܂����B��������߂��܂��B");
            itemCounts[itemName]++; // ���ʓK�p���s�Ȃ̂ŏ�������߂�
            Save(); // ���s������Ԃ��ς��\��������̂ŃZ�[�u
            return false;
        }

        Debug.Log($"{itemName} ���g�p���܂����I �c�菊����: {itemCounts[itemName]}, ���݂̌��ʃ��x��: {itemDef.CurrentLevel}");
        Save(); // �g�p��A�Z�[�u
        return true;
    }

    /// <summary>
    /// �J�e�S�����Ɋ�Â���ItemEffect�������擾���܂��B
    /// </summary>
    private Dictionary<string, ItemEffect> GetItemDictionary(string category)
    {
        switch (category)
        {
            case "Eientei": return EienteiItems;
            case "Workshop": return WorkshopItems;
            case "Laboratory": return LaboratoryItems;
            default:
                Debug.LogError($"���m�̃A�C�e���J�e�S��: {category}");
                return null;
        }
    }

    /// <summary>
    /// �J�e�S�����Ɋ�Â���PlayerData���̃A�C�e���J�E���g�������擾���܂��B
    /// </summary>
    private Dictionary<string, int> GetPlayerItemCountsDictionary(string category)
    {
        switch (category)
        {
            case "Eientei": return playerData.EienteiItemCounts;
            case "Workshop": return playerData.WorkshopItemCounts;
            case "Laboratory": return playerData.LaboratoryItemCounts;
            default:
                Debug.LogError($"���m�̃A�C�e���J�e�S��: {category}");
                return null;
        }
    }

    /// <summary>
    /// �A�C�e���̍w���Ǝg�p�̗�ł��B�iUI����̌Ăяo���Ȃǂ�z��j
    /// </summary>
    public void ExampleItemUsage()
    {
        Debug.Log("--- �A�C�e���w���E�g�p�e�X�g�J�n ---");

        // �e�X�g�p�ɂ���ۃ~���N�𑝂₷
        playerData.DickMilk = 1000;
        Debug.Log($"��������ۃ~���N: {playerData.DickMilk}");
        Debug.Log($"�t�^�i��������������: {playerData.EienteiItemCounts.GetValueOrDefault("�t�^�i����", 0)}");
        Debug.Log($"�t�^�i�����������ʃ��x��: {EienteiItems["�t�^�i����"].CurrentLevel}");
        Debug.Log($"��������ے���: {playerData.DickLength}");

        // 1. �t�^�i������2�w��
        Debug.Log("--- �t�^�i������2�w�� ---");
        BuyItem("Eientei", "�t�^�i����");
        BuyItem("Eientei", "�t�^�i����");
        Debug.Log($"�t�^�i�����w���㏊����: {playerData.EienteiItemCounts.GetValueOrDefault("�t�^�i����", 0)}");
        Debug.Log($"�c�肿��ۃ~���N: {playerData.DickMilk}");

        // 2. �t�^�i������1�g�p
        Debug.Log("--- �t�^�i������1�g�p ---");
        if (UseItem("Eientei", "�t�^�i����"))
        {
            Debug.Log("�t�^�i�����̎g�p�ɐ����I");
        }
        else
        {
            Debug.Log("�t�^�i�����̎g�p�Ɏ��s�B");
        }

        Debug.Log($"�t�^�i�����g�p�㏊����: {playerData.EienteiItemCounts.GetValueOrDefault("�t�^�i����", 0)}");
        Debug.Log($"�t�^�i�����g�p����ʃ��x��: {EienteiItems["�t�^�i����"].CurrentLevel}");
        Debug.Log($"�g�p�タ��ے���: {playerData.DickLength}");

        // 3. �ő僌�x���܂Ńt�^�i�������g�p����� (����Ȃ����͍w������)
        playerData.DickMilk += 5000; // �����Ղ�~���N��ǉ�
        Debug.Log("--- �t�^�i�������ő僌�x���܂Ŏg�p�i�K�v�ɉ����čw���j ---");
        while (EienteiItems["�t�^�i����"].CurrentLevel < EienteiItems["�t�^�i����"].MaxLevel)
        {
            if (playerData.EienteiItemCounts.GetValueOrDefault("�t�^�i����", 0) <= 0)
            {
                Debug.Log("�t�^�i�����̍݌ɂ��Ȃ��̂ōw�������݂܂��B");
                if (!BuyItem("Eientei", "�t�^�i����"))
                {
                    Debug.Log("�t�^�i�������w���ł��܂���ł����B�e�X�g�I���B");
                    break;
                }
            }
            if (UseItem("Eientei", "�t�^�i����"))
            {
                Debug.Log($"�t�^�i�����g�p�����I ���ʃ��x��: {EienteiItems["�t�^�i����"].CurrentLevel}, �c�菊����: {playerData.EienteiItemCounts.GetValueOrDefault("�t�^�i����", 0)}");
            }
            else
            {
                Debug.Log("�t�^�i�����̎g�p�Ɏ��s���܂����B�e�X�g�I���B");
                break;
            }
        }
        Debug.Log($"�ŏI�t�^�i�������ʃ��x��: {EienteiItems["�t�^�i����"].CurrentLevel}, �ŏI����ے���: {playerData.DickLength}");

        Debug.Log("--- �A�C�e���w���E�g�p�e�X�g�I�� ---");
    }
}