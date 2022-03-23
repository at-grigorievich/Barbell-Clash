using UnityEngine;

namespace Barbell
{
    public interface ISizeable
    {
        uint Id { get; }
        float Radius { get; }
        float Thickness{ get; }
        
        Vector3 MeshSize { get; }
    }
}