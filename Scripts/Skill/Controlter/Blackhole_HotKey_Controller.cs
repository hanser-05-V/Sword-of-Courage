using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blackhole_HotKey_Controller : MonoBehaviour
{
    private KeyCode myHotKey; //记录传入的 热键 
    private TextMeshProUGUI myText;

    private Transform enemy;
    private Blackhole_Skill_Controller myBlackHole;
    private SpriteRenderer sr;

    public void SetupHotKey(KeyCode _myHotKey,Transform _enemy,Blackhole_Skill_Controller _myBlackHole)
    {
        myText = this.GetComponentInChildren<TextMeshProUGUI>();
        sr = this.GetComponent<SpriteRenderer>();   

        enemy = _enemy;
        myBlackHole = _myBlackHole;   

        myHotKey = _myHotKey;
        myText.text = _myHotKey.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(myHotKey))
        {
            myBlackHole.AddEnemyToList(enemy);
            sr.color = Color.clear;
            myText.color = Color.clear;

        }
    }
}
