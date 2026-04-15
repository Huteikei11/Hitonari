using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainSceneManager : MonoBehaviour
{
    public int sceneMode; // 0:ホーム画面, 1:永遠亭, 2:河城にとり, 3:パチェの部屋 4:アイテムパネル 5:レミリアの様子
    public GameObject ItemPanelShop; // アイテムパネルの親オブジェクト
    public List<GameObject> homeScreenObjects;

    public List<GameObject> eienteiScreenObjects;
    public List<GameObject> kawasiroScreenObjects;
    public List<GameObject> pacheScreenObjects;

    // アイテムのパネル群 4はアイテムのパネル
    public List<GameObject> itemPanels;
    // レミリアの様子のパネル群 5はレミリアの様子のパネル
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
        sceneMode = mode; // 現在のモードを更新
        if (mode == 0 || mode == 4 || mode == 5)
        {
            ItemPanelShop.SetActive(false); // アイテムパネルを非表示にする
        }
        SetActiveObjects(homeScreenObjects, mode == 0 || mode == 4 || mode == 5); // ホーム画面はモード0と4で表示


        SetActiveObjects(eienteiScreenObjects, mode == 1 );
        SetActiveObjects(kawasiroScreenObjects, mode == 2);
        SetActiveObjects(pacheScreenObjects, mode == 3);
        SetActiveObjects(viewPanels, mode == 5); // レミリアの様子のパネルのアクティブ状態を設定
        SetPanelsActive(mode == 4); // アイテムパネルのアクティブ状態を設定
        SetUIGroupActive(homebuttonPanels, !(mode ==5)); // レミリアの様子のパネルがアクティブな場合、ホームのボタンパネルを非アクティブにする
        SetUIGroupActive(homebuttonPanels, !(mode == 4)); // アイテムパネルがアクティブな場合、ホームのボタンパネルを非アクティブにする 
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
