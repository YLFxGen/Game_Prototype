using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnterButtonManager : MonoBehaviour
{
    private Transform target;
    private string LevelName;
    public VectorValue playerStorage;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("PlayerIcon").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive()
    {
        this.gameObject.SetActive(true);
    }

    public void DeActive()
    {
        this.gameObject.SetActive(false);
    }

    public void SetLevelName(string newLevelName)
    {
        LevelName = newLevelName;
    }

    public void EnterLevel()
    {
        if (LevelName != null)
        {
            if (playerStorage != null)
            {
                playerStorage.initialValue = target.transform.position;
            }

            SceneManager.LoadScene(LevelName);
        }
    }
}
