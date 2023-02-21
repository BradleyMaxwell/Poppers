using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Player player;
    public int sensitivity = 10;
    public float rotationSpeed = 30f;
    [HideInInspector] public EnemyCombat enemyTarget;

    private CinemachineFramingTransposer playerCamera;

    void Start()
    {
        playerCamera = GameObject.Find("PlayerVirtualCamera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = RayToAim();
        Vector3 nextToPlayer = ray.origin + ray.direction * playerCamera.m_CameraDistance;

        // face the body of the player towards the end of their range
        Vector3 aimTarget = nextToPlayer + ray.direction * player.range;
        Vector3 aimTargetXZ = new Vector3(aimTarget.x, transform.position.y, aimTarget.z);
        Vector3 aimDirection = (aimTargetXZ - transform.position).normalized; // gets the direction from the player to the aim target
        FaceAimTarget(aimDirection, rotationSpeed);

        // find if their is an enemy in the line of sight of the player's aim so that the player combat script can know if the player is aiming at an enemy when they attack
        bool collision = Physics.Raycast(nextToPlayer, ray.direction, out RaycastHit hit, player.range);
        if (collision)
        {
            EnemyCombat enemyCombat = hit.collider.GetComponent<EnemyCombat>();
            if (enemyCombat)
            {
                enemyTarget = enemyCombat;
            } else
            {
                enemyTarget = null;
            }
        } else
        {
            enemyTarget = null;
        }
    }

    private void FaceAimTarget(Vector3 direction, float rotationSpeed) // rotates character in 3d space at a given speed to a given direction using linear interpolation
    {
        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
    }

    private Ray RayToAim() // returns the ray that goes from the middle of the screen where the player is aiming
    {
        Vector2 middleOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        return Camera.main.ScreenPointToRay(middleOfScreen);
    }
}
