using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSize : MonoBehaviour
{
    private SizeEnum size = SizeEnum.Normal;

    public void SetSize(SizeEnum size)
    {
        this.size = size;
    }

    public SizeEnum GetSize()
    {
        return size;
    }
}
