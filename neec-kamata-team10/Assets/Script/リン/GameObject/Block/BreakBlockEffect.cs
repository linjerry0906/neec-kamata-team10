//------------------------------------------------------
// 作成日：2018.6.20
// 作成者：林 佳叡
// 内容：Breakされたときのエフェクト
//------------------------------------------------------
using UnityEngine;

public class BreakBlockEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;                    //EffectPrefab

    /// <summary>
    /// 壊れたとこの演出
    /// </summary>
    public void BreakEffect()
    {
        GameObject effect = Instantiate(particle, transform.position, Quaternion.identity);
        float lifeTime = effect.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Debug.Log((effect != null).ToString() + lifeTime);
        Destroy(effect, lifeTime);                  //LifeTimeに合わせて消す
    }
}
