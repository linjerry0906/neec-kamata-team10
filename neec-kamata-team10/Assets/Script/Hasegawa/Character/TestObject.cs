using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour {
    [SerializeField]
    private float changeTime;

    private ChangeScale changeScale;

    // Use this for initialization
    void Start()
    {
        changeScale = new ChangeScale(new Vector3(1, 1, 1), changeTime);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScale();
    }

    void ChangeScale()
    {
        if (tag == "reflect")
            return;
        SizeEnum size = GetComponent<ObjectSize>().GetSize();
        transform.localScale = changeScale.Scale(size/*new Vector2(transform.position.x, transform.position.y)*/);
    }

    void OnTriggerEnter(Collider t)
    {
        if (t.gameObject.CompareTag("mirror"))
        {
            changeScale.SetMirrorSize(t.gameObject.GetComponent<Mirror>().ReflectSize());
        }
    }
}