namespace AVHWarMod{
	public abstract class CustomWeapon:MonoBehaviour{
        public virtual float Range=>0;
		public virtual float Rate=>float.NaN;
        public virtual CustomProjectile Projectile=>null;
		public virtual void Start(){
			//Projectile=
		}
        public void Update(){
			if(!PauseScript.instance.paused){
				float time=0;
				time+=Time.fixedDeltaTime;
				if(time>Rate){
					Transform enemies=ScanForEnemies();
					if(enemies!=null){
						CustomProjectile component2 = gameObject.GetComponent<CustomProjectile>();
						component2.maxVelocity = projectileVelocity;
						component2.penetration = pierce;
						component2.ignoreMask = ignoreMask;
						component2.dir = quaternion;
						component2.Fire(projectileVelocity);
					}
				}
			}
        }
		public Transform ScanForEnemies(){
		    Collider[]array=Physics.OverlapSphere(transform.position,Range,LayerMask.GetMask("Enemy"));
            Log(array.Count());
		    int num=UnityEngine.Random.Range(0,array.Length);
		    if(array.Length!=0&&array[num].gameObject.activeSelf||PauseScript.instance.paused)
		    {
			    if(((bool)array[num].GetComponent<CamoEnemy>()||(bool)array[num].GetComponent<CamoMoabScript>())&&!EquipmentScript.instance.camoDetect)
			    {
				    return null;
			    }
			    return array[num].transform.root;
		    }
		    return null;
        }
    }
}