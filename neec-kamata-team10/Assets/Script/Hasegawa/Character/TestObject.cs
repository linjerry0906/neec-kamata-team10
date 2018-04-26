using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour {
    [SerializeField]
    private Mirror mirror;
    [SerializeField]
    private float changeTime;

    private ChangeScale changeScale;

    // Use this for initialization
    void Start()
    {
        changeScale = new ChangeScale(mirror, changeTime);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeScale();
    }

    void ChangeScale()
    {
        transform.localScale = changeScale.Scale(new Vector2(transform.position.x, transform.position.y));
    }

    void OnTriggerEnter(Collider t)
    {
        if (t.gameObject.CompareTag("mirror"))
        {
            changeScale.SetMirror(t.gameObject.GetComponent<Mirror>());
            //t.gameObject.GetComponent<Mirror>().
        }
    }
}
