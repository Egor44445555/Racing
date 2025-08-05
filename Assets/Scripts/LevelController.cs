using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController main;
    [SerializeField] GameObject successPopup;

    int maxCircles = 2;
    int currentCircle = 0;

    void Awake()
    {
        main = this;
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
            TrajectoryController.main.StartRecordPath();
        }

        if (currentCircle == 2)
        {
            TrajectoryController.main.CreateGhostCar();
        }
        
        if (currentCircle > maxCircles)
        {
            GameOver();
        }
    }
}
