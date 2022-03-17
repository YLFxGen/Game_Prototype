using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateManager : MonoBehaviour
{
    public Date date;
    public Text dayText;
    public Text monthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateDateText()
    {
        dayText.text = "" + date.day;
        monthText.text = date.month;
    }
}
