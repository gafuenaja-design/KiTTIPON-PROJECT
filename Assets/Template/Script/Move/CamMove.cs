using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    private float speed = 10f;
    private float zoomSpeed = 5f;
    private Vector3 move;
    

    

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _cam.orthographicSize -= scroll * zoomSpeed;
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, 2f, 20f);

        
    }

    void FixedUpdate()
    {
        transform.position += move * speed * Time.fixedDeltaTime;
    }
}
