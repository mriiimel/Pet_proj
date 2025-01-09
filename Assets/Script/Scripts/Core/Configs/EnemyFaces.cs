using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFaces",menuName = "ScriptableObject/EnemyFace",order = 0)]
public class EnemyFaces : ScriptableObject
{
    public Material _idleFace;
    public Material _friendlyFace;
    public Material _pockerFace;
    public Material _attackFace;
    public Material _deadFace;
    
}
