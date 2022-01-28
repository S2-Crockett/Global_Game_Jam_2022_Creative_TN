using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
   [Header("References")] 
   public GameObject healthObject;

   private List<GameObject> _healthObjects = new List<GameObject>();
   private int _health;
   
   public void InitHealth(int healthAmount)
   {
      _health = healthAmount;
      Debug.Log(_health);
      
      // create all of the health objects
      int xpos = 48;
      for (int i = 0; i < _health; i++)
      {
         Debug.Log(i);
         GameObject go =Instantiate(healthObject, new Vector3(this.transform.position.x + xpos, 
            this.transform.position.y, 0), Quaternion.identity);
         go.transform.SetParent(this.transform);
         
         _healthObjects.Add(go);
         
         xpos += 48;
      }
   }

   public void AddHealth(int amount)
   {
      _health += amount;

      int length = _healthObjects.Count * 48;
      GameObject go = Instantiate(healthObject, new Vector3(this.transform.position.x + length, 
         this.transform.position.y, 0), Quaternion.identity);
      go.transform.SetParent(this.transform);
      _healthObjects.Add(go);
   }

   public void RemoveHealth(int amount)
   {
      _health -= amount;
      RemoveLastHealthObject();
   }
   
   private void RemoveLastHealthObject()
   {
      Destroy(_healthObjects[_healthObjects.Count]);
   }
}
