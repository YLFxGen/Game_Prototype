using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public FloatVaule coinNumber;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "" + coinNumber.RuntimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoinText()
    {
        coinText.text = "" + coinNumber.RuntimeValue;
    }
}
