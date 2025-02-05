using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void LateUpdate()
    {
        PlayerInput playerInstance = PlayerInput.Instance;
        MainCamera.transform.position = new Vector3(playerInstance.transform.position.x, playerInstance.transform.position.y, -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
