using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public Collider2D boundary;
    private Transform target; //reference to the player

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((target != null && boundary.bounds.Contains(target.transform.position)) || isPaused == true)
        {
            if (Input.GetButtonDown("shop"))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    pausePanel.SetActive(true);
                    //Time.timeScale = 0f;
                }
                else
                {
                    pausePanel.SetActive(false);
                    //Time.timeScale = 1f;
                }
            }
        }
    }
}
