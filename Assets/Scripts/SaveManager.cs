using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̑S�p�����[�^���Ǘ�����N���X�ł��B
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
}


/// <summary>
/// �A�C�e���̌��ʁE��Ԃ��Ǘ�����N���X�ł��B
/// </summary>
[Serializable]
public class ItemEffect
{
    public int CurrentLevel = 0;          // ���݂̃��x��
    public int MaxLevel = 5;              // �ő僌�x��
    public int RequiredDickMilk = 0;      // ���x���A�b�v�ɕK�v�Ȃ���ۃ~���N

    // �e�p�����[�^�ւ̌��ʒl
    public float BaseEjaculation = 0f;      // ��{�ː��ʂւ̉��Z
    public float DickPowerEjaculation = 0f; // ����ۋ����ː��ʂւ̉��Z
    public float BallPowerEjaculation = 0f; // ���܂��܋����ː��ʂւ̉��Z
    public float FacilityMultiplier = 0f;   // �ݔ������{���ւ̉��Z
    public int DickLength = 0;              // ����ے����ւ̉��Z
    public int BallLevel = 0;               // ���ʃ��x���ւ̉��Z
    public float SpermSize = 0f;            // ���q�T�C�Y�ւ̉��Z
    public int SemenDensity = 0;            // ���t�Z�x�ւ̉��Z
    public int PleasureAddiction = 0;       // ���y���œx�ւ̉��Z
    public int PussyLevel = 0;              // ���܂񂱃��x���ւ̉��Z
    public int AnalLevel = 0;               // ���Ȃ郌�x���ւ̉��Z
    public int BoobLevel = 0;               // �����ς����x���ւ̉��Z

    // �f�t�H���g�R���X�g���N�^�i�������p�j
    public ItemEffect() { }

    /// <summary>
    /// �A�C�e���̏����l��ݒ肵�܂��B
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
    /// �A�C�e�������x���A�b�v�����APlayerData�Ɍ��ʂ�K�p���܂��B
    /// ���̃��\�b�h��SaveManager����Ăяo����邱�Ƃ�z�肵�Ă��܂��B
    /// </summary>
    /// <param name="playerData">�v���C���[�f�[�^</param>
    /// <returns>���x���A�b�v�������������ǂ���</returns>
    public bool LevelUp(PlayerData playerData)
    {
        if (CurrentLevel >= MaxLevel)
        {
            Debug.Log($"�A�C�e���͍ő僌�x���ł�: CurrentLevel={CurrentLevel}, MaxLevel={MaxLevel}");
            return false;
        }

        if (playerData.DickMilk < RequiredDickMilk)
        {
            Debug.Log($"����ۃ~���N������܂���B�K�v��: {RequiredDickMilk}, ���ݗ�: {playerData.DickMilk}");
            return false;
        }

        // �R�X�g������
        playerData.DickMilk -= RequiredDickMilk;
        CurrentLevel++;

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

        // ���̃��x���A�b�v�ɕK�v�Ȃ���ۃ~���N�𑝂₷�i��: ���݂̃��x���ɉ����đ����j
        // ����̓Q�[���̃o�����X�ɂ���Ē������Ă��������B
        RequiredDickMilk = (int)(RequiredDickMilk * 1.5f) + 10; // ��: 1.5�{�ɂ���10L�ǉ�

        Debug.Log($"�A�C�e�������x���A�b�v���܂����I���݂̃��x��: {CurrentLevel}");
        return true;
    }
}

