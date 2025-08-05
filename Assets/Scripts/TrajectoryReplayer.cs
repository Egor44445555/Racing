using UnityEngine;

public class TrajectoryReplayer : MonoBehaviour
{
    TrajectoryPoint[] trajectoryPoints;
    int currentPointIndex = 0;
    float replayStartTime;
    bool isReplaying = false;

    public void InitializeReplay(TrajectoryPoint[] points)
    {
        if (points == null || points.Length == 0)
        {
            Debug.LogError("Cannot replay - no trajectory points");
            return;
        }

        trajectoryPoints = points;
        currentPointIndex = 0;
        replayStartTime = Time.time;
        isReplaying = true;
        Debug.Log($"Replay initialized with {points.Length} points");
    }

    void Update()
    {
        if (!isReplaying || trajectoryPoints == null) return;

        float currentTime = Time.time - replayStartTime;
        
        // Find the current trajectory point
        while (currentPointIndex < trajectoryPoints.Length - 1 && trajectoryPoints[currentPointIndex + 1].time <= currentTime)
        {
            currentPointIndex++;
        }

        if (currentPointIndex < trajectoryPoints.Length)
        {
            transform.position = trajectoryPoints[currentPointIndex].position;
            transform.rotation = trajectoryPoints[currentPointIndex].Rotation;
        }
        else
        {
            isReplaying = false;
            Debug.Log("Replay finished");
        }
    }

    public void StartReplay(TrajectoryPoint[] points)
    {
        if (points == null || points.Length == 0)
        {
            Debug.LogError("No trajectory points to replay");
            return;
        }

        trajectoryPoints = points;
        currentPointIndex = 0;
        replayStartTime = Time.time;
        isReplaying = true;
    }
}