using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour{
    public Transform pos1;
    public Transform pos2;
    public Transform moved;

    public float speed;
    float t;

    void Update(){
        t += speed * Time.deltaTime;    
        moved.position = Vector3.Lerp(pos1.position, pos2.position, Mathf.PingPong(t, 1));
    }
}