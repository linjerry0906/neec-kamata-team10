//------------------------------------------------------
// 作成日：2018.5.18
// 作成者：林 佳叡
// 内容：プレイヤーのアニメーションファクトリー
//------------------------------------------------------
public class PlayerAnimeFactory
{
    public static IAnimeState GetState(EPlayerState state)
    {
        switch (state)
        {
            case EPlayerState.Action:
                return new PlayerAnimeAction();
            case EPlayerState.Jump:
                return new PlayerAnimeJump();
            case EPlayerState.Move:
                return new PlayerAnimeMove();
            default:
                return new PlayerAnimeIdle();
        }
    }
}
