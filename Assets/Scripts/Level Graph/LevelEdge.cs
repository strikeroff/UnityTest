﻿using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class LevelEdge : MonoBehaviour
{
    public Material baseMaterial;
    public Color baseEmission;
    public Color unlockedEmission;
    public Color completedEmission;
    public float unhoverEmissionMuliplier = 0.7f;
    public float hoverEmissionMuliplier = 2f;

    public LevelNode from;
    public LevelNode to;
    MeshRenderer[] renderers;

    void OnEnable() {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public bool Hovered() {
        return from.Hovered() || to.Hovered();
    }

    float EmissionMultiplier() {
        return Hovered() ? hoverEmissionMuliplier : unhoverEmissionMuliplier;
    }

    public void SetEmission(Color emission) {
        renderers.ToList().ForEach(r => {
            if (r.material.GetColor("_EmissionColor") != emission) {
                r.material.SetColor("_EmissionColor", emission);
            }
        });
    }

    void Update() {
        if (Extensions.Editor()) {
            if (from != null && to != null) {
                transform.position = (from.transform.position + to.transform.position) / 2;
                transform.LookAt(to.transform);
                transform.localScale = new Vector3(1, 1, (from.transform.position - to.transform.position).magnitude);
                gameObject.name = string.Format("{0} - {1}", from.name, to.name);
                renderers.ToList().ForEach(r => r.enabled = from.visible && to.visible);
            }
        } else {
            if (from.level.Completed()) {
                SetEmission(completedEmission * EmissionMultiplier());
            } else if (from.level.Unlocked()) {
                SetEmission(unlockedEmission * EmissionMultiplier());
            } else {
                SetEmission(baseEmission * EmissionMultiplier());
            }
        }
    }
}