/// <summary>
/// �Z�[�u�E���[�h�E�v�Z���Ǘ�����N���X�ł��B
/// </summary>
public class SaveManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData(); // �v���C���[�f�[�^

    // �i�����A�C�e��
    public Dictionary<string, ItemEffect> EienteiItems = new Dictionary<string, ItemEffect>();
    // �H�[�A�C�e��
    public Dictionary<string, ItemEffect> WorkshopItems = new Dictionary<string, ItemEffect>();
    // �������A�C�e��
    public Dictionary<string, ItemEffect> LaboratoryItems = new Dictionary<string, ItemEffect>();

    private string saveKey = "playerData"; // �Z�[�u�f�[�^�̃L�[
    private string eienteiKey = "eienteiItems";
    private string workshopKey = "workshopItems";
    private string laboratoryKey = "laboratoryItems";

    void Awake()
    {
        InitializeItemData();
        Load(); // �V�[���J�n���Ƀ��[�h�����݂�
    }

    /// <summary>
    /// �e�A�C�e���̏����f�[�^��ݒ肵�܂��B
    /// </summary>
    private void InitializeItemData()
    {
        // �i�����A�C�e���̏�����
        EienteiItems["�t�^�i����"] = new ItemEffect();
        EienteiItems["�t�^�i����"].Initialize(maxLevel: 5, initialRequiredMilk: 100,
                                            baseEjac: 0.5f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 1, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�t�^�i�[��"] = new ItemEffect();
        EienteiItems["�t�^�i�[��"].Initialize(maxLevel: 5, initialRequiredMilk: 200,
                                            baseEjac: 1.0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 2, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�ł����܂�"] = new ItemEffect();
        EienteiItems["�ł����܂�"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0.8f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 1,
                                        spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["���傽�܂��"] = new ItemEffect();
        EienteiItems["���傽�܂��"].Initialize(maxLevel: 5, initialRequiredMilk: 250,
                                            baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 1.5f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 2,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�����Ă��"] = new ItemEffect();
        EienteiItems["�����Ă��"].Initialize(maxLevel: 5, initialRequiredMilk: 300,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                        spermSz: 0.01f, semenDens: 1, pleasureAdd: 0,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�������[��"] = new ItemEffect();
        EienteiItems["�������[��"].Initialize(maxLevel: 5, initialRequiredMilk: 400,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                        spermSz: 0.02f, semenDens: 2, pleasureAdd: 0,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);

        EienteiItems["�Z��"] = new ItemEffect();
        EienteiItems["�Z��"].Initialize(maxLevel: 5, initialRequiredMilk: 50,
                                        baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                        facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                        spermSz: 0f, semenDens: 0, pleasureAdd: 5,
                                        pussyLvl: 0, analLvl: 0, boobLvl: 0);


        // �H�[�A�C�e���̏�����
        WorkshopItems["�I�i�z�[��"] = new ItemEffect();
        WorkshopItems["�I�i�z�[��"].Initialize(maxLevel: 5, initialRequiredMilk: 80,
                                            baseEjac: 0f, dickPowerEjac: 0.3f, ballPowerEjac: 0f,
                                            facilityMult: 0.02f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["�d���I�i�z�[��"] = new ItemEffect();
        WorkshopItems["�d���I�i�z�[��"].Initialize(maxLevel: 5, initialRequiredMilk: 180,
                                                baseEjac: 0f, dickPowerEjac: 0.8f, ballPowerEjac: 0f,
                                                facilityMult: 0.05f, dickLen: 0, ballLvl: 0,
                                                spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["������N"] = new ItemEffect();
        WorkshopItems["������N"].Initialize(maxLevel: 5, initialRequiredMilk: 120,
                                            baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 10,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["������NEX"] = new ItemEffect();
        WorkshopItems["������NEX"].Initialize(maxLevel: 5, initialRequiredMilk: 220,
                                            baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 20,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["�i��g�[��"] = new ItemEffect();
        WorkshopItems["�i��g�[��"].Initialize(maxLevel: 5, initialRequiredMilk: 250,
                                            baseEjac: 0f, dickPowerEjac: 1.0f, ballPowerEjac: 1.0f,
                                            facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                            spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                            pussyLvl: 0, analLvl: 0, boobLvl: 0);

        WorkshopItems["������ʃO�b�Y�u�P�[�z�E175�v"] = new ItemEffect();
        WorkshopItems["������ʃO�b�Y�u�P�[�z�E175�v"].Initialize(maxLevel: 1, initialRequiredMilk: 500,
                                                                baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                                facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                                spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                                pussyLvl: 0, analLvl: 0, boobLvl: 0); // �t�����E���~���A�؂�ւ��p�A���ʂ͂Ȃ�

        WorkshopItems["������ʃO�b�Y�u�ӂ�񂿂��v"] = new ItemEffect();
        WorkshopItems["������ʃO�b�Y�u�ӂ�񂿂��v"].Initialize(maxLevel: 1, initialRequiredMilk: 500,
                                                                baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                                facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                                spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                                pussyLvl: 0, analLvl: 0, boobLvl: 0); // �t�����E���~���A�؂�ւ��p�A���ʂ͂Ȃ�

        // �������A�C�e���̏�����
        LaboratoryItems["���܂񂱐G��"] = new ItemEffect();
        LaboratoryItems["���܂񂱐G��"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                                    baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                    facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 1, analLvl: 0, boobLvl: 0);

        LaboratoryItems["���Ȃ�G��"] = new ItemEffect();
        LaboratoryItems["���Ȃ�G��"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                                    baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                    facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 0, analLvl: 1, boobLvl: 0);

        LaboratoryItems["�����ς��G��"] = new ItemEffect();
        LaboratoryItems["�����ς��G��"].Initialize(maxLevel: 5, initialRequiredMilk: 150,
                                                    baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                    facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 0, analLvl: 0, boobLvl: 1);

        LaboratoryItems["�Z��G��"] = new ItemEffect();
        LaboratoryItems["�Z��G��"].Initialize(maxLevel: 5, initialRequiredMilk: 200,
                                                baseEjac: 0f, dickPowerEjac: 0f, ballPowerEjac: 0f,
                                                facilityMult: 0f, dickLen: 0, ballLvl: 0,
                                                spermSz: 0f, semenDens: 0, pleasureAdd: 15,
                                                pussyLvl: 0, analLvl: 0, boobLvl: 0);

        LaboratoryItems["����ۂɉa���"] = new ItemEffect();
        LaboratoryItems["����ۂɉa���"].Initialize(maxLevel: 5, initialRequiredMilk: 300,
                                                    baseEjac: 0f, dickPowerEjac: 0.5f, ballPowerEjac: 0.5f,
                                                    facilityMult: 0f, dickLen: 1, ballLvl: 1,
                                                    spermSz: 0f, semenDens: 0, pleasureAdd: 0,
                                                    pussyLvl: 0, analLvl: 0, boobLvl: 0);
    }

    /// <summary>
    /// �f�[�^���Z�[�u���܂��B
    /// </summary>
    public void Save()
    {
        ES3.Save(saveKey, playerData);
        ES3.Save(eienteiKey, EienteiItems);
        ES3.Save(workshopKey, WorkshopItems);
        ES3.Save(laboratoryKey, LaboratoryItems);
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
            if (ES3.KeyExists(eienteiKey))
                EienteiItems = ES3.Load<Dictionary<string, ItemEffect>>(eienteiKey);
            if (ES3.KeyExists(workshopKey))
                WorkshopItems = ES3.Load<Dictionary<string, ItemEffect>>(workshopKey);
            if (ES3.KeyExists(laboratoryKey))
                LaboratoryItems = ES3.Load<Dictionary<string, ItemEffect>>(laboratoryKey);
            Debug.Log("���[�h����");
        }
        else
        {
            Debug.Log("�Z�[�u�f�[�^������܂���B�V�K�f�[�^���쐬���܂��B");
            // �V�K�Q�[���̏ꍇ�A������playerData�Ɗe�A�C�e���̏��������K�p����܂��B
            // InitializeItemData()��Awake�Ŋ��ɌĂяo����Ă��܂��B
        }
    }

    /// <summary>
    /// 1��������̎ː��ʂ��v�Z���܂��B
    /// </summary>
    public void CalculateDailyEjaculation()
    {
        float randomRate = UnityEngine.Random.Range(0.95f, 1.05f); // ���������ϓ����闐��
        playerData.EjaculationAmount =
            (playerData.BaseEjaculation +
             playerData.DickPowerEjaculation +
             playerData.BallPowerEjaculation +
             playerData.AddictionEjaculation)
            * playerData.FacilityMultiplier
            * randomRate;
    }

    /// <summary>
    /// �w�肳�ꂽ�J�e�S���Ɩ��O�̃A�C�e�������x���A�b�v�����܂��B
    /// </summary>
    /// <param name="category">�A�C�e���̃J�e�S�� (e.g., "Eientei", "Workshop", "Laboratory")</param>
    /// <param name="itemName">�A�C�e���̖��O</param>
    /// <returns>���x���A�b�v�������������ǂ���</returns>
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
                Debug.LogError($"���m�̃A�C�e���J�e�S��: {category}");
                return false;
        }

        if (targetDictionary.TryGetValue(itemName, out ItemEffect item))
        {
            return item.LevelUp(playerData);
        }
        else
        {
            Debug.LogError($"�A�C�e����������܂���: �J�e�S��={category}, ���O={itemName}");
            return false;
        }
    }

    /// <summary>
    /// �A�C�e���̃��x���A�b�v�@�\�̎g�p��ł��B�iUI����̌Ăяo���Ȃǂ�z��j
    /// </summary>
    public void ExampleLevelUpUsage()
    {
        // �Ⴆ�΁u�t�^�i�����v�����x���A�b�v������ꍇ
        playerData.DickMilk += 1000; // �e�X�g�p�ɂ���ۃ~���N�𑝂₷
        Debug.Log($"���x���A�b�v�O����ۃ~���N: {playerData.DickMilk}");
        Debug.Log($"���x���A�b�v�O����ے���: {playerData.DickLength}");
        Debug.Log($"���x���A�b�v�O��{�ː���: {playerData.BaseEjaculation}");

        if (TryLevelUpItem("Eientei", "�t�^�i����"))
        {
            Debug.Log("�t�^�i�����̃��x���A�b�v�ɐ������܂����I");
            Debug.Log($"���x���A�b�v�タ��ۃ~���N: {playerData.DickMilk}");
            Debug.Log($"���x���A�b�v�タ��ے���: {playerData.DickLength}");
            Debug.Log($"���x���A�b�v���{�ː���: {playerData.BaseEjaculation}");
        }
        else
        {
            Debug.Log("�t�^�i�����̃��x���A�b�v�Ɏ��s���܂����B");
        }

        // �ʂ̃A�C�e��������
        playerData.DickMilk += 500; // �e�X�g�p�ɂ���ۃ~���N�𑝂₷
        if (TryLevelUpItem("Workshop", "�d���I�i�z�[��"))
        {
            Debug.Log("�d���I�i�z�[���̃��x���A�b�v�ɐ������܂����I");
        }
        else
        {
            Debug.Log("�d���I�i�z�[���̃��x���A�b�v�Ɏ��s���܂����B");
        }
    }
}