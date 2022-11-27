using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    private RectTransform bar;
    [Range(0,1)]
    [SerializeField] private float oxygen = 1.0f;
    [SerializeField] private float DrainSpeed = 0.1f;
    [SerializeField] private float RegenSpeed = 0.1f;
    [SerializeField] private bool isDraining = true;

    void Start()
    {
        bar = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(isDraining)
        {
            if(oxygen > 0)
                oxygen -= Time.deltaTime * DrainSpeed;
            else
                oxygen = 0;
        } 
        else
        {
            if (oxygen < 1)
                oxygen += Time.deltaTime * RegenSpeed;
            else
                oxygen = 1;
        }

        setScale(new Vector3(oxygen, 1, 1));
    }

    // Getters and setters
    void setScale(Vector3 scalar) => bar.localScale = scalar;
    Vector3 getScale() => bar.localScale;
    void setDrainSpeed(float f) => DrainSpeed = f;
    float getDrainSpeed() => DrainSpeed;
    void setIsDraining(bool b) => isDraining = b;
    bool getIsDraining() => isDraining;
}
