using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viking{
    public class VikingTransformScript:MonoBehaviour{
    public Animator animInstance=null;
    public CharacterController CharControllerInstance=null;
    public string idfk="";
    public GameObject GTATransformThing=null;
    public GameObject ATGTransformThing=null;
    void Start(){
        GTATransformThing=gameObject.GetComponentsInChildren<Transform>().First(a=>a.gameObject.name=="GTATransformThing").gameObject;
        ATGTransformThing=gameObject.GetComponentsInChildren<Transform>().First(a=>a.gameObject.name=="ATGTransformThing").gameObject;
    }
    void ATGStart(){
        GTATransformThing.gameObject.SetActive(false);
        ATGTransformThing.gameObject.SetActive(true);
        idfk="ATG";
    }
    void ATGHideThingy(){
        GTATransformThing.gameObject.SetActive(true);
        ATGTransformThing.gameObject.SetActive(false);
    }
    void ATGFinish(){
        VikingRootScript.Instance.GroundScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
        idfk="";
    }
    void GTAStart(){
        GTATransformThing.gameObject.SetActive(true);
        ATGTransformThing.gameObject.SetActive(false);
        idfk="GTA";
    }
    void GTAHideThingy(){
        GTATransformThing.gameObject.SetActive(false);
        ATGTransformThing.gameObject.SetActive(true);
    }
    void GTAFinish(){
        VikingRootScript.Instance.AirScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
        idfk="";
    }
    void Update(){
        if(idfk=="ATG"){
            if(transform.localPosition.y>=0){
                transform.localPosition=new Vector3(transform.localPosition.x,transform.localPosition.y-0.1f,transform.localPosition.z);
            }
            if(VikingRootScript.Instance.transform.position.y>=0){
                Transform GlobalTransform=VikingRootScript.Instance.transform;
                GlobalTransform.position=new Vector3(GlobalTransform.position.x,GlobalTransform.position.y-0.08f,GlobalTransform.position.z);
            }
        }
        if(idfk=="GTA"){
            if(transform.localPosition.y<=5){
                transform.localPosition=new Vector3(transform.localPosition.x,transform.localPosition.y+0.1f,transform.localPosition.z);
            }
            if(VikingRootScript.Instance.transform.position.y<=25){
                Transform GlobalTransform=VikingRootScript.Instance.transform;
                GlobalTransform.position=new Vector3(GlobalTransform.position.x,GlobalTransform.position.y+0.08f,GlobalTransform.position.z);
            }
        }
    }
    }
}
