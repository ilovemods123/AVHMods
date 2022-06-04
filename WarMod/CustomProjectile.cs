namespace AVHWarMod{
    public class CustomProjectile:MonoBehaviour{
		public int predictionStepsPerFrame=1;
		public Vector3 projectileVelocity;
		public int Pierce;
		public float maxVelocity;
		public float hitRadius;
		public float lifeTime;
		public LayerMask ignoreMask;
		public bool Gravity=true;
		public Quaternion dir;
		public List<Transform>hitEnemies=new List<Transform>();
		public int pen;
		public float timeLeft;
		public GameObject proj;
        public void Fire(float speed){
			pen=Pierce;
			projectileVelocity=transform.forward*speed;
			hitEnemies=new List<Transform>();
			hitEnemies.Clear();
			timeLeft=lifeTime;
		}
        public void Update(){
            if(PauseScript.instance.paused)return;
			Vector3 vector=transform.position;
			float num=1f/predictionStepsPerFrame;
			for(float num2=0f;num2<1f;num2+=num){
				if(Gravity)projectileVelocity+=Physics.gravity*num*Time.deltaTime;
				Vector3 vector2=vector+projectileVelocity*num*Time.deltaTime;
				Ray ray=new Ray(vector,vector2-vector);
				RaycastHit[]array=(!(hitRadius<=0f))?Physics.SphereCastAll(ray,hitRadius,(vector2-vector).magnitude,~(int)ignoreMask):Physics.RaycastAll(ray,(vector2-vector).magnitude,~(int)ignoreMask);
				Array.Sort(array,(RaycastHit x,RaycastHit y)=>x.distance.CompareTo(y.distance));
				RaycastHit[]array2=array;
				for(int i=0;i<array2.Length;i++){
					RaycastHit hit=array2[i];
					if(pen>0&&!hitEnemies.Contains(hit.transform)){
						Log(hit.transform.name);
						hitEnemies.Add(hit.transform);
						pen--;
						transform.rotation=dir;
						projectileVelocity=transform.forward*projectileVelocity.magnitude;
					}
					if(pen<=0){
						projectileVelocity=Vector3.zero;
						Destroy(gameObject);
						return;
					}
				}
				vector=vector2;
				transform.position=vector;
			}
			timeLeft-=Time.deltaTime;
			if(timeLeft<=0f)Destroy(gameObject);
		}
    }
}
