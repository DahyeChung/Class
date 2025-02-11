using DB;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    private protected SkillTable skillTable;


    [Header("Health")]
    public int maxHealth;
    public int health = 2;
    public bool shieldActive = true;
    public float shieldRegenTime = 10f;
    private float shieldRegenActualTime = 0;
    public float invincibilityTime = 0f;

    [Header("Firefly")]
    public int maxLuciferin = 3;
    public int luciferin = 3;

    private SkillController _skillController;
    public bool isHide = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _skillController = new SkillController(GetComponent<UI_HUD>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!shieldActive) // if shield is not active, regenerate it
        {
            shieldRegenActualTime -= 1 * Time.deltaTime; //cooldown goes down


            if (shieldRegenActualTime <= 0) // if cooldown is finished, make the shield active
            {
                shieldActive = true; //shield is now active
            }
        }

        //invincibility time

        if (invincibilityTime > 0)
        {
            invincibilityTime -= 1 * Time.deltaTime; //cooldown goes down
        }



        //DEBUG TOOLS


        if (Input.GetKey(KeyCode.P)) //reloads the scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.L)) //makes player take damage
        {
            playerTakeDamage(1);
        }
    }





    public void playerTakeDamage(int amount) //method to make the player take damage
    {
        //play damage animations

        if (invincibilityTime <= 0) //damage is only taken if there is no invincibility time
        {
            if (shieldActive) //if the shield is active, negate one damage and deactivate the shield
            {
                shieldActive = false; //deactivate shield
                amount--; //minus one from attack value
                shieldRegenActualTime = shieldRegenTime;
            }

            health -= amount;

            //invincibility time

            invincibilityTime = .67f;

            //if health reaches 0, the player dies, maybe use 

            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }



    }

    //gaining helath method controled by health items



    public void gainLuciferin(int amount) // add luciferin
    {

        if (luciferin + amount >= maxLuciferin)
        {
            //do nothing, do not collect object
        }
        else
        {
            luciferin += amount;
        }

    }

    public void Scouting1()
    {
        _skillController.SkillShot(1001, transform.position + transform.forward * 5f, transform.rotation, () =>
        {

        });
    }

    public void LumiAbsorption2()
    {
        _skillController.SkillShot(1002, transform.position, Quaternion.identity, () =>
        {

        });
    }

    public void FLashBang3()
    {
        _skillController.SkillShot(1003, transform.position + transform.forward * 1f, transform.rotation, () =>
            {
            }
        );
    }

    public void Skill3End()
    {

    }
}
