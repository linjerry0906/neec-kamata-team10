using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlockScript : MonoBehaviour
{

    [SerializeField]
    int breakMass;
    [SerializeField]
    float breakTime;
    float time;
    bool trigger;
    Player player;

    // Use this for initialization
    void Start()
    {
        time = breakTime;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                GetComponent<BreakBlockEffect>().BreakEffect();
                Destroy(transform.parent.gameObject);
            }
        }
    }


    //他のコライダと接触した時
    void OnTriggerStay(Collider col)
    {
        //既に接触が始まった場合->無視する
        if (trigger) return;

        if (col.gameObject.CompareTag("Player"))
        {
            player = col.GetComponent<Player>();
            trigger = IsMass(col);
        }
        else if (IsTrueTag(col.gameObject.tag))
        {
            trigger = IsMass(col);
        }
    }

    private void OnDestroy()
    {
        //Playerが触れた場合
        if (player != null)
        {
            player.SetPlayerState(EPlayerState.Jump);
            player.SetIsJump(true);
        }
    }

    //6.15 本田 変更:Enemy,MagicBlock,Splinterでも破壊される
    private bool IsTrueTag(string tag)
    {
        if (tag.Equals("Enemy")) return true;
        else if (tag.Equals("magic_block")) return true;
        else if (tag.Equals("Splinter")) return true;

        return false;
    }

    private bool IsMass(Collider col)
    {
        Rigidbody rigidbody = col.GetComponent<Rigidbody>();
        return (breakMass <= rigidbody.mass);
    }
}
