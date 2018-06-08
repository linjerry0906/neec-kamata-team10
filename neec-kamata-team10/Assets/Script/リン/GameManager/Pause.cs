//------------------------------------------------------
// 作成日：2018.6.08
// 作成者：林 佳叡
// 内容：PauseScene
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private GameManager gameManager;
    private ICharacterController controller;

    void Start()
    {
        gameManager = GameManager.Instance;
        controller = gameManager.GetController();
        controller.SetFadeFlag(true);
    }
	
	void Update ()
    {
        if (!controller.SwitchToTheLeft())
            return;

        controller.SetFadeFlag(false);
        gameManager.Return();
	}
}
