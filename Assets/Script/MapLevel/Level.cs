using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public bool canEnter;
    public PlayerLogo target;
    public Collider2D boundary;
    public List<Level> LevelsNearby = new List<Level>();
    public bool LocateNearby = false;
    public MySignal LevelName;
    public int timeCost;

    // Start is called before the first frame update
    void Start()
    {
        if (boundary.bounds.Contains(target.transform.position))
        {
            target.CurrentLevel = this;
            SearchLevelsNearby();
            target.CheckLevel();
        }
        else if(!(boundary.bounds.Contains(target.transform.position)) && !(LocateNearby))
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!(boundary.bounds.Contains(target.transform.position)) && !(LocateNearby))
        {
            for (int i = LevelsNearby.Count - 1; i >= 0; i--)
            {
                LevelsNearby[i].gameObject.SetActive(false);
                LevelsNearby[i].LocateNearby = false;
            }
            target.LeaveLevel();
            this.gameObject.SetActive(false);
        }
    }

    public void SearchLevelsNearby()
    {
        for (int i = LevelsNearby.Count - 1; i >= 0; i--)
        {
            LevelsNearby[i].gameObject.SetActive(true);
            LevelsNearby[i].LocateNearby = true;
        }
    }


    void OnMouseDown()
    {
        if (!target.isMoving)
        {
            target.targetPosition = transform.position;
            target.isMoving = true;
            target.TempLevel = this;

        }
        
    }

}
