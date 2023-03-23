using UnityEngine;

namespace LevelGenerator.Scripts
{
    public class DeadEnd : MonoBehaviour
    {
        public Bounds Bounds;

        public void Initialize(LevelGenerator levelGenerator)
        {
            transform.SetParent(levelGenerator.Container);
            levelGenerator.RegistrerNewDeadEnd(Bounds.Colliders);

            name = name.Replace("(Clone)", "");
            name += " " + transform.GetSiblingIndex().ToString();
        }
    }
}