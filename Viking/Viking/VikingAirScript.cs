namespace Viking{
    public class VikingAirScript:MonoBehaviour{
        public CharacterController CharControllerInstance=null;
        public float Speed=20;
        void Start(){
            CharControllerInstance=VikingRootScript.Instance.CharControllerInstance;
        }
        void Update(){
            if(VikingRootScript.Instance.vertInput>0){
                Speed/=1.5f;
            }
            if(VikingRootScript.Instance.horizInput!=0){
                VikingRootScript.Instance.transform.Rotate(0,VikingRootScript.Instance.horizInput/5,0,Space.World);
            }
            if(VikingRootScript.Instance.horizInput==0&&VikingRootScript.Instance.vertInput==0){
                if(Input.GetKeyDown(KeyCode.Space)){
                    VikingRootScript.Instance.audioSource.PlayOneShot(VikingRootScript.Instance.audioClips.First(a=>a.name=="Viking-TransformATG"));
                    VikingRootScript.Instance.TransformScript.gameObject.SetActive(true);
                    VikingRootScript.Instance.TransformScript.animInstance.CrossFade("Viking-AirToGround",0.1f);
                    gameObject.SetActive(false);
                }
            }
            Transform GlobalTransform=VikingRootScript.Instance.transform;
            GlobalTransform.position=GlobalTransform.position+GlobalTransform.forward/Speed;
        }
    }
}