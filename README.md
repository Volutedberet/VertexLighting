# Vertex Based Lighting, for Unity URP
![](https://github.com/Volutedberet/VertexLighting/blob/main/VertexGif.gif)

## This is a lighting system, that uses vertex colors to fake lighting, which was a technique used by older games like Silent Hill 1
![](https://github.com/Volutedberet/VertexLighting/blob/main/LightExample.png)
### It produces some nice and blocky lighting, perfectly fitting for making games that look like they belong on the ps1

### Key Features:
```
-Realtime Lights:
    -Updated each tick, lighting up static and nonstatic surfaces
    (Intended for places where the light is always on, and the performance cost doesn't matter)

-Static Lights:
    -On Load, bakes it's light onto static surfaces
    -Baked lighting can be temporarily overwritten by realtime lights with more intensity
    (Intended for lighting background enviroments, since it's lighting doesn't translate to dynamic surfaces that move)

-Dynamic Lights:
    -On Load, bakes just like a static, but can act as a realtime light, when the main camera is close
    -The activation distance is changable
    (Intended for main enviroment lighting, since they are a middle ground between static and realtime, offering performance even when there is a lot of them, but they can still cast lighting on dynamic surfaces when needed)

-4 light modes:
    -lit (Lights rendered as normal)
    -Unlit (No lighting)
    -Light Debug (Only the lighting is rendered)
    -Vertex Debug (Displays a gradient that helps you see how geometry dense the area is)

-Adjustable update rate, to get even chunkier results

-Custom main shader, that supports detail textures

-3 Demo Scenes, showcasing a setup for each lighting mode
```

## The lightings resolution is 1-1 to the resolution of your meshes, so to get the intended blocky lighting, you must use low poly count meshes