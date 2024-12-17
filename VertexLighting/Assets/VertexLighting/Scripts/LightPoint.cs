using UnityEngine;

public enum LightRenderMode{
   realtime,
   baked,
   dynamic
}

public class LightPoint : MonoBehaviour{
    [Range(0, 255)]
    public float radious = 3;

    [Range(0, 10)]
    public float intensity = 1;

    [Space(10), Tooltip("Realtime = Updates constantly; Baked = Updates only when lighting is baked, and only applies to static surfaces; Dynamic = Bakes onto static surfaces, and doesn't update onto non static surfaces, unless the Main Camera gets close to it")]
    public LightRenderMode lightMode;
    Vector3 startPosition;

    private void Start(){
        startPosition = this.transform.position;
    }

    private void Update(){
        if(lightMode == LightRenderMode.baked){
            if(this.transform.position != startPosition){
                Debug.LogError($"A static light ({this.gameObject.name}) has been moved!    If you want this lights lighting to update to it's new position, use the realtime light mode!");
                this.transform.position = startPosition;
            }
        }
    }

    private void OnDrawGizmos(){
        switch (lightMode){
            case LightRenderMode.realtime:
                Gizmos.DrawIcon(transform.position, "../VertexLighting/Gizmos/RealtimeLight.png", true);
                break;
            case LightRenderMode.baked:
                Gizmos.DrawIcon(transform.position, "../VertexLighting/Gizmos/StaticLight.png", true);
                break;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(this.transform.position, radious);
    }
}