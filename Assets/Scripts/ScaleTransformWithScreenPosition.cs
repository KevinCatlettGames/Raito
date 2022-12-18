using UnityEngine;

public class ScaleTransformWithScreenPosition : MonoBehaviour
{
    [SerializeField] float xScaleFactor;
    [SerializeField] float yScaleFactor;

    float originalXScale;
    float originalYScale;

    public SpriteRenderer spriteRen;

    private void Start()
    {
        originalXScale = transform.localScale.x;
        originalYScale = transform.localScale.y;
    }
    // Update is called once per frame
    void LateUpdate()
    {


        if (transform.localScale.x > 0 )
        {
            // scale rightway
            float xScaling = originalXScale - (transform.position.y / xScaleFactor);
            float yScaling = originalYScale - (transform.position.y / yScaleFactor);

            transform.localScale = new Vector2(xScaling, yScaling);
        }

        else if (transform.localScale.x < 0)
        {
            // scale leftway
            float xScaling = originalXScale - (transform.position.y / xScaleFactor);
            float yScaling = originalYScale - (transform.position.y / yScaleFactor);

            transform.localScale = new Vector2(-xScaling, yScaling);
        }
    }
}
