using UnityEngine;
using System.Collections;

namespace HWRWeaponSystem
{
	public class DamageManager : MonoBehaviour
	{
		public GameObject Effect;
		public int HP = 100;
		private ObjectPool objPool;
		[HideInInspector]
		public GameObject LatestHit;
       
        private bool alreadynotifiedplayer = false;
		private void Awake ()
		{
			objPool = this.GetComponent<ObjectPool> ();	
		}

		private void Start ()
		{
           

		}
      

		public void ApplyDamage (int damage)
		{
            if (HP < 0)
            {
             
                return;
            }

			HP -= damage;
			if (HP <= 0) {
				Dead ();
  
			}

		}

		public void ApplyDamage (DamagePack damage)
		{
            if (HP < 0)
            {
                
                return;
            }

			LatestHit = damage.Owner;
			HP -= damage.Damage;
			if (HP <= 0) {
              
				Dead ();
			}

		}

		public void Dead ()
		{
			//print ("Game object died : " + gameObject.name);
			if (gameObject.name =="WW2AIFriend(Clone)" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);

			}
			if (gameObject.name =="WW2ADead(Clone)" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);

			}
			if (gameObject.name =="FighterAI(Clone)" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);
				//print ("enemy Died");
			}
			if (gameObject.name =="FighterAIFriend(Clone)" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);
				//print ("friendly Died");
			}
			if (gameObject.name =="X_Fighter(Clone)" )
			{
				AkSoundEngine.PostEvent ("normalExplosion", gameObject);

			}
			if (gameObject.name =="Xfighter_Dead(Clone)" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);

			}
			if (gameObject.name =="V_Fighter_AI(Clone)" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);

			}
			if (gameObject.name =="V_Fighter" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);

			}
			if (gameObject.name =="Fighter" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);

			}
			if (gameObject.name =="WW2" )
			{
				AkSoundEngine.PostEvent ("metalExplosion", gameObject);

			}
			if (Effect) {
				GameObject deadobj = null;
				if (WeaponSystem.Pool != null && Effect.GetComponent<ObjectPool> ()) {
					deadobj = (GameObject)WeaponSystem.Pool.Instantiate (Effect, transform.position, transform.rotation);
				} else {
					deadobj = (GameObject)GameObject.Instantiate (Effect, transform.position, transform.rotation);
				}
				if(deadobj!=null){
					if(deadobj.GetComponent<Rigidbody>() != null && this.GetComponent<Rigidbody>() != null){
						deadobj.GetComponent<Rigidbody>().AddForce(this.GetComponent<Rigidbody>().velocity,ForceMode.VelocityChange);
					}
				}
			}
			if (objPool != null) {
				objPool.Destroying ();
			} else {
				Destroy (this.gameObject);
			}
			this.gameObject.SendMessage ("OnDead", SendMessageOptions.DontRequireReceiver);
		}

	}
}
