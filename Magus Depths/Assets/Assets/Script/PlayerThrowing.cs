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
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start() {
        readyToThrow = true;
    }

    private void Update() {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows>0) {
            Throw();
        }
        Debug.Log("INN");
    }

    // public void ThrowUpdate() {
    //     if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows>0) {
    //         Throw();
    //     }
    //     Debug.Log("INN");
    // }

    private void Throw() {
        readyToThrow = false;

        // Instantiate projectile
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // Get projectile RigidBody
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        // Create force Vector, in the direction we're looking
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

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
