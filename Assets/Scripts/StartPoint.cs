using UnityEngine;

public class StartPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelController.main.IncreaseCircleCount();
        }
    }
}
