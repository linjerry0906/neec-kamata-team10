//------------------------------------------------------
// 作成日：2018.5.18
// 作成者：林 佳叡
// 内容：プレイヤーのアニメーションを制御
//------------------------------------------------------
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    private Animator animator;                              //UnityのAnimator
    private IAnimeState currentState;                       //現在のステート
    private EPlayerState currentStateEnum;
    private EPlayerState previousStateEnum;

    void Start ()
    {
        animator = GetComponent<Animator>();                //Animatorを取得
        currentState = PlayerAnimeFactory.GetState(EPlayerState.Stay);

        currentStateEnum = EPlayerState.Stay;
        previousStateEnum = EPlayerState.Stay;
    }

    private void Update()
    {
        currentState.Update();                              //更新

        if (currentState.IsEnd())                           //終わったら
            currentState.Exit(animator);                    //終了処理
    }

    public void ChangeState(EPlayerState state)
    {
        currentState.Exit(animator);                        //前のステートの終了処理

        currentState = PlayerAnimeFactory.GetState(state);  //ステートを取得
        currentState.Execute(animator);                     //ステートを実行

        previousStateEnum = currentStateEnum;
        currentStateEnum = state;
    }
}
