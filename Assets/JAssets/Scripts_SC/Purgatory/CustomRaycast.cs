using UnityEngine;

namespace JAssets.Scripts_SC
{
    public class CustomRaycast
    {
        public CustomRaycast(RaycastHit2D hit, GroundCastDirection Direction)
        {
            RayHit = hit;
            this.Direction = Direction;
        }

        public RaycastHit2D RayHit { get; }
        public GroundCastDirection Direction { get; }
    }
}