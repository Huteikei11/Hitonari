using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System; // TextMeshProを使用するために必要

public class ShowStatus : MonoBehaviour
{

    // Inspectorから紐づけるTextMeshPro
    [SerializeField]
    private TextMeshProUGUI statusUI;
    [SerializeField] private SaveManager saveManager; // SaveManagerをインスペクターから紐づける

    // ステータスの種類を定義
    public enum StatusType
    {
        Mental,
        Penis,
        Testicle,
        Bust,
        Vagina,
        Anal,
        Thickness,
        Sperm
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
    public void SetAnalStatus(string text) => SetStatus(StatusType.Anal, text);
    public void SetThicknessStatus(string text) => SetStatus(StatusType.Thickness, text);
    public void SetSpermStatus(string text) => SetStatus(StatusType.Sperm, text);


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


        statusText = ChoiseMental(2);//saveManager.playerData.PleasureAddiction); // 淫乱度を元に精神状態の文章を選ぶ。
        SetStatus(StatusType.Mental, statusText);

        statusText = ChoisePenis(2); // ちんぽの大きさを元にちんぽの文章を選ぶ。
        SetStatus(StatusType.Penis, statusText);

        statusText = ChoiseTesticle(2);
        SetStatus(StatusType.Testicle, statusText);
        
        statusText = ChoiseBust(2);
        SetStatus(StatusType.Bust, statusText);

        statusText = ChoiseVagina(2);
        SetStatus(StatusType.Vagina, statusText);

        statusText = ChoiseAnal(2);
        SetStatus(StatusType.Anal, statusText);
        
        statusText = ChoiseTesticle(2);
        SetStatus(StatusType.Thickness, statusText);

        statusText = ChoiseSperm(2);
        SetStatus(StatusType.Sperm, statusText);

    }







