using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed = 8f;
    private Camera mainCamera;
    private Vector3 mouseInput = Vector3.zero;



    private void Initialized()
    {
        mainCamera = Camera.main;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Initialized();
    }

    private void Update()
    {
        if (!IsOwner || !Application.isFocused)
        {
            return;
        }

        mouseInput.x = Input.mousePosition.x;
        mouseInput.y = Input.mousePosition.y;
        mouseInput.z = mainCamera.nearClipPlane;
        
        Vector3 mouseToWorldPoint = mainCamera.ScreenToWorldPoint(mouseInput);

        transform.position = Vector3.MoveTowards(transform.position, mouseToWorldPoint, Time.deltaTime * speed);

        if (mouseToWorldPoint != transform.position)
        {
            Vector3 targetDirection = mouseToWorldPoint - transform.position;
            targetDirection.z = 0;
            transform.up = targetDirection;
        }
    }


}
