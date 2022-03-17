using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogo : MonoBehaviour
{
    private float speed = 3;
    public Vector3 targetPosition;
    public Level TempLevel;
    public Level CurrentLevel;
    public bool isMoving = false;
    public MySignal enterLevelSignal;
    public MySignal exitLevelSignal;
    public VectorValue startingPosition;
    public Date date;

    // Start is called before the first frame update
    void Start()
    {
        if (startingPosition != null)
        {
            transform.position = startingPosition.initialValue;
        } 
    }

    private void OnEnable()
    {
        if (startingPosition != null)
        {
            transform.position = startingPosition.initialValue;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
        }
    }


    public void Move()
    {
        targetPosition.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            CurrentLevel = TempLevel;
            CheckLevel();
            isMoving = false;

            CurrentLevel.gameObject.SetActive(true);
            CurrentLevel.SearchLevelsNearby();

            date.UpdateDate(CurrentLevel.timeCost);
        }
    }

    public void CheckLevel()
    {
        if (CurrentLevel != null)
        {
            if (CurrentLevel.canEnter)
            {
                enterLevelSignal.Raise();
                if (CurrentLevel.LevelName != null)
                {
                    CurrentLevel.LevelName.Raise();
                }
            }
        }
    }

    public void LeaveLevel()
    {
        if (CurrentLevel != null)
        {
            if (CurrentLevel.canEnter)
            {
                exitLevelSignal.Raise();
            }
        }
    }
}
