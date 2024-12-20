# Vertex Based Lighting, for Unity URP

## This lighting system uses vertex colors to fake lighting, which was a older technique for lighting in games like Silent Hill 1
## The lightings resolution is 1-1 to the resolution of your meshes, so to get the intended blocky lighting, you must use low poly count meshes

### Key Features:
```
-Adjustable update rate, to get even chunkier results

-Realtime Lights:
    -Each update tick, they update the lighting on dynamic and static light surfaces
    (Intended for places where the light is always on, and the performance cost doesn't matter)

-Static Lights:
    -On Load, they baked their lighting onto static light surfaces
    -Their light can be temporarily overridden by higher intensity realtime lights
    (Intended for lighting background enviroments, since it's lighting doesn't translate to dynamic surfaces that move)

-Dynamic Lights:
    -On Load, they bake just like static lights, however if the Main Camera (marked by the MainCamera tag) gets in a set distance from them they switch to become realtime lights 
    (Intended for main enviroment lighting, since they are a middle ground between static and realtime, offering performance even when there is a lot of them, but they can still cast lighting on dynamic surfaces when needed)
```