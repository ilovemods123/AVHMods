namespace BetterCharController{
    public class CustomCharController:MonoBehaviour{
        int walkSpeed=7;
        int runSpeed=10;
        int jumpHeight=10;
        float vertInput=0;
        float horizInput=0;
        float idfk=0;
        public CharacterController charController=null;
        void Update(){
            vertInput=Input.GetAxis("Vertical");
            horizInput=Input.GetAxis("Horizontal");
            charController.SimpleMove(transform.TransformDirection(new Vector3(horizInput*walkSpeed,0,vertInput*walkSpeed)));
            transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X"),0),Space.Self);
            idfk+=Input.GetAxis("Mouse Y");
            idfk=Mathf.Clamp(idfk,-70,50);
            transform.localEulerAngles=new Vector3(-idfk,0,0);
            Cursor.lockState=CursorLockMode.Locked;
        }
        void OnGUI(){
            GUI.Box(new(10,20,150,20),Input.GetAxis("Mouse X")+" "+Input.GetAxis("Mouse Y")+" "+idfk);
        }
    }
}
