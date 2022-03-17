using ATG.LevelControl;
using Barbell;
using PlayerLogic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Levels/New Barbell Level", order = 0)]
public class BarbellLevelData : LinearLevelData
{
    [field:Header("Level Configuration")]
    
    [field: Space(15)]
    [field: SerializeField] 
    public BarbellConfigContainer BarbellInitialConfig { get; private set; }
    
    [field: Space(5)]
    [field: SerializeField] 
    public CameraConfigContainer CameraInitialConfig { get; private set; }
}
