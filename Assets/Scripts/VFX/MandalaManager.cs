using UnityEngine;

public class MandalaManager : MonoBehaviour {
    [SerializeField] private Transform axis;
    [SerializeField] private Mandala mandala;

    //You a more robust system for updating the mandala position/enabling/disabling. Honestly I can make one if you need a sysarch guy.
    void Update() {
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        UpdateMandala(mousePos);
    }

    void Start() {Deactivate();}
    
    //Sets the mandala's position given mouse pos
    void UpdateMandala(Vector3 mousePos) {
        Vector3 d = mousePos-transform.position;
        Vector3 p = projectAgainstAxis(d);

        float a = Vector3.SignedAngle(Vector3.right, p, Vector3.up) - 90;
        var prevRot = axis.localEulerAngles;
        prevRot.y = a;
        axis.localEulerAngles = prevRot;
    }

    //Converts a vector in the xy plane to a vector on the plane orthogonal to the axis vector.
    //Think of it like a reverse projection from the axis's plane to the xy plane.
    public Vector3 projectAgainstAxis(Vector3 p) {
        Vector3 axisV = axis.rotation * Vector3.up;
        float newZ = -(axisV.x * p.x + axisV.y * p.y)/axisV.z;
        return new Vector3(p.x, p.y, newZ);
    }

    //Think of it like the origin point for all spell vfx.
    public Vector2 GetMandalaCenter() {
        return mandala.GetPos();
    }

    public void Activate() {
        mandala.Activate();
    }

    public void Deactivate() {
        mandala.Deactivate();
    }
}