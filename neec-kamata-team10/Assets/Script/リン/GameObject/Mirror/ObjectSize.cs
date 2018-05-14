//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：サイズを管理するクラス
//------------------------------------------------------
using UnityEngine;

public class ObjectSize : MonoBehaviour
{
    [SerializeField]
    private SizeEnum size = SizeEnum.Normal;

    private Vector3 objSize;

    private void Start()
    {
        objSize = transform.localScale;
    }

    public void SetSize(SizeEnum size)
    {
        this.size = size;
    }

    public SizeEnum GetSize()
    {
        return size;
    }

    public Vector3 DefaultSize()
    {
        return objSize;
    }
}
