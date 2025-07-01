using UnityEngine;
using UnityEngine.UI; // UI�v�f���g���ꍇ

public class ItemUpgradeUI : MonoBehaviour
{
    // �V�[�����SaveManager�C���X�^���X�ւ̎Q��
    public SaveManager saveManager;

    // UI�{�^���̃C�x���g�Ɋ��蓖�Ă郁�\�b�h
    public void OnUpgradeButtonClicked(string category, string itemName)
    {
        if (saveManager == null)
        {
            Debug.LogError("SaveManager���A�T�C������Ă��܂���I");
            return;
        }

        // SaveManager��TryLevelUpItem���\�b�h���Ăяo��
        // bool success = saveManager.TryLevelUpItem(category, itemName);
        bool success = false; // �����ł̓_�~�[�̐����t���O���g�p

        if (success)
        {
            Debug.Log($"{itemName} �̃��x���A�b�v�ɐ������܂����I");
            // UI���X�V���鏈���Ȃǂ������ɒǉ�
            UpdateUI();
        }
        else
        {
            Debug.Log($"{itemName} �̃��x���A�b�v�Ɏ��s���܂����B");
            // ���s�������R�ɉ��������b�Z�[�W��\�����鏈���Ȃǂ������ɒǉ�
        }
    }

    // ��: UI�̕\�����X�V����_�~�[���\�b�h
    private void UpdateUI()
    {
        Debug.Log("UI���X�V��...");
        // �����Ɍ��݂̃v���C���[�f�[�^��A�C�e�����x����UI�ɔ��f���郍�W�b�N���������܂�
        // ��: �A�C�e�����ɑΉ�����Text�R���|�[�l���g�̕\�����X�V
        // ��: ����ۃ~���N�̎c�ʂ�\������Text�R���|�[�l���g�̕\�����X�V
    }
}