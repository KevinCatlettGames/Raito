using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightFlicker : MonoBehaviour
{
    [SerializeField] Light2D myLight;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;
    bool dim;
    [SerializeField] float dimSpeed;

    private void Start()
    {
        if(!myLight && GetComponent<Light2D>())
        {
            myLight = GetComponent<Light2D>();
        }
    }
    // Update is called once per frame
    void Update()
    {
       if(dim)
        {
            myLight.intensity -= dimSpeed * Time.deltaTime;
            if(myLight.intensity <= minIntensity)
            {
                dim = false;
            }
        }

       else if(!dim)
        {
            myLight.intensity += dimSpeed * Time.deltaTime;
            if (myLight.intensity >= maxIntensity)
            {
                dim = true;
            }
        }
    }
}
