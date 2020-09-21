using UnityEngine;

public class LivingEntity:MonoBehaviour{

    public Species species;
    public Vector3 coord;
    protected bool dead;

    public virtual void Init(Vector3 coord){
        this.coord =  coord;
    }
    //Die function



}
