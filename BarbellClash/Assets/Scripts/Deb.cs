using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deb : MonoBehaviour
{
    public SkinnedMeshRenderer _r;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_r.sharedMesh.colors32.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
