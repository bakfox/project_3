using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class get_broken_setting : MonoBehaviour
{
    public Sprite sp_temp;//È¹µæÇÑ ÀÌ¹ÌÁö
    public int i_temp;//È¹µæ °¹¼ö
    public TextMeshProUGUI text_temp;//ÀÚ½Å text
    float time_max = 1f;
    Vector3 now_postion;//±âº» À§Ä¡ 
    // Start is called before the first frame update
    void Start()
    {
        setting_on();
    }
    void setting_on()
    {
        now_postion= transform.position;
        gameObject.GetComponent<Image>().sprite = sp_temp;
        text_temp.SetText(" + "+i_temp );
        StartCoroutine("up_effect");
    }
    IEnumerator up_effect()
    {
        Image image_temp = gameObject.GetComponent<Image>();
        float time_temp = 0;
        while (time_max > time_temp)
        {
            Debug.Log("time_temp "+ time_temp);
            time_temp =time_temp + Time.deltaTime;
            text_temp.color = new Color(text_temp.color.r, text_temp.color.g, text_temp.color.b, (255f - (time_temp * 255f))/255f);
            image_temp.color = new Color(image_temp.color.r, image_temp.color.g, image_temp.color.b, (255f-(time_temp* 255f))/255f);
            Debug.Log(text_temp.alpha);
            gameObject.transform.position = new Vector3(now_postion.x, now_postion.y+ (time_temp* 255f), now_postion.z);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
