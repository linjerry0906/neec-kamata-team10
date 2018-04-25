//------------------------------------------------------
// 作成日：2018.4.25
// 作成者：林 佳叡
// 内容：鏡を設置する機能
//------------------------------------------------------
using System.Collections;
using UnityEngine;

public class MirrorSetting : MonoBehaviour
{
    private readonly static Vector2 MIRROR_SIZE = new Vector2Int(5, 4);
    private readonly static int INTERVAL_MASS = 2;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] mirrors;
    private int currentMirror;

    private Queue usedMirrors;
    private ICharacterController controller;

	void Start ()
    {
        controller = GameManager.Instance.GetController(EController.KEYBOARD);
        usedMirrors = new Queue();
        currentMirror = 0;
	}
	
	void Update ()
    {
        ChangeMirror();
        SetMirror();
	}

    private void ChangeMirror()
    {
        int amount = mirrors.Length;
        if (controller.SwitchToTheLeft())
            currentMirror--;
        if (controller.SwitchToTheRight())
            currentMirror++;
#region Index Clamp
        if (currentMirror >= amount)
            currentMirror %= amount;
        if(currentMirror < 0)
        {
            currentMirror %= amount;
            currentMirror += amount;
        }
#endregion
    }

    private void SetMirror()
    {
        if (!controller.OperateTheMirror())
            return;

        Vector3 pos = player.transform.position;
        pos.z = 0.1f;
        GameObject newMirror = Instantiate(mirrors[currentMirror], pos, Quaternion.identity);
        if (!CheckMirrorPos(newMirror))
        {
            Destroy(newMirror);
            return;
        }
        usedMirrors.Enqueue(newMirror);
        RemoveExpiredMirror();
    }


    private bool CheckMirrorPos(GameObject newMirror)
    {
        foreach(GameObject mirror in usedMirrors.ToArray())
        {
            Vector3 diff = newMirror.transform.position - mirror.transform.position;
            int diffX = (int)Mathf.Abs(diff.x);
            int diffY = (int)Mathf.Abs(diff.y);
            if (diffX < MIRROR_SIZE.x + INTERVAL_MASS &&
                diffY < MIRROR_SIZE.y + INTERVAL_MASS)
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveExpiredMirror()
    {
        if (usedMirrors.Count <= 3)
            return;

        GameObject expired = usedMirrors.Dequeue() as GameObject;
        Destroy(expired);
    }
}
