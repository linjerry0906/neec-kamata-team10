using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    [SerializeField]
    private GameObject cameraTarget;
    private Vector2 viewPort;
    [SerializeField]
    private float cameraSpeed = 0.1f;
    [SerializeField]
    private float targetRadius;
    //Vector3.Lerp(ベクトル0,ベクトル1,0と1の間の割合)
    Ray ray;

    // Use this for initialization
    void Start () {
        viewPort = Camera.main.pixelRect.center;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = CameraMove();
        float length = velocity.magnitude;
        if (length < targetRadius) return;
        if (length != 0)
            velocity /= length;
        Camera.main.transform.position += velocity * length * cameraSpeed;
	}

    
    Vector3 CameraMove(){
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.z = 0;
        Vector3 targetPosition = cameraTarget.transform.position;
        targetPosition.z = 0;
        return targetPosition - cameraPosition;
    }
}
