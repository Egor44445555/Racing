using UnityEngine;

[System.Serializable]
public struct TrajectoryPoint
{
    public float time;
    public Vector3 position;
    public Vector3 rotationEuler;
    public Vector3 velocity;

    public Quaternion Rotation => Quaternion.Euler(rotationEuler);

    public TrajectoryPoint(float time, Transform transform, Vector3 velocity)
    {
        this.time = time;
        this.position = transform.position;
        this.rotationEuler = transform.rotation.eulerAngles;
        this.velocity = velocity;
    }
}