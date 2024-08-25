using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class protect_statuse : MonoBehaviour, IPointerClickHandler
{

    public mainmanager main_temp;

    public Sprite[] protect_sprite;
    public Sprite[] protect_efect;

    public SpriteRenderer efect_image;
    public Image image_temp;
    
    public void check_img()
    {
        if (main_temp.protect == true)
        {
            image_temp.sprite = protect_sprite[1];
            efect_image.sprite = protect_efect[1];
        }
        else
        {
            image_temp.sprite = protect_sprite[0];
            efect_image.sprite = protect_efect[0];
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (main_temp.save_temp.save_data.user_now_have_protect > 0)
        {
            if (main_temp.protect == false)
            {
                main_temp.protect = true;
                check_img();
            }
            else
            {
                main_temp.protect = false;
                check_img();

            }
        }
        
        

    }

}