    private String ChoiseMental(int value) 
    {
        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }



        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "「まったく、どうして私が…まあいいわ。減るものでもないし、効率が良いというなら付き合いましょう」\r\n生殖については当然知っていたが、無関心だった。性的刺激に対しては、不快ではないが自ら求めるようなものでないと思っている。";
        }
        else if (num == 2)
        {
            return "「まさか、毎日毎日射精することになるなんてね。さっさと片づけたいものだわ。それにしても、大きくなったせいで結構じゃまね。男も大変なのね」\r\n必要に応じて性的刺激を受け入れ、何度も絶頂を経験した。今のところ、早く今回の厄介ごとが終わらないかなと思っている。";
        }
        else if (num == 3)
        {
            return "「もっと沢山射精すためだからって…あんなものまで。まあ、一言に快感と言ってもなかなか違いがあるし、面白いものね」\r\n気が付けば、ちんぽだけでは無く様々な性感帯を開発されるようになった。気持ちが良いにもいろいろと違いがあるものだなと、快感に興味を持ち始めている。";
        }
        else if (num == 4)
        {
            return "「いろいろと手を加えたからかしら、大きく育ったペニスに違和感も無いし、とても心地いいわ。快感を得るために、こんなに様々な道具が生まれるなんて、もはや一つの文化ね」\r\n性的快感を良いものとして受け入れ、すっかり搾精の時間が楽しみになった。元々好奇心旺盛だったからか、日々新しい薬や玩具に目を輝かせ、成長し肥大化するちんぽと金玉を楽しそうに眺めている。";
        }
        else if (num == 5)
        {
            return "「せっかく、こんなに気持ち良くなれる身体になったのだから、有効活用しないとね。だけど、流石に大きいわね…簡単に刺激できる玩具を私室にも置こうかしら」\r\n毎日の搾精だけではなく、自分でもプライベートな時間に自慰をするようになった。ちんぽをいじって、ゆっくりと上り詰める快感を味わったり、激しく連続で絶頂を味わったり、その日に物足りなかった快感を自分で補っている。今回の厄介ごとが解決しても、日々の自慰はレミリアの日課として続いていくだろう。\r\n\r\n";
        }
        else if (num == 6)
        {
            return "「ん…❤ふーーっ❤ふう❤この道具もいい具合ね。ふふ、また楽しみが増えたわ。次はどうやって…ちんぽもおまんこも、お尻も…、まだまだ…もっと…」\r\nちんぽだけではなく、おまんこやアナルも自慰で使うようになった。開発された身体で2穴をほじくり、ちんぽを刺激し、金玉を揺らし、全身を焼く快楽を味わっている。自分はこんなに気持ち良くなれる、それに気が付かせてくれた今回の機会に感謝している。…借金返済のための搾精以外に、毎日数時間自慰に時間を使うため、日常生活に若干の支障が出始めた。\r\n";
                  
        }
        else if (num == 7)
        {
            return "「うふ❤うふふ❤今日はどうするのかしら❤ずぅっと気持ちが良くて毎日幸せだわ。おちんぽこんなに大きくなって…もう少し、んっんぐっ、おっ❤はっ❤はああぁぁぁぁ❤」\r\n毎日目覚めと共に媚薬を摂取するようになり、快楽に溶けた頭で1日を過ごしている。その方がより性感開発が進むから…搾精効率が良いから…と自分に言い訳をしているが、調教中使用した媚薬や触手の媚薬体液の依存性に、心が耐えきれなくなっただけである。誇り高き吸血鬼はもう…。\r\n\r\n";
        }
        else if (num == 8)
        {
            return "「はぁ❤はあぁぁぁ❤おちんぽっ❤じゅぼじゅぼしてっ❤他のところも忘れちゃダメよ？ああ、こんな幸せがあるなんて❤このために今まで生きてきたのね❤」\r\nこんなに気持ちが良いなんて、これが限界だと思った快感を、次々と更新されていく。こんなに大きくなるなんて、こんなに広がるなんて、こんなに続くなんて。どんどん夢中になっていく。あれも、これも、まだまだ足りない、もっともっと。そういえば、借金の残りはどれくらいだったかしら？まあいいわ。そんなの関係ない。射精して射精して、気持ち良くなって。幸せになることが私の全て。\r\n\r\n";
        }
        else if (num == 9)
        {
            return "いぐっ❤いぐいぐいぐいぐうううぅぅぅぅぅっ❤んぁっ❤これ、射精っ❤すごいわっ❤金玉、ずっと出してるっ❤止まらなっ❤ほっ❤ほおおおおぉぉぉっ❤おまんこっ❤みちみちっ❤ひりょがってっ❤んぎいいいぃぃぃっ❤ひっひぎゅううううぅぅぅっ❤にゃゃゃゃっ！？おまたっ❤これぇ？おひりまんこもっ❤ケツイキっ❤おひりいぐううぅぅぅぅっ❤ちんぴょがっ一杯射精してるのにっ♥おわらにゃっ♥すぐに出来立てせーえきがっ♥もうこわれちゃっ❤ひぎっぱなしでっ❤おっぐおおおぉぉぉっ❤\r\n";
        }
        else if (num == 10)
        {
            return "";
        }
        else if (num == 11)
        {
            return "";
        }
        else if (num == 12)
        {
            return "";
        }
        else if (num == 13)
        {
            return "";
        }

        return "";
    }

    private String ChoisePenis(int value)
    {
        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }

        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "長さ3cm/まったく成熟していない、可愛らしい幼児ちんぽ。刺激を与えると、皮をかむったままぴょこんと勃起する。";
        }
        else if (num == 2)
        {
            return "長さ7cm/成長の兆しを見せる小さな子供ちんぽ。勃起してもまだ亀頭の先も見えないが、亀頭の張りがわかるようになってきた。";
        }
        else if (num == 3)
        {
            return "長さ12cm/生殖器らしくなってきた、思春期ちんぽ。勃起すると、普段は外気に触れぬぷりぷりの亀頭が顔を見せる。";
        }
        else if (num == 4)
        {
            return "長さ15cm/一般的なサイズにまで成長した大人ちんぽ。太さも増し、亀頭がむっちりと膨らんだ。勃起するとしっかり皮がむけ、粘膜の色合いがいやらしいコントラストを見せる。";
        }
        else if (num == 5)
        {
            return "長さ23cm/ぶっとい血管が走り、亀頭が反り返る、たぐいまれなる巨根ちんぽ。子供の腕ほどもあるこのちんぽでメス穴をほじくられれば、一瞬で女の幸せを叩き込まれるだろう。";
        }
        else if (num == 6)
        {
            return "長さ35cm/長大で極太、大柄な大人の腕ほどもある巨大なちんぽ。もはや女を犯し壊す凶器だが…今はひたすら精液を絞られる可哀そうなちんぽだ。レミリアほどの魔力を持った妖怪ならば、自然に成長していけば、やがてこの程度のふたなり巨ちんぽへと至ったであろう。";
        }
        else if (num == 7)
        {
            return "長さ50cm/もはや、レミリアの脚ほどの大きさになった巨ちんぽ。まるで脚が3本生えているようだ。尿道は腕を呑み込めるほど広がり、脈打つ血管の太さは普通のちんぽほど、亀頭は濃い赤になり、まさに異形のちんぽだ。このちんぽをくわえ込めるのは、人外の者だけだろう。偶に…小悪魔がその腹を醜く歪ませながら、レミリア巨ちんぽでボコ腹セックスを楽しんでいるとか。";
        }
        else if (num == 8)
        {
            return "長さ90cm/長さと共に、一気に太く成長し、レミリアの胴体より極太になったちんぽ。力強く反り返り、亀頭だけで人間の顔よりも大きい。重さも、レミリアの身体の体重よりちんぽの方が重い始末で、吸血鬼としての身体能力がなければ、レミリアは身動きも出来なくなっていただろう。…小悪魔が、肉体強化魔法に筋弛緩薬、一時的な骨格破壊などを併用し、お腹をカエルのように膨らませてこのちんぽを受け入れていたとか。";
        }
        else if (num == 9)
        {
            return "長さ140cm/ついにレミリアの身長よりも長くなったふたなり巨ちんぽ。レミリアにちんぽが生えているのか、ちんぽにレミリアが生えているのか…。あまりにも巨大なちんぽのため、小さな子供や妖精ならちんぽの中に入れるかもしれない。…人の形ではもはや受け入れようがない。これ以上大きくなっても快感に寄与しないと小悪魔が巨ちんぽのつまみ食いを諦めたらしい。";
        }
        else if (num == 10)
        {
            return "長さ180cm/大柄な大人ほどの大きさに育ったでかちんぽ。まるで建物の柱のような、堂々たる肉柱がそびえ立つ。大蛇のような血管がまとわりつくようにからみ、耳をすませばその脈動を感じられるようだ。脈打つたびに熱を感じさせ、ときおり溢れ出す先走りはまるで桶をひっくり返したよう。先端は濡れ光り、淫靡な雰囲気を漂わせている。";
        }
        else if (num == 11)
        {
            return "長さ260cm/普通の部屋だと天井にぶつかるほどの肉柱。さらに太くなり、まるで名のある巨木のようだ。これだけ育っても感度は全く衰えること無く、巨ちんぽ全体で感じた快楽をレミリアの身体に送り込む。某九尾の狐や、全てを食らうもふもふに夢を支配するもふもふ等、巨大な獣形態のおまんこにも対抗できるだろう。あまりに大きいちんぽのため、快感に跳ねる巨ちんぽの動きに巻き込まれてメイド妖精がピチュった事がある。";
        }
        else if (num == 12)
        {
            return "長さ320cm/あまりに巨大になったため、見る方向によっては一目ではちんぽと分からなくなってしまったちんぽ。そのスケールは見る者を圧倒し、近づくだけで震えが止まらない。もはや性的な意味での震えではなく、畏怖の感情が沸き上がる。";
        }
        else if (num == 13)
        {
            return "長さ580cm/ゆとりのある建築、紅魔館ですら部屋に収まらなくなってきたふたなりちんぽ。それはちんぽというより、正に肉塊である。大きすぎるちんぽは、逆にすけべに見えなくなったと不満の声が一部から上がっている。";
        }

        return "";
    }

    private String ChoiseTesticle(int value)
    {
        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }

        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "直径1cm体積0.55cm³/小さなブルーベリーサイズの睾丸。玉袋も身体に密着しており、目立たない未発達な状態。性ホルモンの分泌も始まっていないが、レミリアの膨大な魔力が精力に変換され続けるので、刺激さえ与えれば十分に射精する。";
        }
        else if (num == 2)
        {
            return "直径2cm体積4.189cm³/小さなチェリーほどの大きさ。玉袋もわずかに膨らみ、触るとほんの少し弾力が感じられる。人間の医学に当てはめると、思春期の始まりの睾丸サイズである。";
        }
        else if (num == 3)
        {
            return "直径3cm14.137cm³/ミニトマトほどの大きさ。大人に向けて成長途中のサイズで、玉袋もまだ幼さが残る様子だ。";
        }
        else if (num == 4)
        {
            return "直径4cm体積33.510cm³/大型のぶどうほどの大きさ。一般的な成人男性のサイズで、外見もしっかりとした金玉袋になっている。立派に性ホルモンを分泌し、レミリアに射精欲求を訴えかけている。";
        }
        else if (num == 5)
        {
            return "直径6cm体積113.097cm³/スモモほどの大きさで、人間にとっては記録的な大きさだ。巨玉を含んだ袋がずっしりと垂れ下がっているのが外から見てもわかる。";
        }
        else if (num == 6)
        {
            return "直径10cm体積523.599cm³/大人が両手で作った握り拳より大きいサイズ。あまりの重さに皮が伸びつつ金玉袋が垂れ下がっている。並みの成人男性の10倍以上の容量を誇り、びゅるびゅると大量の精液を吐き出し、さらに凄まじい速度で新たな精液を製造する。レミリアほどの魔力を持った妖怪ならば、自然に成長していけば、やがてこの程度のふたなり巨玉へと至ったであろう。";
        }
        else if (num == 7)
        {
            return "直径18cm体積3053.628 cm³/小型のスイカほどの大きさ。スイカが2つ詰まった肉袋は、膝より下までずっしりと垂れ下がっている。表面には太い血管が走り、その金玉の力強さをうかがわせる。もしもおまんこにその中身をぶちまければ、大量の精液によってたちまち胎が臨月の妊婦のように膨らむだろう。";
        }
        else if (num == 8)
        {
            return "直径31cm体積15596.502cm³/頭よりも大きくなったでっぷり巨玉。大量の精液を貯蔵し、凄まじい射精欲求を訴えかける。脳より大きい金玉が送り付ける欲望は、強い精神力がないと耐えきれないだろう。そう、いかにレミリアであろうと、薬や触手調教で、その心を折る後押しをされていれば…射精狂いに身を堕とすかもしれない。";
        }
        else if (num == 9)
        {
            return "直径48cm体積57905.835cm³/膝を抱え座る子供ほどの大きさ。レミリアの身体よりも体積で言えば大きいほどだ。もはやレミリアに金玉が生えているのか、金玉にレミリアが生えているのか…。その能力は非常に高まり、耳を近づけると、ぎゅるぎゅると大量の精液を製造する音が聞こえるだろう。あまりに多量の精液は、射精をしていない時に、自然にちんぽからあふれ出すこともあるほどだ。";
        }
        else if (num == 10)
        {
            return "直径64cm体積137258.877cm³/座り込んだ大人ほどの大きさ。表面の血管はますます太くなり、張り詰めた皮膚が繁殖力の高さをうかがわせる。2つ合わせれば浴槽よりも大きな容量であり、精液風呂も楽しめるだろう。金玉の重さが200ｋｇを超えたため、家具や床に注意が必要。";
        }
        else if (num == 11)
        {
            return "直径98cm体積492468.695cm³/２玉合わせて家畜の牛ほどの大きさ。吸血鬼の身体能力で無理矢理ひきずっても、足をとられてまともに動けない。人間が一生かかっても射精せないほどの量の精液が蓄えられており、離れていてもぐるぐると唸るような精液製造音が聞こえる。";
        }
        else if (num == 12)
        {
            return "直径150cm体積1767145.867cm³/1つで身長よりも大きく、角度次第ではレミリアが見えないほどの巨玉。多量の精液が欲望となって煮えたぎり、もしもさとり妖怪が近くにいれば、精子たちの着床要求に脳を犯されてしまうだろう。";
        }
        else if (num == 13)
        {
            return "直径200cm体積4188790.204cm³/ちょっとした資材小屋ほどの大きさの金玉袋。逆に一目では金玉だとわからないかもしれない。人知を超えた射精欲求が渦巻き、欲望の渦が半ば妖怪化しかけている。主人の思考すら犯し、人生をかけて射精し続けるための傀儡にしようとしている。平時のレミリアの魔力なら安定して対抗できるが、欲望に負けていると・・・。";
        }

        return "";
    }

    private String ChoiseBust(int value)
    {

        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }


        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "ほとんど膨らみもなく、快感を知らない未発達な胸。身を清める際に自身で触れたこともあるが、乳房も乳首もただの皮膚としての反応しか返さなかった。";
        }
        else if (num == 2)
        {
            return "触手による性感開発が始まり感度が上昇しつつある、性を知り成長し始めた胸。優しく刺激をすると次第に気持ちよくなるが、絶頂に至るほどではない。";
        }
        else if (num == 3)
        {
            return "触手による刺激と媚薬体液による体質の作り替えが順調に進んでいる。見た目は変わらないが、成人女性の感度ほどには感じる。しかし、おっぱいアクメは難易度が高く、イキそうな気配は感じるが、まだ絶頂にたどり着くことは出来ていない。";
        }
        else if (num == 4)
        {
            return "触手による執拗な乳房開発により、ついにおっぱい絶頂を達成した。ただ、乳房全体での絶頂ではなく、乳首責めによる絶頂のため、まだまだ開発の余地は残っている。ぷしゃぷしゃと快楽が噴き出すような乳首アクメはレミリアも気に入ったようで、ふたなり搾精の休み時間に、自分でも胸を触るようになった。";
        }
        else if (num == 5)
        {
            return "乳房全体によるおっぱい絶頂を達成するために、日々触手が性感開発を続けている。今回はあくまでふたなりちんぽの射精量を増やすことが目的のため、膨乳改造は行っていない。触手たちは限界を超えて成長して熟れきった乳房に、肥大した乳暈に改造させてくれれば、もっと簡単に終わらないおっぱい絶頂に叩き込めるのにと不満顔です。";
        }
        else if (num == 6)
        {
            return "ついに乳房全体による、深いバスト絶頂を達成した。快感が冷たく神経を通り抜け、首筋から脳天まで駆け上がるおっぱいアクメは、確実にレミリアの価値観を塗り替えた。塗り替えてしまった…。";
        }
        else if (num == 7)
        {
            return "開発されきった乳房の先端は常に尖り、刺激を待っている。下着をつけていても抑えることが出来ず、日常生活で不意な絶頂に達するようになった。開発をしなければ味わえない胸の絶頂感は格別で、日々レミリアの精神をむしばんでいる。";
        }
        else if (num == 8)
        {
            return "おっぱい全体が完全に性感帯となった。もはや、子供をはぐぐむ事ではなく、快感を感じる事だけが仕事の淫らな器官だ。その控えめな乳肉が揺れるだけでも深い快感が生じるため、おっぱいアクメで身体が跳ねるたび、その振動でさらなる絶頂が襲う快楽地獄に囚われている。いいや、快楽に満たされる柔肉が2つもついているのだ、レミリアも積極的に触手に胸をこすりつけている。きっと地獄ではなく「天国」だろう。";
        }
        else if (num == 9)
        {
            return "";
        }
        else if (num == 10)
        {
            return "";
        }
        else if (num == 11)
        {
            return "";
        }
        else if (num == 12)
        {
            return "";
        }
        else if (num == 13)
        {
            return "";
        }

        return "";
    }

    private String ChoiseVagina(int value)
    {

        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }

        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "ふたなりのため、淫核はちんぽになっている。スジがぴっちりと閉じた、快感を知らない未発達な女性器。清潔にしているが、快楽のために触れたことは無い。";
        }
        else if (num == 2)
        {
            return "機械や触手による性感開発が始まり感度が上昇しつつある、性を知り目覚め始めたおまんこ。しかし、まだくすぐったいだけのようだ。レミリアも性知識は当然持っているが、まさかこんなにすぐに使う時が来るとは思っていなかった。今回は射精を促すための性感開発のため、機械は挿入許可を出したが、触手の挿入は許していない。";
        }
        else if (num == 3)
        {
            return "物理的な刺激と薬品による丹念な開発によって、軽い絶頂を達成したそろそろ一人前の女性器。触手に膣口全体を揉まれながら、ちょんちょんとつつかれるのが特に気持ち良いらしい。機械による神経まで読み取った正確なピストンや揉みほぐし、触手の媚薬体液で開発が進んでおり、日々その疼きは強まっている。";
        }
        else if (num == 4)
        {
            return "膣口は閉じているが淫唇がややぷっくりしてきたおまんこ。開発はさらに進み、今では貪欲にイキ続け快感を味わっている。機械が劣っているというわけでは無いが、触手にも挿入許可を出せば、Gスポット、ポルチオ、子宮口、子宮内とおまんこ内部のさらに多くの性感帯で、今以上の快楽を味わえるだろう。搾精の休みに、レミリア一人でこっそりと指を入れてみたが、自分ではうまく気持ち良さを拾えなかった…。";
        }
        else if (num == 5)
        {
            return "さらなる快感への欲望が抑えられなくなり、ついに触手に挿入許可を出した。散々媚薬体液が浸み込んだおまんこは痛み無く触手を受け入れ、容易く絶頂に達した。ものの1時間で、おまんこ内部の複数の性感帯でアクメし、腹の中から外側に爆発するような中イキ連続絶頂を叩き込まれてしまった。レミリアの快楽神経の発達を感じ取った調教マシンもそれを読み取り、日々のおまんこ絶頂が何倍にも強化されてしまった。";
        }
        else if (num == 6)
        {
            return "おまんこ本来の役目である、ほじられアクメを味わってしまい、完全に性に目覚めた淫らな穴。普通のちんぽを受け入れる前に、休むことなく動き続ける触手の、快楽を与えるためだけの形を味わってしまったため、レミリアがいつか愛する者と結ばれても、性に満足は出来ないだろう。救出された触手苗床の被害者が、自ら触手のもとに向かって身を捧げるのもこれが原因である。";
        }
        else if (num == 7)
        {
            return "淫唇は伸びて黒ずみ始め、メス穴はぽっかりと口を広げている開発され切ったおまんこ。常に快感を求めて疼き、涎を垂れ流している。ちんぽ絞りが終わった後、おまんこから触手が抜け出すのを見て寂しく感じてしまうほどだ。触手は様々な形状のものを受け入れ、調教マシンのアタッチメントも刺激パターンも日々調整し、飽きることなく開発を続けている。";
        }
        else if (num == 8)
        {
            return "完全に作り変えられた、女の幸せを味わうためだけのおまんこ。イキ続けているのが普通で、刺激により更なる超強絶頂へと至り天国を日々味わっている。膣口は腕のような太さだろうと呑み込み、調教され切った子宮口は柔軟に口を開き、その中にまで相手を受け入れる。もはや、返済のための搾精とは関係なしに、24時間触手や固定式の調教機器をくわえ込んでいる。";
        }
        else if (num == 9)
        {
            return "";
        }
        else if (num == 10)
        {
            return "";
        }
        else if (num == 11)
        {
            return "";
        }
        else if (num == 12)
        {
            return "";
        }
        else if (num == 13)
        {
            return "";
        }

        return "";
    }

    private String ChoiseAnal(int value)
    {

        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }


        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "薄紅色で固く閉じた菊穴は、何かを受け入れられるようなサイズではない。お尻の穴も適切に開発すれば、快感を感じ、大量射精の助けになると説明を聞いて内心引いている。";
        }
        else if (num == 2)
        {
            return "性感開発が始まった、未知の刺激に翻弄される肛門。丹念に撫でまわされ、潤滑液でほぐされ、軽く穴を刺激されて、ゆっくりと緊張を解かれつつある。まだ刺激に対する違和感が大きく、さらなる調教が必要だろう。";
        }
        else if (num == 3)
        {
            return "外部からの刺激に慣れ、口を広げ始めたお尻。小指ほどの太さから受け入れ始めて、じっくりと太いものを受け入れられるように調教が進んでいる。痛み無く挿入を受け入れることが出来たが、快感を感じるにはまだ遠い様子だ。";
        }
        else if (num == 4)
        {
            return "じっくりと開発され、アナルセックスが出来る程度にほぐれた肛門。しかし、お尻だけの刺激ではまだあまり快感は感じない。ちんぽやおまんこなどの他の性感帯と同時に刺激され、快感を結び付け快感神経を発展させる手法をとっている。また、搾精おわりには腸内に媚薬を注ぎ込み、腸全体の感度も24時間開発されている。";
        }
        else if (num == 5)
        {
            return "快感を感じ始め、性器と化しつつある菊穴。肛門が広がる感覚、深めに挿入されお腹の奥が揺さぶられる感覚など、丹念な調教でついに肛門快感を感じ始めた。肛門だけでの絶頂も近いだろう。また、プレイ前のケツ穴ほぐしもすっかり短時間で済むようになった。";
        }
        else if (num == 6)
        {
            return "ついにアナルアクメを達成した淫らな肛門。お腹の中がねじ切れるような激しい肛門絶頂は独特の中毒性があり、レミリアはアナルプラグや極太触手による24時間アナル拡張を認めてしまった。さらなる開発のために広げ続けられた肛門は、現在6～7cmほどまで広がり、極太の玩具や触手を受け入れることが出来る。また、肛門からさらに奥の、結腸、大腸、さらには小腸まで触手が侵入し、開発が進んでいる。";
        }
        else if (num == 7)
        {
            return "何度も肛門絶頂を経験し、もはや性器と化した哀れなケツ穴。ぐっぽりと引き延ばされた肛門はイキ続け、奥の奥では腸の刺激で腹全体が揺さぶられ、腹膜の揺れによる内臓全体の壮大なアクメが襲い掛かる。感度はもちろん拡張もさらに進み、プレイ中は腹が膨れるほど奥まで触手を受け入れ快楽をむさぼっている。レミリアが身を清めているときに、つい我慢できず肛門自慰をした時に、強めに押し込んだ指がさらに呑み込まれ、気が付いた時には腕ごと肛門に呑み込まれてイキまくったことがある。";
        }
        else if (num == 8)
        {
            return "もはや排泄するだけで絶頂する、完全に作り変えられたケツまんこ。肛門イキ、結腸イキ、腹膜イキ、休むことなくイキ続け、レミリアの胴体は痙攣がおさまらない。大人の腕だろうと呑み込み、しかし吸血鬼の肉体は広がり切った穴でも十分に締め付け、相手に快感を与える完璧な具合に開発された。返済のための搾精とは関係なしにアナル寄生型の触手を住まわせ、24時間肛門快感をむさぼっている。";
        }
        else if (num == 9)
        {
            return "";
        }
        else if (num == 10)
        {
            return "";
        }
        else if (num == 11)
        {
            return "";
        }
        else if (num == 12)
        {
            return "";
        }
        else if (num == 13)
        {
            return "";
        }

        return "";
    }
    private String ChoiseThickness(int value)
    {

        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }


        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "身体的には幼いレミリアを、無理に開発し搾り取っているからだろうか、しゃばしゃばとした粘性が少ない精液だ。ぷしゃぷしゃとちんぽから噴き出し、さらさらと流れていく。";
        }
        else if (num == 2)
        {
            return "精液の質を改善する薬によって強化され、とろりとした中にぷりっとした塊がある、一般的な濃さの精液になった。糸を引いて流れ、所々に色が濃い塊が漂ういやらしい見た目だ。";
        }
        else if (num == 3)
        {
            return "より価値のある精液にするために、たくさんのお薬を摂取している。全体的にプリプリのゼリーのような濃厚精液になった。吐き出された精液はほとんど途切れずにぶりゅぶりゅと塊を作りながら流れていく。";
        }
        else if (num == 4)
        {
            return "多量の薬物摂取により、体質ごと変化しつつある。ゼリーよりもさらに粘度がある練乳のような濃厚精液。さらに、所々に半固形物が混じる特濃精液になった。";
        }
        else if (num == 5)
        {
            return "開発され切った金玉は、濃く重い精の塊を蓄える。吐き出された精液は、もはや流れずに地面に山を作るようになった。手で触れると簡単に持ち上げることが出来、千切ったり丸めたりと、まるでつきたてのお餅のようだ。";
        }
        else if (num == 6)
        {
            return "";
        }
        else if (num == 7)
        {
            return "";
        }
        else if (num == 8)
        {
            return "";
        }
        else if (num == 9)
        {
            return "";
        }
        else if (num == 10)
        {
            return "";
        }
        else if (num == 11)
        {
            return "";
        }
        else if (num == 12)
        {
            return "";
        }
        else if (num == 13)
        {
            return "";
        }

        return "";
    }

    private String ChoiseSperm(int value)
    {

        // 番号を条件分岐から決定する
        int num = 0;
        if (value <= 1)
        {
            num = 0;
        }
        else if (value >= 1)
        {
            num = 1;
        }
        else if (value <= 1)
        {
            num = 2;
        }
        else if (value <= 1)
        {
            num = 3;
        }
        else if (value <= 1)
        {
            num = 4;
        }
        else if (value <= 1)
        {
            num = 5;
        }
        else if (value <= 1)
        {
            num = 7;
        }
        else if (value <= 1)
        {
            num = 8;
        }
        else if (value <= 1)
        {
            num = 9;
        }
        else if (value <= 1)
        {
            num = 10;
        }
        else if (value <= 1)
        {
            num = 11;
        }
        else if (value <= 1)
        {
            num = 12;
        }
        else if (value <= 1)
        {
            num = 13;
        }

        if (num == 0)
        {
            return "";
        }
        else if (num == 1)
        {
            return "身体的には幼いレミリアを、無理に開発し搾り取っているからだろうか、精子は少なく動きも弱弱しい。";
        }
        else if (num == 2)
        {
            return "精子の質を改善する薬によって強化され、目標を目指して元気に動き回る、一般的な精子になった。";
        }
        else if (num == 3)
        {
            return "より価値のある精液にするために、たくさんのお薬を摂取している。優秀な遺伝子を大量に詰め込み、目を凝らせば肉眼で見えるほどの精子へと変化した。";
        }
        else if (num == 4)
        {
            return "多量の薬物摂取により、体質ごと変化しつつある。金玉が作る精子は、小川にいる小魚のようなサイズへと変貌し、手で持ち上げるとぴちぴち跳ね回るほどだ。";
        }
        else if (num == 5)
        {
            return "金玉は完全に作り変えられ、取り返しがつかなくなっている。レミリアの精子は、鳥の卵ほどの頭を持つ巨大精子へと成長し、金玉の中を泳ぎ回る刺激だけで、レミリアを金玉アクメへと追いやっている。もしもこの精子が、誰かのおまんこに侵入したら、子宮口をこじあけ、卵子を食い尽くし、巨大な頭部に蓄えた遺伝子を使って、レミリアのクローンを孕ませるだろう。";
        }
        else if (num == 6)
        {
            return "";
        }
        else if (num == 7)
        {
            return "";
        }
        else if (num == 8)
        {
            return "";
        }
        else if (num == 9)
        {
            return "";
        }
        else if (num == 10)
        {
            return "";
        }
        else if (num == 11)
        {
            return "";
        }
        else if (num == 12)
        {
            return "";
        }
        else if (num == 13)
        {
            return "";
        }

        return "";
    }
}
