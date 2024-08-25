using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class broken_swoad_manager : MonoBehaviour , IPointerClickHandler
{
    public int now_id;//��ȯ������ ���� 

    public GameObject protect_ui;//
    public GameObject broken_obj;//�η����� ���� ������Ʈ
    public GameObject canver_obj;

    public Sprite[] broken_sprite_temp;

    public save_sc save_temp;
    public mainmanager main_temp;

    public void protect_ui_on()
    {
        main_temp.check_have_broken_item();
        protect_ui.SetActive(true);
    }
    public void protect_ui_off()
    {
        protect_ui.SetActive(false);
    }
    public void get_swoad_broken()//���ū ��� ����
    {
        if (save_temp.save_data.user_now_swoad >= 15)
        {
            int i_temp = 0;//�ı��� �߰� ��� üũ
            int i_temp_2 = 0;//�ı��� ���� Ȯ�� üũ
            int i_temp_3 = 0;//üũ�� 

            i_temp_2 = random_sc.random_gacha(20, 80);

            if (i_temp_2 == 2)
            {
                i_temp = random_sc.random_gacha(70, 20);
                if (save_temp.save_data.user_now_swoad >= 15 && save_temp.save_data.user_now_swoad < 20)
                {
                    save_temp.save_data.user_sowd_broken_have[0] += i_temp;
                    i_temp_3 = 0;
                }
                else if (save_temp.save_data.user_now_swoad >= 20 && save_temp.save_data.user_now_swoad < 25)
                {
                    save_temp.save_data.user_sowd_broken_have[1] += i_temp;
                    i_temp_3 = 1;
                }
                else if (save_temp.save_data.user_now_swoad >= 25 && save_temp.save_data.user_now_swoad < 30)
                {
                    save_temp.save_data.user_sowd_broken_have[2] += i_temp;
                    i_temp_3 = 2;
                }

                GameObject effect_temp = Instantiate(broken_obj, canver_obj.transform);
                effect_temp.GetComponent<get_broken_setting>().sp_temp = broken_sprite_temp[i_temp_3];
                effect_temp.GetComponent<get_broken_setting>().i_temp = i_temp;
            }
        }
        
    }
    public void OnPointerClick(PointerEventData eventData)//Ŭ�������� 
    {
        switch (now_id)
        {
            case 0:
                if (save_temp.save_data.user_sowd_broken_have[0] <= 5)
                {
                    save_temp.save_data.user_sowd_broken_have[0] -= 5;
                    save_temp.save_data.user_now_have_protect++;
                    main_temp.check_have_broken_item();
                    save_temp.Save();
                }
                break;
            case 1:
                if (save_temp.save_data.user_sowd_broken_have[1] <= 3)
                {
                    save_temp.save_data.user_sowd_broken_have[1] -= 3;
                    save_temp.save_data.user_now_have_protect++;
                    main_temp.check_have_broken_item();
                    save_temp.Save();
                }
                break;
            case 2:
                if (save_temp.save_data.user_sowd_broken_have[2] <= 1)
                {
                    save_temp.save_data.user_sowd_broken_have[2] -= 1;
                    save_temp.save_data.user_now_have_protect++;
                    main_temp.check_have_broken_item();
                    save_temp.Save();
                }
                break;

        }
    }
}
