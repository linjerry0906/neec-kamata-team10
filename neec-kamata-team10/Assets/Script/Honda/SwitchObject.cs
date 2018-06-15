using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作成日 2018/6/9
/// 作成者 本田 尚大
/// 
/// コメント:
///     1flame待機で試してみたらかなりジャッジが不安定だったから
///     安定させる為に多重フレーム判定を入れたかっただけなのに
///     なんでbyteにしてこんな構文書いたんだろ
///   
/// やってること
///     byte型をboolが8個の配列に見立てて
///     上の桁ほど古いデータ、下ほど新しいデータと設定
///     
/// 使い方
///     Player/Enemyが触れればOn 離れればOff
///     コードさえ書けば動く床も作れそう(作る気はない)
/// </summary>

public class SwitchObject : MonoBehaviour {

    public bool IsTurnOn = false;  //Switchの起動状態bool
    private byte Judge;            //動作安定用の8flame待機bool

    Animator animator;             //アニメ制御用
    SpriteRenderer spriteRenderer; //描画色の制御用
    ParticleSystem particleSystem; //パーティクル制御用
    float defaultSpeed;            //デフォルトのアニメ再生速度

    [SerializeField]
    private float delay = 0.5f;    //スイッチ状態の変更時のカラー制御Delay

    float counter = 0f;                                  //delay秒数Counter
    Color colorNow;                                      //counter開始前の描画色
    Color colorOff = new Color(0f, 0f, 0f, 1f); //スイッチoff時のColor
    Color colorOn = new Color(1f, 0.25f, 0.6f, 1f);      //スイッチOn時のColor

    //// Use this for initialization
    void Start()
    {
        //取得する
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();

        defaultSpeed = animator.speed;
        colorNow = colorOff;

        particleSystem.Stop(); //初期状態では停止させておく
    }

    // Update is called once per frame
    void Update()
    {
        ////ジャッジを反映 8flameのどこかでも触れた形跡があったらTrue
        //IsTurnOn = (Judge != 0);

        /// <summary>
        /// ラグが気になるならこちらで設定
        /// 
        /// 対象flame数 1 2 3  4  5  6   7   8
        /// 設定する値   1 3 7 15 31 63 127 255 
        /// 
        /// </summary>
        IsTurnOn = ((Judge & 7) != 0);

        UpdateAnime(); //アニメのUpdate

        UpdateParticle(); //パーティクルのUpdate

        //これからの1flameのジャッジのを入れるために左に1bitシフト
        Judge = (byte)(Judge << 1);
    }

    void OnTriggerStay(Collider other)
    {
        //新たに対象Targetとの接触があれば直前flameに情報を入れる 但し既に入っていたら無視したいのでOR演算
        if (IsColliderTarget(other.tag))
        {
            Judge = (byte)(Judge | 1);
        }
    }

    public bool IsColliderTarget(string tag) //ぶつかっているColliderがtargetかどうか返す
    {
        //現状のSwitch対象はPlayerタグとEnemyタグのみ
        if (tag.Equals("Player")) return true;
        else if (tag.Equals("Enemy")) return true;

        //else
        return false;
    }

    void UpdateAnime()
    {
        animator.speed = (IsTurnOn) ? defaultSpeed : 0f;              //アニメ速度の設定

        Color imagineColor = (IsTurnOn) ? colorOn : colorOff;   //アニメ終了後の色

        if(counter >= delay)
        {   //カウンター:指定時間以上
            spriteRenderer.color = imagineColor; //色の更新
            colorNow = imagineColor;             //現在の色を新しい色に更新
            counter = 0f;                        //カウンターをリセット
        }
        else if(spriteRenderer.color == imagineColor)
        {
            //アニメーション不要 変更なし
            //カウントの必要もない
            return;
        }
        else
        {
            //カウンター:指定時間未満
            spriteRenderer.color = Color.Lerp(colorNow, imagineColor, (counter / delay));

            counter += Time.deltaTime;
        }
    }

    void UpdateParticle()
    {
        if(particleSystem.isPlaying && !IsTurnOn) //Particleは動いているけどスイッチOff
        {
            particleSystem.Stop();   //生成を止めろ
        }
        //上の真逆
        else if(particleSystem.isStopped && IsTurnOn)
        {
            particleSystem.Play();   
        }
    }
}
