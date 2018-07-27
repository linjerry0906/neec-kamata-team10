//------------------------------------------------------
// 作成日：2018.7.27
// 作成者：林 佳叡
// 内容：ワープする前のエフェクト制御
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour 
{
	[SerializeField]
	private GameObject warpParticlePrefab;		//Prefab
	private GameObject particle;				//一個だけある
	private ICharacterController input;			//入力
	private Timer timer;						//ワープするまでのタイマー
	private AliveFlag aliveFlag;

	void Start () 
	{
		input = GameManager.Instance.GetController();
		aliveFlag = GetComponent<AliveFlag>();
		timer = new Timer(2.0f);
		Initialize();
	}
	
	/// <summary>
	/// 初期化
	/// </summary>
	private void Initialize()
	{
		timer.Initialize();			//タイマー初期化

		if(particle != null)		//パーティクルある場合は削除
		{
			Destroy(particle);
			particle = null;
		}
	}

	void Update () 
	{
		if(aliveFlag.IsDead())		//死んだら実行しない
			return;

		if(!input.Respawn())		//キーが押されない場合
		{
			Initialize();
			return;
		}
		
		timer.TimerUpdate();		//タイマー更新
		if(particle == null)		//パーティクルが作成されてない場合
		{
			particle = Instantiate(warpParticlePrefab, transform.position, Quaternion.identity, transform);
		}
		if(timer.IsTime())			//時間にになったらリセット
		{
			aliveFlag.Dead();
			input.SetFadeFlag(true);
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
}
