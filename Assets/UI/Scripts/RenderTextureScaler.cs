using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureScaler : MonoBehaviour
{
    public RenderTexture Target;
    public event Action OnResize;
    [SerializeField] [Range(0, 2)]public float u = 1;
    [SerializeField] [Range(0, 2)]public float v = 1;
    Resolution res;
    Camera c;
    private void Awake() {
         c = GetComponent<Camera>();
        Target = new RenderTexture((int)(Screen.width*u), (int)(Screen.height*v), 24);
        res = new Resolution();
        c.targetTexture = Target;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Refresh() {
        Target.Release();
        Target = new RenderTexture((int)(Screen.width * u), (int)(Screen.height * v), 24);
        Target.antiAliasing = 4;
        c.targetTexture = Target;
        res.width = Screen.width;
        res.height = Screen.height;

        OnResize?.Invoke();

    }
    // Update is called once per frame
    void Update()
    {
        if (res.height != Screen.height|| res.width != Screen.width) {

            Refresh();
        }

    }
}
