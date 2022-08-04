using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;


    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;


    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start() {
        readyToThrow = true;
    }

    // This function is called by InputManager whenever button assigned in InputSystem is pressed
    public void Throw() {
        if (readyToThrow && totalThrows>0) {
            ThrowLogic();
        }
    }

    // Helper function for Throw(). Contains logic for throwing projectile
    private void ThrowLogic() {
        readyToThrow = false;

        // Instantiate projectile
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // Get projectile RigidBody
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        // Calculate direction
        Vector3 forceDiraction = cam.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f)) {
            forceDiraction = (hit.point - attackPoint.position).normalized;
        }

        // Create force Vector, in the direction we're looking
        Vector3 forceToAdd = forceDiraction * throwForce + transform.up * throwUpwardForce;

        // Add force to projectile
        projectileRB.AddForce(forceToAdd, ForceMode.Impulse); // ForceMode.Impulse for adding the force only once
        totalThrows--;

        // Throw cooldown
        Invoke(nameof(ResetThrow), throwCooldown);

    }

    private void ResetThrow() {
        readyToThrow = true;
    }

}
