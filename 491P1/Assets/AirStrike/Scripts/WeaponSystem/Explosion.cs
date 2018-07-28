using UnityEngine;
using System.Collections;

namespace HWRWeaponSystem
{
	public class Explosion : MonoBehaviour
	{
		public int Force;
		public int Radius;
		public AudioClip[] Sounds;

		private void Start ()
		{
      		
		}
	
		private void OnEnable ()
		{
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPos, Radius);
			if (Sounds.Length > 0) {
				AudioSource.PlayClipAtPoint (Sounds [Random.Range (0, Sounds.Length)], transform.position);

			}
			foreach (Collider hit in colliders) {
				if (hit.GetComponent<Rigidbody>()) {
					hit.GetComponent<Rigidbody>().AddExplosionForce (Force, explosionPos, Radius, 3.0f);
                  
				}
				//print ("something exploded");
				//print ("what did it hit " + hit.gameObject.tag);
				if (hit.gameObject.tag == "Scene") {
					AkSoundEngine.PostEvent ("sandExplosion", hit.gameObject);
				}
				if (hit.gameObject.tag == "Enemy") {
					AkSoundEngine.PostEvent ("metalExplosion", hit.gameObject);
				}
				if (hit.gameObject.tag == "Player") {
					AkSoundEngine.PostEvent ("metalExplosion", hit.gameObject);
				}
				if (hit.gameObject.tag == "Untagged") {
					AkSoundEngine.PostEvent ("normalExplosion", hit.gameObject);
				}
			}
		}
	}
}