//------------------------------------------------------
// 作成日：2018.5.28
// 作成者：林 佳叡
// 内容：クリア時に選択できる項目
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSelect : MonoBehaviour
{
    private enum ESelection
    {
        StageSelect = 0,
        TryAgain,
    }

    [SerializeField]
    private Vector3 maxButtonScale = new Vector3(1.2f, 1.2f, 1);

    [SerializeField]
    private GameObject selectButton;
    [SerializeField]
    private GameObject tryButton;

    private ICharacterController controller;
    private ESelection currentSelect;

    private void Start()
    {
        controller = GameManager.Instance.GetController();
        currentSelect = ESelection.TryAgain;
    }

    private void Update()
    {
        Choose();

        AnimateButton();

        Select();
    }

    private void Choose()
    {
        Vector3 move = controller.VerticalMove();
        if (move.y == 0)
            return;

        if (move.y > 0)
        {
            currentSelect = ESelection.StageSelect;
        }
        else
        {
            currentSelect = ESelection.TryAgain;
        }
    }

    private void AnimateButton()
    {
        if (currentSelect == ESelection.StageSelect)
        {
            SetScale(ref selectButton, ref tryButton);
            return;
        }
        SetScale(ref tryButton, ref selectButton);
    }

    private void SetScale(ref GameObject large, ref GameObject small)
    {
        large.transform.localScale = maxButtonScale;
        small.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Select()
    {
        if (!controller.Jump())
            return;

        if (currentSelect == ESelection.TryAgain)
        {
            GameManager.Instance.TrySameStage();
            return;
        }
        GameManager.Instance.ChangeScene(EScene.StageSelect);
    }
}
