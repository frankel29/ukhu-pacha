using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector2 roomSize = new Vector2(25, 25);

    private Vector2 currentRoomPos;

    void Start()
    {
        currentRoomPos = Vector2.zero;
        MoveCameraToRoom(currentRoomPos);
    }

    public void MoveCameraToRoom(Vector2 roomGridPos)
    {
        Vector3 newCamPos = new Vector3(roomGridPos.x * roomSize.x, roomGridPos.y * roomSize.y, transform.position.z);
        transform.position = newCamPos;
        currentRoomPos = roomGridPos;
    }
}
