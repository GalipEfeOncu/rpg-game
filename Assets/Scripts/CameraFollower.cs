using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float minX, maxX, minZ, maxZ;

    private int state = 0; // Camera position state
    private float firstOffsetX;
    private float firstOffsetZ;
    Vector3 rotation;

    private void ApplyCameraState()
    {
        float angle = state * 45f + 270f;
        transform.rotation = Quaternion.Euler(50, angle, 0);

        switch (state)
        {
            case 0:
                offset = new Vector3(firstOffsetX, offset.y, firstOffsetZ);
                break;
            case 1:
                offset = new Vector3(10f / Mathf.Sqrt(2f), offset.y, -10f / Mathf.Sqrt(2f));
                break;
            case 2:
                offset = new Vector3(0f, offset.y, -10f);
                break;
            case 3:
                offset = new Vector3(-10f / Mathf.Sqrt(2f), offset.y, -10f / Mathf.Sqrt(2f));
                break;
            case 4:
                offset = new Vector3(-10f, offset.y, 0f);
                break;
            case 5:
                offset = new Vector3(-10f / Mathf.Sqrt(2f), offset.y, 10f / Mathf.Sqrt(2f));
                break;
            case 6:
                offset = new Vector3(0f, offset.y, 10f);
                break;
            case 7:
                offset = new Vector3(10f / Mathf.Sqrt(2f), offset.y, 10f / Mathf.Sqrt(2f));
                break;
        }
    }

    private void Start()
    {
        firstOffsetX = offset.x;
        firstOffsetZ = offset.z;
        rotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = (state + 1) % 8;
            Debug.Log(state);
            ApplyCameraState();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            state = (state - 1 + 8) % 8;
            Debug.Log(state);
            ApplyCameraState();
        }
    }

    private void LateUpdate()
    {
        float clampedX = Mathf.Clamp(player.transform.position.x + offset.x, minX, maxX);
        float clampedZ = Mathf.Clamp(player.transform.position.z + offset.z, minZ, maxZ);
        float Y = player.transform.position.y;

        transform.position = new Vector3(clampedX, Y + offset.y, clampedZ);
    }
}
