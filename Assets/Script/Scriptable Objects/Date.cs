using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Date : ScriptableObject
{

    public string month = "January";
    public int day = 1;
    private List<string> monthName = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
    public int monthNumber = 0;
    public MySignal dateSignal;

    public void UpdateDate(int timeToAdd)
    {
        day += timeToAdd;
        if (day > 30)
        {
            monthNumber++;
            if (monthNumber > 11)
            {
                monthNumber = 0;
            }
            day -= 30;
            month = monthName[monthNumber];
        }
        dateSignal.Raise();
    }
}
