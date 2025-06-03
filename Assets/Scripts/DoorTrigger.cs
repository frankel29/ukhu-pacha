using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Vector2 connectedRoomPosition; // Posición de la sala a la que lleva esta puerta (en coordenadas de grilla)

    private CameraController cameraController;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Puerta cruzada, moviendo cámara a: " + connectedRoomPosition);
            cameraController.MoveCameraToRoom(connectedRoomPosition);
        }
    }
}

