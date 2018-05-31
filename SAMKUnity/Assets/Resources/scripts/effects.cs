using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour
{

    [Header("Object effect")]
    [Tooltip("To choose right effect behaviour write one:(Bow/Pulse)")]
    public string EffectType;
    public bool isStar;
    public bool isCountdown;
    public bool isGoLabel;
    static float i = 1;

    int max_i;
    int i_fade;


    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {

        if (EffectType == "Bow")
        {
            if (isCountdown == true)
            {
                max_i = 50;
                i_fade = 50;
            }
                
            else
            {
                max_i = 10;
                i_fade = 50;
            }
                


            if (isGoLabel == true)
            {
                max_i = 1000;
                i_fade = 1000;
            }

            
            if (i < max_i)
            {
                /*
                if (isStar == true)
                {
                    transform.localScale = new Vector2(0.01f * i / 1000, 0.1f * i / 1000);
                }
                else
                {
                    transform.localScale = new Vector2(0.01f * i / 5, 0.1f * i / 5);
                }
                */
                transform.localScale = new Vector2(0.1f * i / 5, 0.1f * i / 5);

            }

            else
            {
                transform.localScale = new Vector2(0.2f, 0.2f);
            }

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 / (i / 13));

            if (i > i_fade)
            {
                Destroy(this.gameObject);
                i = 1;
            }
        }

        else if (EffectType == "Pulse")
        {

            if (i > 100)
            {
                i = 1;
            }
        }

        else
        {

        }

        ++i;
    }
}
