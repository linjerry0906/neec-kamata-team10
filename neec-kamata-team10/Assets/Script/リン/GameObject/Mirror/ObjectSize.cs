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
    private Vector3 resize;

    private void Start()
    {
        objSize = transform.localScale;
        resize = new Vector3(1, 1, 1);
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

    public Vector3 GetReflectSize()
    {
        return resize;
    }

    public void SetReflectSize(Vector3 resize)
    {
        this.resize = resize;
    }
}
