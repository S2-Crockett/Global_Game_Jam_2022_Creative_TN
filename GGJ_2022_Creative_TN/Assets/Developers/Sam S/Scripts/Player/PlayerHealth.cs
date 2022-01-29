using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")] 
    public int health;
    private bool _damageTaken;

    private void Awake()
    {
        UIManager.instance.healthUI.InitHealth(health);
    }

    public void IncreaseHealth(int amount)
    {
        health += amount;
        UIManager.instance.healthUI.AddHealth(amount);
    }

    public void DecreaseHealth(int amount)
    {
        if (!_damageTaken)
        {
            _damageTaken = true;
            StartCoroutine(ResetInvunerablePeriod(0.1f));

            health -= amount;
            if (health <= 0)
            {
                Die();
                
                UIManager.instance.healthUI.RemoveHealth(amount);
            }
            else
            {
                UIManager.instance.healthUI.RemoveHealth(amount);
            }
        }
    }
    
    public void Die()
    {
        Debug.Log("DIE");
        GameManager.instance.UpdateGameState(GameState.Lose);
        WaypointManager.instance.Respawn(GameObject.FindWithTag("Player").transform);
        WaypointManager.instance.Respawn(GameObject.FindWithTag("Player Two").transform);
    }

    IEnumerator ResetInvunerablePeriod(float time)
    {
        yield return new WaitForSeconds(time);
        _damageTaken = false;
    }
}