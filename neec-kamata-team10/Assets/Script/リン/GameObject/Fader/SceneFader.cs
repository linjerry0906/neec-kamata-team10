//------------------------------------------------------
// 作成日：2018.4.23
// 作成者：林 佳叡
// 内容：シーンフェーダー（最後にレンダーするカメラに）
//------------------------------------------------------
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    [SerializeField]
    private Material shaderMaterial;                //フェイドShader
    [SerializeField]
    private float speed;                            //変換スピード
    private float fadeFactor = 0.0f;                //Factor

    private StageManager stageManager;              //ステージマネージャー

    private void Start()
    {
        GameManager.Instance.GetController().SetFadeFlag(true);
        stageManager = GameManager.Instance.GetStageManager();  //ステージマネージャーを取得
    }

    private void Update()
    {
        if (!IsEnd())                               //終了してない場合
        {
            Time.timeScale = 0;                     //タイムスケールを静止
            return;
        }

        Time.timeScale = 1;                         //タイムを正常
        stageManager.StartStage();                  //Time計算開始
        Destroy(gameObject.GetComponent<SceneFader>());         //Faderを削除
    }

    /// <summary>
    /// Factorを更新する
    /// </summary>
    private void UpdateShader()
    {
        fadeFactor += speed;                                    //Fadeする
        fadeFactor = Mathf.Clamp(fadeFactor, - 0.1f, 1.1f);     //クランプする
    }

    /// <summary>
    /// 画像処理
    /// </summary>
    /// <param name="source">元画像</param>
    /// <param name="destination">出力先</param>
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        UpdateShader();                                         //Factor更新
        shaderMaterial.SetFloat("_Factor", fadeFactor);         //Shader内の変数設定
        Graphics.Blit(source, destination, shaderMaterial);     //画像処理
    }

    /// <summary>
    /// フェイド終了か
    /// </summary>
    /// <returns></returns>
    private bool IsEnd()
    {
        return fadeFactor >= 1.1f;
    }

    private void OnDestroy()
    {
        GameManager.Instance.GetController().SetFadeFlag(false);
    }
}
