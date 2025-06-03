using System.Collections.Generic;
using UnityEngine;

public class SimpleMapGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;         // Room_1, Room_2, Room_3 (boss), etc.
    public int maxRooms = 12;
    public Vector2 roomSize = new Vector2(5, 3); // Tamaño real de las salas en unidades Unity

    private List<Vector2> occupiedPositions = new List<Vector2>();
    private Vector2 currentPos = Vector2.zero;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // Instanciar sala inicial
        Instantiate(roomPrefabs[0], currentPos, Quaternion.identity).tag = "Room";
        occupiedPositions.Add(currentPos);

        List<Vector2> normalRoomPositions = new List<Vector2>();

        // Generar salas normales (sin jefe)
        for (int i = 1; i < maxRooms; i++)
        {
            Vector2 nextPos = GetNextPosition();
            Debug.Log("Generando sala en: " + nextPos);

            int roomIndex;

            // Elegir una sala distinta a la del jefe (índice 2)
            do {
                roomIndex = Random.Range(0, roomPrefabs.Length);
            } while (roomIndex == 2);

            GameObject newRoom = Instantiate(roomPrefabs[roomIndex], nextPos, Quaternion.identity);
            newRoom.tag = "Room";

            occupiedPositions.Add(nextPos);
            normalRoomPositions.Add(nextPos);
            currentPos = nextPos;
        }

        // Determinar cuál es la sala más lejana al inicio
        Vector2 bossPos = normalRoomPositions[0];
        float maxDistance = Vector2.Distance(Vector2.zero, bossPos);

        foreach (Vector2 pos in normalRoomPositions)
        {
            float dist = Vector2.Distance(Vector2.zero, pos);
            if (dist > maxDistance)
            {
                maxDistance = dist;
                bossPos = pos;
            }
        }

        // Reemplazar sala lejana por la sala jefe
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            Vector2 roomPos = new Vector2(room.transform.position.x, room.transform.position.y);
            if (Vector2.Distance(roomPos, bossPos) < 0.1f)
            {
                Destroy(room);
                break;
            }
        }

        // Instanciar sala del jefe en esa posición
        Instantiate(roomPrefabs[2], bossPos, Quaternion.identity).tag = "Room";
        Debug.Log("Sala del jefe instanciada en: " + bossPos);
    }

    Vector2 GetNextPosition()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 newPos;

        do
        {
            Vector2 dir = directions[Random.Range(0, directions.Length)];
            newPos = currentPos + new Vector2(dir.x * roomSize.x, dir.y * roomSize.y);
        }
        while (occupiedPositions.Contains(newPos));

        return newPos;
    }
}
