using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainSceneManager : MonoBehaviour
{
    public int sceneMode; // 0:ホーム画面, 1:永遠亭, 2:河城にとり, 3:パチェの部屋 4:アイテムパネル
    public List<GameObject> homeScreenObjects;

    public List<GameObject> eienteiScreenObjects;
    public List<GameObject> kawasiroScreenObjects;
    public List<GameObject> pacheScreenObjects;

    // アイテムのパネル群
    public List<GameObject> itemPanels;
    // レミリアの様子のパネル群
    public List<GameObject> viewPanels;
    // homeのボタンのパネル群
    public List<GameObject> homebuttonPanels;


    // Start is called before the first frame update
    void Start()
    {
        SwitchScreen(0); // 初期状態はホーム画面

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 画面切り替えこれを参照する
    public void SwitchScreen(int mode)
    {
        sceneMode = mode;

        SetActiveObjects(homeScreenObjects, mode == 0 || mode == 4);


        SetActiveObjects(eienteiScreenObjects, mode == 1 );
        SetActiveObjects(kawasiroScreenObjects, mode == 2);
        SetActiveObjects(pacheScreenObjects, mode == 3);
        SetPanelsActive(mode == 4); // アイテムパネルのアクティブ状態を設定
    }

    public void SetPanelsActive(bool isActive)
    {
        // アイテムパネルのアクティブ状態を設定する。
        SetUIGroupActive(itemPanels, isActive);
        if (isActive == true)
        {
            // アイテムパネルがアクティブな場合、ホームのボタンパネルを非アクティブにする。
            SetUIGroupActive(homebuttonPanels, false);
        }
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
