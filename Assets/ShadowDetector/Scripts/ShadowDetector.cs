﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetector : MonoBehaviour
{

    public bool autoGetCollider = true;
    public CapsuleCollider playerCollider;
    public LayerMask lightLayers = -1;
    public LayerMask obstaclesLayers = -1;
    [Tooltip("Maximum detector brightness to trigger the stealth mode")]
    public float shadedBright = 0.7f;
    [Tooltip("Response frequency")]
    public float sensorDelay = 1f;
    [Tooltip("Brightness sensor threshold")]
    public float maxShadowBright = 2f;
    [Tooltip("Auto brightness sensor threshold")]
    public bool autoMaxShadowBright = true;
    [Tooltip("Consider the ambient intensity from render settings")]
    public bool useAmbientIntensity = true;
    [Tooltip("Consider shadow strength of the light source")]
    public bool useShadowStrength = true;
    public bool debugMode = true;

    private static List<Light> v_directionalLightList = new List<Light>();
    private static List<Light> v_pointLightList = new List<Light>();
    private static List<Light> v_spotLightList = new List<Light>();
    private float v_bright;
    private bool v_croutineReady = true;
    private Transform t_player;
    private Vector3 v_capsCenter;
    private float v_capsRadius;
    private float v_capsHalfHeight;
    private Vector3 v_upDirection;

    public bool hidden
    {

        get { return v_bright <= shadedBright; }
    }

    public float bright
    {
        get { return v_bright; }
    }

    void Start()
    {
        if (autoGetCollider)
        {
            playerCollider = GetComponent<CapsuleCollider>();
        }
        else
        {
            if (playerCollider == null) playerCollider = gameObject.AddComponent<CapsuleCollider>();
        }
        t_player = transform;
    }

    void Update()
    {

        // Debug.Log(v_bright);

        if (autoMaxShadowBright)
        {
            maxShadowBright = useAmbientIntensity ? 2 : 1;
        }

        if (v_croutineReady)
        {
            v_bright = 0f;
            v_croutineReady = false;
            v_capsCenter = t_player.TransformPoint(playerCollider.center);
            v_capsRadius = playerCollider.radius;
            v_capsHalfHeight = playerCollider.height * 0.5f;
            v_upDirection = t_player.up;
            StartCoroutine(GetAllCloseLights(v_capsCenter, () =>
            {
                StartCoroutine(GetDirectionalLightsBright(() =>
                {
                    StartCoroutine(GetPointLightsBright(() =>
                    {
                        StartCoroutine(GetSpotLightsBright(() =>
                        {
                            if (useAmbientIntensity) v_bright += RenderSettings.ambientIntensity;
                            v_bright = maxShadowBright < v_bright ? maxShadowBright : v_bright;
                            StartCoroutine(WaitReadyCroutine(sensorDelay));
                        }));
                    }));
                }));
            }));
        }

    }

    private IEnumerator WaitReadyCroutine(float t)
    {
        yield return new WaitForSeconds(t);
        v_croutineReady = true;
    }

    private IEnumerator GetAllCloseLights(Vector3 point, Action callback)
    {

        v_directionalLightList.Clear();
        v_pointLightList.Clear();
        v_spotLightList.Clear();

        Light[] lights = FindObjectsOfType<Light>();

        foreach (Light currLight in lights)
        {
            switch (currLight.type)
            {
                case LightType.Directional:
                    if (currLight.enabled && IsInLayerMask(currLight.gameObject)) v_directionalLightList.Add(currLight);
                    break;
                case LightType.Point:
                    float currPointLightDist = (currLight.transform.position - point).sqrMagnitude;
                    if (currLight.enabled && IsInLayerMask(currLight.gameObject)
                        && currPointLightDist < currLight.range * currLight.range)
                        v_pointLightList.Add(currLight);
                    break;
                case LightType.Spot:
                    float currSpotLightDist = (currLight.transform.position - point).sqrMagnitude;
                    if (currLight.enabled && IsInLayerMask(currLight.gameObject)
                        && currSpotLightDist < currLight.range * currLight.range)
                        v_spotLightList.Add(currLight);
                    break;
            }
        }

        callback();
        yield return null;

    }

    private void LightCollision(Transform from, Action<Vector3> callback)
    {
        Vector3 lightPos = from.position;
        Vector3 heading = v_capsCenter - lightPos;
        Vector3 leftDirection = Vector3.Cross(heading.normalized, v_upDirection).normalized;
        RaycastHit hit;
        // center
        if (Physics.Linecast(lightPos, v_capsCenter, out hit, obstaclesLayers))
        {
            //left
            if (t_player != hit.transform &&
                Physics.Linecast(lightPos, v_capsCenter + leftDirection * v_capsRadius, out hit, obstaclesLayers))
            {
                // right
                if (t_player != hit.transform &&
                    Physics.Linecast(lightPos, v_capsCenter + -leftDirection * v_capsRadius, out hit, obstaclesLayers))
                {
                    // up
                    if (t_player != hit.transform &&
                        Physics.Linecast(lightPos, v_capsCenter + v_upDirection * v_capsHalfHeight, out hit, obstaclesLayers))
                    {
                        // down
                        if (t_player != hit.transform &&
                            Physics.Linecast(lightPos, v_capsCenter + -v_upDirection * v_capsHalfHeight, out hit, obstaclesLayers))
                        {
                            if (t_player != hit.transform) return;
                        }
                    }
                }
            }
        }
        if (debugMode) Debug.DrawLine(lightPos, hit.point, Color.green, 0.1f);
        callback(heading);
    }

    private bool SpotAngleTest(Light light)
    {
        Vector3 heading = v_capsCenter - light.transform.position;
        float toPosAngle = Vector3.Angle(v_capsCenter - light.transform.position, light.transform.forward);
        return !(heading.magnitude > light.range || toPosAngle > light.spotAngle / 2f);
    }

    private IEnumerator GetDirectionalLightsBright(Action callback)
    {
        foreach (Light light in v_directionalLightList)
        {
            Vector3 heading = v_capsCenter - light.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = light.transform.rotation * Vector3.forward;
            direction = Vector3.Reflect(direction, -direction);
            RaycastHit hit;
            if (Physics.Raycast(v_capsCenter, direction, out hit, distance, obstaclesLayers)) continue;
            float to_bright = light.intensity;
            if (useShadowStrength) to_bright = to_bright * light.shadowStrength;
            v_bright += to_bright;
        }
        callback();
        yield return null;
    }

    private IEnumerator GetPointLightsBright(Action callback)
    {
        foreach (Light light in v_pointLightList)
        {
            LightCollision(light.transform, (heading) =>
            {
                float to_bright = light.intensity - (heading.magnitude / light.range * light.intensity);
                if (useShadowStrength) to_bright = to_bright * light.shadowStrength;
                v_bright += to_bright;
            });
        }
        callback();
        yield return null;
    }

    private IEnumerator GetSpotLightsBright(Action callback)
    {
        foreach (Light light in v_spotLightList)
        {
            if (!SpotAngleTest(light)) continue;
            LightCollision(light.transform, (heading) =>
            {
                float to_bright = light.intensity - (heading.magnitude / light.range * light.intensity);
                if (useShadowStrength) to_bright = to_bright * light.shadowStrength;
                v_bright += to_bright;
            });
        }
        callback();
        yield return null;
    }

    private bool IsInLayerMask(GameObject obj)
    {
        return ((lightLayers.value & (1 << obj.layer)) > 0);
    }

}