using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{

    private string playerTag = "Player";
    private float bounceForce = 10f;

    private void OnCollisionEnter(Collision collision) {
        Rigidbody other = collision.rigidbody;
        other.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
        Debug.Log(other.tag);
    }
}
