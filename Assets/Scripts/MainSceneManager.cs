using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainSceneManager : MonoBehaviour
{
    public int sceneMode; // 0:�z�[�����, 1:�i����, 2:�͏�ɂƂ�, 3:�p�`�F�̕��� 4:�A�C�e���p�l��
    public List<GameObject> homeScreenObjects;

    public List<GameObject> eienteiScreenObjects;
    public List<GameObject> kawasiroScreenObjects;
    public List<GameObject> pacheScreenObjects;

    // �A�C�e���̃p�l���Q
    public List<GameObject> itemPanels;
    // ���~���A�̗l�q�̃p�l���Q
    public List<GameObject> viewPanels;
    // home�̃{�^���̃p�l���Q
    public List<GameObject> homebuttonPanels;


    // Start is called before the first frame update
    void Start()
    {
        SwitchScreen(0); // ������Ԃ̓z�[�����

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ��ʐ؂�ւ�������Q�Ƃ���
    public void SwitchScreen(int mode)
    {
        sceneMode = mode;

        SetActiveObjects(homeScreenObjects, mode == 0 || mode == 4);


        SetActiveObjects(eienteiScreenObjects, mode == 1 );
        SetActiveObjects(kawasiroScreenObjects, mode == 2);
        SetActiveObjects(pacheScreenObjects, mode == 3);
        SetPanelsActive(mode == 4); // �A�C�e���p�l���̃A�N�e�B�u��Ԃ�ݒ�
    }

    public void SetPanelsActive(bool isActive)
    {
        // �A�C�e���p�l���̃A�N�e�B�u��Ԃ�ݒ肷��B
        SetUIGroupActive(itemPanels, isActive);
        if (isActive == true)
        {
            // �A�C�e���p�l�����A�N�e�B�u�ȏꍇ�A�z�[���̃{�^���p�l�����A�N�e�B�u�ɂ���B
            SetUIGroupActive(homebuttonPanels, false);
        }
    }


    private void SetActiveObjects(List<GameObject> objects, bool isActive)
    {
        // �؂�ւ��̓����B
        if (objects == null) return;
        foreach (var obj in objects)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
    }

    private void SetUIGroupActive(List<GameObject> uiGroup, bool isActive)
    {
        // UI�O���[�v�̃A�N�e�B�u��Ԃ�ݒ肷��B
        if (uiGroup == null) return;
        foreach (var obj in uiGroup)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
    }
}
