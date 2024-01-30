using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform middlePoint;
    [SerializeField] float moveSpeed = .5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.Lerp(transform.position, middlePoint.position, moveSpeed);
    }
}
