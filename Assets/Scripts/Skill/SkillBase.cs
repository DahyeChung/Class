using System;
using System.Collections;
using System.Collections.Generic;
using DB;
using UnityEngine;

public enum SkillDir
{
   Normal,
   Forward,
}

public enum BulletType
{
   Straight,
   Throw,
   Space
}

public class SkillBase : MonoBehaviour
{
   public SkillDir skillDir;
   //public ParticleSystem particleSystem;
   [SerializeField]
   public Skill skillTable;

   protected Rigidbody rigidbody;
   protected Vector3 dir;
   protected ParticleSystem particleSystem;
   protected bool _init = false;

   protected Action callback;
   private SphereCollider _sphereCollider;
   
   private void Awake()
   {
      rigidbody = this.GetComponent<Rigidbody>();
      particleSystem = this.GetComponentInChildren<ParticleSystem>();
      _sphereCollider = this.GetComponent<SphereCollider>();
      
   }

   private void Start()
   {
      
   }

   private void OnEnable()
   {
   
   }

   private void OnDisable()
   {
      
   }
   
   public void SetInfo(Skill skill ,Action call)
   {
      skillTable = skill;
      callback = call;
      
      Init();
      Shot();
   }
   
   public virtual void Init()
   {
      //skillTable = DB.DataTables.GetSkill(skillID);
      if (_sphereCollider)
      {
         _sphereCollider.radius = skillTable.Range;
         _sphereCollider.enabled = true;
      }

      _init = true;
      if(particleSystem)
         particleSystem.Stop();
      if(particleSystem)
         particleSystem.Play();
      
      if (skillTable.Duration > 0)
      {
         StartCoroutine(Duration());
      }
   }
   
   public virtual IEnumerator Duration()
   {
      yield return new WaitForSeconds(skillTable.Duration);
      
      if (_sphereCollider)
      {
         _sphereCollider.enabled = false;
      }
      
      if(callback != null)
         callback.Invoke();
      
      this.gameObject.SetActive(false);
   }
   
   public virtual void Shot()
   {
      
   }
}
