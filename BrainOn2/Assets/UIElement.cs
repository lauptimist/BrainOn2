using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject target;

    private bool mouse_over = false;
     void Update()
     {
         if (mouse_over)
         {
            target.SetActive(true);
        }
        else
        {
            target.SetActive(false);
        }
     }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
         mouse_over = true;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         mouse_over = false;
     }




}
