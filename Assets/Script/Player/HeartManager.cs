using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite threeQuarterHeart;
    public Sprite halfFullHeart;
    public Sprite oneQuarterHeart;
    public Sprite emptyHeart;
    public FloatVaule heartContainers;
    public FloatVaule playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
        UpdateHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].sprite = fullHeart;
            hearts[i].gameObject.SetActive(true);
            
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 4;
        if (tempHealth >= 0 )
        {
            for (int i = 1; i <= heartContainers.RuntimeValue; i++)
            {
                if (i <= tempHealth)
                {
                    //full heart
                    hearts[i-1].sprite = fullHeart;
                }
                else if (i > tempHealth && (i - 0.25) <= tempHealth)
                {
                    //three quarter heart
                    hearts[i-1].sprite = threeQuarterHeart;
                }
                else if (i > tempHealth && (i - 0.5) <= tempHealth)
                {
                    //half full heart
                    hearts[i-1].sprite = halfFullHeart;
                }
                else if (i > tempHealth && (i - 0.75) <= tempHealth)
                {
                    //one quarter heart
                    hearts[i-1].sprite = oneQuarterHeart;
                }
                else
                {
                    //empty heart
                    hearts[i-1].sprite = emptyHeart;
                }
            }
        }
        
    }
    
}
