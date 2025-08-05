using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController main;
    [SerializeField] GameObject successPopup;

    int maxCircles = 2;
    int currentCircle = 0;
    TrajectoryController trajectoryController;

    void Awake()
    {
        main = this;
        trajectoryController = TrajectoryController.main;
    }

    public void GameOver()
    {
        successPopup.SetActive(true);
    }

    public int GetMaxCircles()
    {
        return maxCircles;
    }

    public int GetCurrentCircle()
    {
        return currentCircle;
    }

    public void IncreaseCircleCount()
    {
        currentCircle++;
        UIController.main.CheckCount(currentCircle);

        if (currentCircle == 1)
        {
            trajectoryController.StartRecordPath();
        }

        if (currentCircle == 2)
        {
            trajectoryController.CreateGhostCar();
        }
        
        if (currentCircle > maxCircles)
        {
            GameOver();
        }
    }
}
