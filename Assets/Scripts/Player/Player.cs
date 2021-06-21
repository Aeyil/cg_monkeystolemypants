using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    public Slider healthBar;
    public int Health = 100;
    public int Damage = 10;
    public int Armor = 0;
    Animator animator;
    float waitTime;
    float startTime;
    bool helper;
    bool helper2;
    public GameObject deathText;
    void Start()
    {
        animator = GameObject.Find("LevelChanger").GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        Health = PlayerInfo.Health;
        Damage = PlayerInfo.Damage;
        Armor = PlayerInfo.Armor;
        healthBar.maxValue = PlayerInfo.Health;
        startTime = Time.time;
        waitTime = 4f;
        helper = false;
        helper2 = false;
    }

    public void Update(){
        healthBar.value = Health;

        if (!helper && Health <= 0)
        {
            startTime = Time.time;
            Debug.Log("HEALTH <= 0");
            helper = true;
        }
        if (!helper2 && Time.time > (startTime + waitTime) && Health <= 0)
        {
            animator.SetTrigger("fadeOut");
            Debug.Log("FADE");
            helper2 = true;

        }
        if (helper2 && Time.time > (startTime + waitTime + 2f) && Health <= 0)
        {
            // set text

            deathText.SetActive(true);
        }
        if (helper2 && Time.time > (startTime + waitTime + 4f) && Health <= 0)
        {
            FadeToBase();
        }
    }

    public void TakeDamage(int damageTaken){
        if(playerMovement.canBeHit){
            if(Health - damageTaken <= 0){
                Health = 0;
                playerMovement.Die();
            }
            else{
                Health -= damageTaken;
                playerMovement.GetHit();
            }
        }
    }
    public void FadeToBase()
    {
        WorldInfo.Initialize();
        SceneLoaderInfo.sceneId = 2;
        SceneManager.LoadScene(1);

    }
}
