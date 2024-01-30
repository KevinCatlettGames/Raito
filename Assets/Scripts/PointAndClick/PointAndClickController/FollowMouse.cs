
using UnityEngine;

/// <summary>
/// Moves a transform to follow the mouse position;
/// </summary>
public class FollowMouse : MonoBehaviour
{
    [SerializeField] float lerpSpeed;

    #region Methods

    private void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector3.Lerp(transform.position, worldPosition, lerpSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }


    #endregion
}
