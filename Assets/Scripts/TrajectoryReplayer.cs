using UnityEngine;

public class TrajectoryReplayer : MonoBehaviour
{
    TrajectoryPoint[] trajectoryPoints;
    int currentPointIndex = 0;
    float replayStartTime;
    bool isReplaying = false;

    void Update()
    {
        if (!isReplaying || trajectoryPoints == null || trajectoryPoints.Length == 0)
            return;

        float currentTime = Time.time - replayStartTime;

        // Protection against array overflow
        if (currentPointIndex >= trajectoryPoints.Length - 1)
        {
            FinishReplay();
            return;
        }

        // Find the current trajectory point
        while (currentPointIndex < trajectoryPoints.Length - 1 && trajectoryPoints[currentPointIndex + 1].time <= currentTime)
        {
            currentPointIndex++;
        }

        if (currentPointIndex < trajectoryPoints.Length - 1)
        {
            float segmentDuration = trajectoryPoints[currentPointIndex + 1].time - trajectoryPoints[currentPointIndex].time;
            float t = Mathf.Clamp01((currentTime - trajectoryPoints[currentPointIndex].time) / Mathf.Max(0.001f, segmentDuration));

            transform.position = Vector3.Lerp(
                trajectoryPoints[currentPointIndex].position,
                trajectoryPoints[currentPointIndex + 1].position,
                t);

            transform.rotation = Quaternion.Lerp(
                trajectoryPoints[currentPointIndex].Rotation,
                trajectoryPoints[currentPointIndex + 1].Rotation,
                t);
        }
        else
        {
            FinishReplay();
        }
    }

    void FinishReplay()
    {
        if (trajectoryPoints.Length > 0)
        {
            transform.position = trajectoryPoints[^1].position;
            transform.rotation = trajectoryPoints[^1].Rotation;
        }
        isReplaying = false;
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