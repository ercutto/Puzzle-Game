using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera _camera = null;
    public Transform player = null;
    
    public Vector3 ofsett;
   
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _camera.transform.position=new Vector3(player.transform.position.x,player.transform.position.y+ofsett.y,ofsett.z);
    }
}
