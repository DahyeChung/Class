using System.Collections;
using UnityEngine;

public class SkillFlashBang : SkillBase
{
    private ConeCollider _coneCollider;
    private MeshCollider _meshCollider;
    public override void Init()
    {
        if (_coneCollider == null)
            _coneCollider = this.GetComponent<ConeCollider>();

        if (_coneCollider)
        {
            _coneCollider.distance = skillTable.Range;
            _coneCollider.Init();

            if (_meshCollider == null)
                _meshCollider = GetComponent<MeshCollider>();
            if (_meshCollider)
                _meshCollider.enabled = true;
        }

        base.Init();
    }

    public override IEnumerator Duration()
    {
        yield return new WaitForSeconds(skillTable.Duration);

        if (_meshCollider)
        {
            _meshCollider.enabled = false;
        }

        if (callback != null)
            callback.Invoke();

        this.gameObject.SetActive(false);
    }
    public override void Shot()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            // TODO : Enemy stun 
            //if(other.gameObject.GetComponent<Enemy>())
            //other.gameObject.GetComponent<Enemy>().TakeStun();
            Debug.Log("other Name : " + other.gameObject.name);
        }
        //오브젝트꺼짐
    }


    private void Update()
    {

    }
}
