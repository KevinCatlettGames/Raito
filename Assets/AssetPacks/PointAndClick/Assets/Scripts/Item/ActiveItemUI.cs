using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemUI : MonoBehaviour
{

    public static ActiveItemUI instance;

    [SerializeField]
    Image image;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        image.enabled = false;
    }
    public void ActivateActiveItemImage(Sprite activeItemSprite)
    {
        image.sprite = activeItemSprite;
        image.enabled = true;
    }

    public void DeactivateActiveItemImage()
    {
        image.sprite = null;
        image.enabled = false;
    }
}
