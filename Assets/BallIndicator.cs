using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIndicator : MonoBehaviour {

    public GameObject frontBall;
    public GameObject backBall;
    public GameObject rightBall;
    public GameObject leftBall;

    // The direction of the ball to display
    private Direction ballDirection;

    // The previous direction of the ball (to avoid re-rendering)
    private Direction previousBallDirection;

    // Control whether to display this indicator
    private bool isDisplayed;

    // Previous state of control (to avoid re-rendering)
    private bool previousIsDisplayed;

	// Use this for initialization
	void Start () {
        HideAllBalls();
        previousBallDirection = ballDirection;
        previousIsDisplayed = isDisplayed;

    }
	
	// Update is called once per frame
	void Update () {
        if (previousIsDisplayed != isDisplayed ||
            previousBallDirection != ballDirection)
        {
            HideAllBalls();
            if (isDisplayed)
            {
                ShowBall(ballDirection);
            } else
            {
                HideAllBalls();
            }
            previousIsDisplayed = isDisplayed;
            previousBallDirection = ballDirection;
        } 
	}

    public void HideIndicator()
    {
        isDisplayed = false;
        ballDirection = Direction.NONE;
    }

    public void ShowIndicator(Direction direction)
    {
        isDisplayed = true;
        ballDirection = direction;
    }

    void HideAllBalls()
    {
        frontBall.SetActive(false);
        backBall.SetActive(false);
        rightBall.SetActive(false);
        leftBall.SetActive(false);
    }

    void ShowBall(Direction direction)
    {
        switch (direction)
        {
            case Direction.FRONT:
                frontBall.SetActive(true);
                break;
            case Direction.RIGHT:
                rightBall.SetActive(true);
                break;
            case Direction.BACK:
                backBall.SetActive(true);
                break;
            case Direction.LEFT:
                leftBall.SetActive(true);
                break;
            case Direction.NONE:
                break;
        }
    }
}
