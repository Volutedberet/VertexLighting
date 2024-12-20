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

    [HideInInspector] public bool isLightActiveDynamic;
    LightingManager lightingManager;
    Vector3 startPosition;

    public void SetMeUp(LightingManager lm){
        startPosition = this.transform.position;
        lightingManager = lm;
    }

    private void Update(){
        if(lightMode == LightRenderMode.baked || lightMode == LightRenderMode.dynamic){
            if(this.transform.position != startPosition){
                Debug.LogError($"A static/dynamic light ({this.gameObject.name}) has been moved!    If you want this lights lighting to update to it's new position, use the realtime light mode!");
                this.transform.position = startPosition;
            }
        }

        if(lightMode == LightRenderMode.dynamic){
            isLightActiveDynamic = Vector3.Distance(this.transform.position, lightingManager.camMain.transform.position) < lightingManager.dynamicLightActiveDistance;
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
            case LightRenderMode.dynamic:
                Gizmos.DrawIcon(transform.position, "../VertexLighting/Gizmos/DynamicLight.png", true);
                break;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(this.transform.position, radious);
    }
}

//TODO: Add a system that allows lens flares to be displayed where a light is