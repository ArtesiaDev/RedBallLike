using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Configs/MainConfig", order = 1)]
    public class Config: ScriptableObject
    {
        public const string JUMP_BUTTON = "Jump";
        public const string LEFT_BUTTON = "Left";
        public const string RIGHT_BUTTON = "Right";
        
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public int PlayerPieceCount { get; private set; }
        [field: SerializeField] public int PlayerPieceImpulse { get; private set; }
        [field: SerializeField] public float ParallaxScale { get; private set; }
    }
}