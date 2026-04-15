using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro; // TextMeshProを使用するために必要

public class ShowStatus : MonoBehaviour
{

    // Inspectorから紐づけるTextMeshPro
    [SerializeField]
    private TextMeshProUGUI statusUI;

    // ステータスの種類を定義
    public enum StatusType
    {
        Mental,
        Penis,
        Testicle,
        Bust,
        Vagina,
        Thickness
    }

    // Inspectorで確認できる文字列のリスト
    public List<string> statusTexts;

    void Awake()
    {
        // Enumの要素数に合わせてリストを初期化
        int statusCount = System.Enum.GetNames(typeof(StatusType)).Length;
        statusTexts = new List<string>(new string[statusCount]);
    }

    // オブジェクトがアクティブ（表示状態）になるたびに呼ばれる
    void OnEnable()
    {
        // statusUIが設定されているか確認してから更新する
        if (statusUI != null)
        {
            statusUI.text = GetStatus();
        }
        else
        {
            Debug.LogWarning("ShowStatus: TextMeshProがアタッチされていません。");
        }
    }

    // （Startの中身はOnEnableで都度更新するため削除、または用途に合わせて残しても構いません）
    void Start()
    {
    }

    
    public string GetStatus() 
    {
        ChoiseStatus(); // ステータスの文章を決める
        return CreateStatusText(); // 合体させて渡す
    }

    // 指定したステータスに文字列を書き込む関数
    public void SetStatus(StatusType type, string text)
    {
        int index = (int)type;
        statusTexts[index] = text;
    }

    // 個別の関数で設定したい場合の書き方
    public void SetMentalStatus(string text) => SetStatus(StatusType.Mental, text);
    public void SetPenisStatus(string text) => SetStatus(StatusType.Penis, text);
    public void SetTesticleStatus(string text) => SetStatus(StatusType.Testicle, text);
    public void SetBustStatus(string text) => SetStatus(StatusType.Bust, text);
    public void SetVaginaStatus(string text) => SetStatus(StatusType.Vagina, text);
    public void SetThicknessStatus(string text) => SetStatus(StatusType.Thickness, text);


    // 全てのステータスを指定のフォーマットで1つのテキストにする
    private string CreateStatusText()
    {
        List<string> formattedStatuses = new List<string>();
        StatusType[] statusTypes = (StatusType[])System.Enum.GetValues(typeof(StatusType));
        
        for (int i = 0; i < statusTypes.Length; i++)
        {
            string text = statusTexts[i];
            
            // 文章が設定されている場合のみ処理する
            if (!string.IsNullOrEmpty(text))
            {
                // Enum名（Mentalなど）を取得
                string statusName = statusTypes[i].ToString();
                
                // "Mental\n文章" の形式を作成
                formattedStatuses.Add($"{statusName}\n{text}");
            }
        }
        
        // 各ブロックを "\n\n"（改行2回＝空行を1行挟む）で繋げる
        return string.Join("\n\n", formattedStatuses);
    }

    private void ChoiseStatus() //ステータスのテキストを条件から選ぶ
    {
        string statusText = "テストテキストです"; // 確認用に一時的に文字を入れています

        SetStatus(StatusType.Mental, statusText);
        SetStatus(StatusType.Penis, statusText);
        SetStatus(StatusType.Testicle, statusText);
        SetStatus(StatusType.Bust, statusText);
        SetStatus(StatusType.Vagina, statusText);
        SetStatus(StatusType.Thickness, statusText); // Testicleが重複していたので修正しました
    }
}
