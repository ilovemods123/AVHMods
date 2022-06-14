namespace Viking{
    public class VikingRootScript:MonoBehaviour{
        public List<AudioClip>audioClips=new List<AudioClip>();
        public VikingGroundScript GroundScript=null;
        public VikingTransformScript TransformScript=null;
        public VikingAirScript AirScript=null;
        public AudioSource audioSource=null;
        public static VikingRootScript Instance=null;
        public float horizInput=0;
        public float vertInput=0;
        public CharacterController CharControllerInstance=null;
        void Update(){
            horizInput=Input.GetAxis("Horizontal");
            vertInput=Input.GetAxis("Vertical");
        }
        void OnEnable(){
            Instance=this;
            GetComponent<Camera>().gameObject.SetActive(true);
        }
        void OnDisable(){
            GetComponent<Camera>().gameObject.SetActive(false);
        }
    }
}
