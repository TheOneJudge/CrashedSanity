using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollScript : MonoBehaviour
{

    [Header("Scroll Rect")]

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform vpTransform;
    [SerializeField] private RectTransform cpTransform;

    [SerializeField] private HorizontalLayoutGroup HLG;

    [SerializeField] private RectTransform[] Items;

    // Start is called before the first frame update
    void Start()
    {
        int ItemsToAdd = Mathf.CeilToInt(vpTransform.rect.width / (Items[0].rect.width + HLG.spacing));

        for (int i = 0; i < ItemsToAdd; i++)
        {
            RectTransform RT = Instantiate(Items[i % Items.Length], cpTransform);
            RT.SetAsLastSibling();
        }

        for (int i = 0; i < ItemsToAdd; i++)
        {
            int num = Items.Length - i - 1;
            while (num < 0)
            {
                num += Items.Length;
            }
            RectTransform RT = Instantiate(Items[num], cpTransform);
            RT.SetAsFirstSibling();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
