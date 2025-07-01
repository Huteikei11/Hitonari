using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainSceneManager : MonoBehaviour
{
    public int sceneMode; // 0 = Home, 1 = Pache, 2 = Kawasiro, 3 = Eientei
    public List<GameObject> homeScreenObjects;
    public List<GameObject> pacheScreenObjects;
    public List<GameObject> kawasiroScreenObjects;
    public List<GameObject> eienteiScreenObjects;

    // �A�C�e���̃p�l���Q
    public List<GameObject> itemPanels;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ��ʐ؂�ւ�������Q�Ƃ���
    public void SwitchScreen(int mode)
    {
        sceneMode = mode;

        SetActiveObjects(homeScreenObjects, mode == 0);
        SetActiveObjects(pacheScreenObjects, mode == 1);
        SetActiveObjects(kawasiroScreenObjects, mode == 2);
        SetActiveObjects(eienteiScreenObjects, mode == 3);
    }

    public void SetPanelsActive(bool isActive)
    {
        // �A�C�e���p�l���̃A�N�e�B�u��Ԃ�ݒ肷��B
        SetUIGroupActive(itemPanels, isActive);
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
