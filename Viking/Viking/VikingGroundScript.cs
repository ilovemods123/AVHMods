namespace Viking{
    public class VikingGroundScript:MonoBehaviour{
        public int Speed=25;
        public Animator AnimInstance=null;
        public CharacterController CharControllerInstance=null;
        public List<ParticleSystem>particleSystems=new List<ParticleSystem>();
        public bool attacking=false;
        void Start(){
            AnimInstance=GetComponent<Animator>();
            foreach(ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>()){
                particleSystems.Add(particleSystem);
                particleSystem.Stop();
            }
        }
        void Update(){
            if(Input.GetMouseButton(0)){
                attacking=true;
                if(particleSystems[0].isPlaying==false){
                    foreach(ParticleSystem particleSystem in particleSystems){
                        particleSystem.Play();
                    }
                }
            }
            if(attacking==false){
                if(particleSystems[0].isPlaying==true){
                    foreach(ParticleSystem ps in particleSystems){
                        ps.Stop();
                    }
                }
            }
            attacking=false;
            if(VikingRootScript.Instance.vertInput>0){
                try{
                    if(AnimInstance.GetCurrentAnimatorClipInfo(0)[0].clip.name!="Viking-GroundWalk"){
                        AnimInstance.CrossFade("Viking-GroundWalk",0.1f,0);
                    }
                    Transform GlobalTransform=VikingRootScript.Instance.transform;
                    GlobalTransform.position=GlobalTransform.position+GlobalTransform.forward/Speed;
                }catch{}
            }
            if(VikingRootScript.Instance.horizInput!=0){
                try{
                    if(AnimInstance.GetCurrentAnimatorClipInfo(0)[0].clip.name!="Viking-GroundWalk"){
                        AnimInstance.CrossFade("Viking-GroundWalk",0.1f,0);
                    }
                    VikingRootScript.Instance.transform.Rotate(0,VikingRootScript.Instance.horizInput/5,0,Space.World);
                }catch{}
            }
            if(VikingRootScript.Instance.horizInput==0&&VikingRootScript.Instance.vertInput==0){
                if(Input.GetKeyDown(KeyCode.Space)){
                    VikingRootScript.Instance.audioSource.PlayOneShot(VikingRootScript.Instance.audioClips.First(a=>a.name=="Viking-TransformGTA"));
                    //VikingRootScript.Instance.TransformScript.gameObject.SetActive(true);
                    //VikingRootScript.Instance.TransformScript.animInstance.CrossFade("Viking-GroundToAir",0.1f);
                    //gameObject.SetActive(false);
                }
                try{
                    if(AnimInstance.GetCurrentAnimatorClipInfo(0)[0].clip.name!="Viking-GroundStand"){
                        AnimInstance.CrossFade("Viking-GroundStand",0.1f,0);
                    }
                }catch{}
            }
        }
    }
}
