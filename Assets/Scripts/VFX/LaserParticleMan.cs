using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticleMan : MonoBehaviour
{
    [SerializeField] private Transform tipParticleTransform;
    [SerializeField] private ParticleSystem sparks;

    public void UpdateParticles(Vector3 playerPos, Vector3 tipPos) {
        tipParticleTransform.position = tipPos;
        float aimAngle = Vector3.SignedAngle(Vector3.up, tipPos-playerPos, Vector3.forward);
        sparks.transform.eulerAngles = new Vector3(0, 0, aimAngle);
    }
}
