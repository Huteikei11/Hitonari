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

    // アイテムのパネル群
    public List<GameObject> itemPanels;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 画面切り替えこれを参照する
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
        // アイテムパネルのアクティブ状態を設定する。
        SetUIGroupActive(itemPanels, isActive);
    }


    private void SetActiveObjects(List<GameObject> objects, bool isActive)
    {
        // 切り替えの内部。
        if (objects == null) return;
        foreach (var obj in objects)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
    }

    private void SetUIGroupActive(List<GameObject> uiGroup, bool isActive)
    {
        // UIグループのアクティブ状態を設定する。
        if (uiGroup == null) return;
        foreach (var obj in uiGroup)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
    }
}
