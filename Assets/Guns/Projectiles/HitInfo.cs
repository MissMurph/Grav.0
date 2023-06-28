using Grav.Entities;

namespace Grav.Guns {
	public struct HitInfo {
		public Entity Target { get; set; }
		public Projectile Bullet { get; set; }
		public bool Result { get; set; }
	}
}