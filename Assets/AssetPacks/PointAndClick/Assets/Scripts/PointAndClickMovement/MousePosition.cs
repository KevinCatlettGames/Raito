using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    #region Variables
    public static MousePosition instance;

    #endregion

    #region Awake
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    #endregion

    #region Methods
    /// <summary>
    /// Returns the current mouse position in screen space.
    /// </summary>
    public Vector3 ScreenMousePosition()
    {
        Vector3 screenMousePosition = Input.mousePosition;
        return screenMousePosition;
    }

    /// <summary>
    /// Returns the current mouse position in world space.
    /// </summary>
    public Vector3 WorldMousePosition()
    {
        Vector3 screenMousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
        return worldMousePosition;
    }

    /// <summary>
    /// Performs a raycast at the mouse position and returns the gameobject located there if there is one.
    /// </summary>
    /// <returns></returns>
    public GameObject ObjectAtMousePosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(WorldMousePosition(), Vector2.zero);
        {
            if(!hit)
            {
                return null;
            }
            else
            {
                return hit.transform.gameObject;
            } 
           
        }
    }

    #endregion

}
