using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightableSurface : MonoBehaviour{
    public bool staticSurface;
    Mesh mesh;
    float[] bakedBrightness;
    Vector3 startPosition;
    Renderer renderer;

    void Start(){
        mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        renderer = this.gameObject.GetComponent<Renderer>();
        startPosition = this.transform.position;

        if(bakedBrightness == null){
            bakedBrightness = new float[mesh.vertices.Length];
        }
    }

    private void Update(){
        if(staticSurface){
            if(this.transform.position != startPosition){
                Debug.LogError($"A static surface ({this.gameObject.name}) has been moved!  This surface might have baked lighting, that will get inaccurate when the surface is moved, if you want this object to move, make sure it's not set to static!");
                this.transform.position = startPosition;
            }
        }
    }

    public void BakeLighting(){
        if(mesh == null){
            mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        }

        LightingManager lm = FindObjectOfType<LightingManager>();
        LightPoint[] lights = FindObjectsOfType<LightPoint>();

        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        bakedBrightness = new float[mesh.vertices.Length];

        for (int i = 0; i < vertices.Length; i++){
            foreach (var light in lights){
                if(light.lightMode == LightRenderMode.baked || light.lightMode == LightRenderMode.dynamic){
                    if(Vector3.Distance(this.transform.position + vertices[i], light.transform.position) < light.radious){
                        float lightLevel = Mathf.Lerp(1, 0, Vector3.Distance(this.transform.position + vertices[i], light.transform.position) / light.radious) * light.intensity;
                        if(lightLevel > bakedBrightness[i]){
                            bakedBrightness[i] = lightLevel;
                        }
                    }                    
                }
            }
            colors[i] = Color.Lerp(lm.baseLightColor, new Color(1, 1, 1, 1), bakedBrightness[i]);
        }

        mesh.colors = colors;
    }

    public void ClearBakedData(){
        if(mesh == null){
            mesh = this.gameObject.GetComponent<MeshFilter>().sharedMesh;
        }

        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        bakedBrightness = new float[mesh.vertices.Length];

        for (int i = 0; i < vertices.Length; i++){
            colors[i] = new Color(1, 1, 1, 1);
        }

        mesh.colors = colors;     
    }

    public void UpdateSurfaceLighting(LightingManager lightingManager, LightPoint[] lights){       
        if(!renderer.isVisible){
            return;
        } 

        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        float[] brightness = new float[vertices.Length];

        if(staticSurface){
            if(bakedBrightness.Length > 0){
                for (int i = 0; i < bakedBrightness.Length; i++){
                    brightness[i] = bakedBrightness[i];
                }                
            }
        }

        for (int i = 0; i < vertices.Length; i++){
            foreach (var light in lights){
                if(light.lightMode == LightRenderMode.realtime){
                    if(Vector3.Distance(this.transform.position + vertices[i], light.transform.position) < light.radious){
                        float lightLevel = Mathf.Lerp(1, 0, Vector3.Distance(this.transform.position + vertices[i], light.transform.position) / light.radious) * light.intensity;
                        if(lightLevel > brightness[i]){
                            brightness[i] = lightLevel;
                        }
                    }                    
                }else if(light.lightMode == LightRenderMode.dynamic){
                    if(light.isLightActiveDynamic){     
                        if(Vector3.Distance(this.transform.position + vertices[i], light.transform.position) < light.radious){
                            float lightLevel = Mathf.Lerp(1, 0, Vector3.Distance(this.transform.position + vertices[i], light.transform.position) / light.radious) * light.intensity;
                            if(lightLevel > brightness[i]){
                                brightness[i] = lightLevel;
                            }
                        }       
                    }
                }
            }
            colors[i] = Color.Lerp(lightingManager.baseLightColor, new Color(1, 1, 1, 1), brightness[i]);
        }

        mesh.colors = colors;
    }

    public void ResetLighting(){
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++){
            colors[i] = new Color(1, 1, 1, 1);
        }

        mesh.colors = colors;        
    }

    public void VisualiseVerts(){
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++){
            colors[i] = Color.Lerp(Color.blue, Color.red, Mathf.Repeat(0.5f * i, 1));
        }

        mesh.colors = colors;
    }
}