using UnityEngine;
using System.Collections;
using HWRWeaponSystem;

namespace AirStrikeKit
{
	public class DamageDecay : MonoBehaviour
	{

		private DamageManager damage;
		public int[] DamageLowerThan = { 10 };
		public GameObject[] DecayObject;
        private bool lowHPsoundplayingyet = false;

		void Start ()
		{
			damage = this.GetComponent<DamageManager> ();
		}
        /*
		private void OnDisable()
		{
            AkSoundEngine.PostEvent("stopEngineSputter", gameObject);
		}
        
		private void OnDestroy()
		{
            AkSoundEngine.PostEvent("stopEngineSputter", gameObject);
		}*/

		void Update ()
		{
			if (damage == null || DecayObject.Length != DamageLowerThan.Length || DecayObject.Length <= 0)
				return;

			for (int i = 0; i < DecayObject.Length; i++) {
				if (damage.HP > DamageLowerThan [i]) {
					DecayObject [i].SetActive (false);
				}
			}

			for (int i = 0; i < DecayObject.Length; i++) {
				if (damage.HP < DamageLowerThan [i]) {
					DecayObject [i].SetActive (true);
                    //if(lowHPsoundplayingyet = false)
                    //{
                      //  AkSoundEngine.PostEvent("startEngineSputter", gameObject);
                        //lowHPsoundplayingyet = true;
                   // }

				}
			}


		}
	}
}