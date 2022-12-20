using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    MousePosition mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = MousePosition.instance;
    }

    // Update is called once per frame
    void Update()
    {     
        transform.position = mousePosition.ScreenMousePosition();
    }

}
