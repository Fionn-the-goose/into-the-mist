using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public const float GROUND_CHECK_RADIUS = 0.25f;
    
    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, GROUND_CHECK_RADIUS);
    }
}
