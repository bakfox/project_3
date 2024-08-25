using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;
using static UnityEngine.UI.CanvasScaler;

public class mainmanager : MonoBehaviour
{
    public bool protect = false;//보호권 사용

    public save_sc save_temp;
    public swod_data_base swod_data_base_temp;
    public swod_data now_swod_temp;
    public protect_statuse protect_temp;

    public broken_swoad_manager broken_temp;

    public TextMeshProUGUI succes_ui;
    public TextMeshProUGUI failed_ui;
    int succes_int = 90;
    int failed_int = 10;

    public SpriteRenderer swod_img;
    public TextMeshProUGUI swoad_atck_ui;
    public TextMeshProUGUI swod_name_text;
    public TextMeshProUGUI[] have_broken_swoad;//부서진 아이템 text
    public TextMeshProUGUI protect_text;

    public TextMeshProUGUI gold_ui;
    public TextMeshProUGUI upgrade_gold_ui;
    public TextMeshProUGUI sell_gold_ui;

    public int upgrade_nead_gold;
    public int sell_swoad_gold;//팔때 금액

    private void Start()
    {
        save_temp = this.GetComponent<save_sc>();
        check_now_sowd();
    }

    public void upgrade()
    {
        if (save_temp.save_data.user_now_swoad != 29)
        {
            if (protect)
            {
                if (save_temp.save_data.user_now_gold >= upgrade_nead_gold)
                {
                    
                    int i_temp = random_sc.random_gacha(succes_int, failed_int);
                    save_temp.save_data.user_now_gold -= upgrade_nead_gold;
                    if (i_temp == 1)
                    {
                        save_temp.save_data.user_now_swoad++;
                        save_temp.save_data.user_now_have_protect--;
                        protect = false;
                        check_now_sowd();
                        save_temp.Save();
                        return;
                    }
                    else if (i_temp == 2)
                    {
                        check_now_sowd();
                        save_temp.save_data.user_now_have_protect--;
                        protect = false;
                        save_temp.Save();
                        return;
                    }
                    else
                    {
                        check_now_sowd();
                        save_temp.save_data.user_now_have_protect--;
                        protect = false;
                        save_temp.Save();
                        return;
                    }
                }
            }
            else
            {
                if (save_temp.save_data.user_now_gold >= upgrade_nead_gold)
                {
                    int i_temp = random_sc.random_gacha(succes_int, failed_int);
                    save_temp.save_data.user_now_gold -= upgrade_nead_gold;
                    if (i_temp == 1)
                    {
                        save_temp.save_data.user_now_swoad++;
                        check_now_sowd();
                        save_temp.Save();
                        return;
                    }
                    else if (i_temp == 2)
                    {
                        check_now_sowd();
                        save_temp.Save();
                        return;
                    }
                    else
                    {
                        broken_temp.get_swoad_broken();
                        save_temp.save_data.user_now_swoad = 0;
                        check_now_sowd();
                        save_temp.Save();
                        return;
                    }
                }
            }
        }

        
    }
    public int level_upgrade()
    {
        int i_temp = 0;

        return i_temp;
    }
    public void check_now_sowd()
    {
        check_upgrade_int();
        now_swod_temp = swod_data_base_temp.return_find_item_data(save_temp.save_data.user_now_swoad);
        return_nead_gold();

        succes_ui.SetText(succes_int + "%");
        failed_ui.SetText(failed_int + "%");

        swod_img.sprite = now_swod_temp.sprite_img;
        swoad_atck_ui.SetText(change_unit(now_swod_temp.atck_dmg.ToString()));
        swod_name_text.SetText(now_swod_temp.item_name);
        protect_text.SetText(change_unit(save_temp.save_data.user_now_have_protect.ToString()));
        sell_gold_ui.SetText(change_unit(sell_swoad_gold.ToString()) + " G");
        gold_ui.SetText(change_unit(save_temp.save_data.user_now_gold.ToString()) + " G");

        protect_temp.check_img();

        if (save_temp.save_data.user_now_swoad == 29)
        {
            upgrade_gold_ui.SetText("현재 마지막 검입니다");
        }
        else
            upgrade_gold_ui.SetText(change_unit(upgrade_nead_gold.ToString()) + " G");
    }
    public void check_have_broken_item()//보호권 열때 초기화
    {
        have_broken_swoad[0].SetText(save_temp.save_data.user_sowd_broken_have[0]+" 개");
        have_broken_swoad[1].SetText(save_temp.save_data.user_sowd_broken_have[1] + " 개");
        have_broken_swoad[2].SetText(save_temp.save_data.user_sowd_broken_have[2] + " 개");
    }
    public void check_upgrade_int()//30이 최대 
    {
        if (save_temp.save_data.user_now_swoad < 5)
        {
            succes_int = 90;
            failed_int = 10;
            return;
        }
        else if (save_temp.save_data.user_now_swoad <= 10)
        {
            succes_int = 75;
            failed_int = 25;
            return;
        }
        else if (save_temp.save_data.user_now_swoad <= 15)
        {
            succes_int = 50;
            failed_int = 40;
            return;
        }
        else if (save_temp.save_data.user_now_swoad <= 20)
        {
            succes_int = 50;
            failed_int = 10;
            return;
        }
        else if (save_temp.save_data.user_now_swoad <= 25)
        {
            succes_int = 30;
            failed_int = 10;
            return;
        }
        else if (save_temp.save_data.user_now_swoad <= 30)
        {
            succes_int = 10;
            failed_int = 50;
            return;
        }
    }
    
    public void return_nead_gold()//필요한 골드랑 파는 골드 구하는 공식
    {
        if (save_temp.save_data.user_now_swoad == 0)
        {
            upgrade_nead_gold = 0;
            sell_swoad_gold = 0;
        }
        if (save_temp.save_data.user_now_swoad > 0)
        {
            upgrade_nead_gold = 100 * save_temp.save_data.user_now_swoad * save_temp.save_data.user_now_swoad;//업그레이드 비용 
            if (save_temp.save_data.user_now_swoad >= 15)
            {
                sell_swoad_gold = 100 * save_temp.save_data.user_now_swoad * save_temp.save_data.user_now_swoad * 5 * save_temp.save_data.user_now_swoad;
            }
            sell_swoad_gold = 100 * save_temp.save_data.user_now_swoad * save_temp.save_data.user_now_swoad * 5;//파는거 5배로
        }
    }
    public void sell_swoad_btn()
    {
        save_temp.save_data.user_now_gold += sell_swoad_gold;
        save_temp.save_data.user_now_swoad = 0;
        save_temp.Save();
        check_now_sowd();
    }

    public string change_unit(string i_string)//숫자 변환용 
    {
        char[] unit_a = new char[3] { 'K', 'M', 'B' };
        int unit = 0;

        while (i_string.Length > 6)
        {
            unit++;
            i_string = i_string.Substring(0, i_string.Length - 3);
        }
        if (i_string.Length > 3)
        {
            int I_temp = int.Parse(i_string);
            if (i_string.Length > 4)
            {
                return (I_temp / 1000).ToString() + unit_a[unit];
            }
            else
            {
                return (I_temp / 1000f).ToString("0.0") + unit_a[unit];
            }
        }
        else
        {
            int I_temp = int.Parse(i_string);
            return (I_temp).ToString();
        }
    }
}
