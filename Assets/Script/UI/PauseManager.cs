using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public bool isMap;
    public GameObject pausePanel;
    public StringValue previousLevelName;
    private Transform target;
    public VectorValue playerStorage;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        if (isMap)
        {
            target = GameObject.FindWithTag("PlayerIcon").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pausePanel != null)
        {
            if (Input.GetButtonDown("pause"))
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

    public void LeaveLevel()
    {
        if (target != null && playerStorage != null)
        {
            playerStorage.initialValue = target.transform.position;
        }


        if (previousLevelName != null)
        {
            SceneManager.LoadScene(previousLevelName.initialValue);
        }
        else
        {
            SceneManager.LoadScene("WorldMap");
        }
        
    }
}
