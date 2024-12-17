using UnityEngine;

public class RotateOvertime : MonoBehaviour{
    public Vector3 speed;

    void Update(){
        this.transform.Rotate(speed * Time.deltaTime);
    }
}