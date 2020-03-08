using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class select_brain : MonoBehaviour
{
    public TMP_Dropdown weaponOne, weaponTwo, headArmor,chestArmor,armArmor,legArmor,feetArmor,classHolder,backstoryHolder;
    private int personId = 0;
    private Weapon wSel1, wSel2;
    private Armor hSel, cSel,aSel,lSel,fSel;
    // Start is called before the first frame update
    void Start()
    {
        nextPerson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextPerson(){
        if (personId > 0){
            //save fields

        
        }
        if (personId == 4){
            SceneManager.LoadSceneAsync("SampleScene");
        }else{
        //load stuff into dropdowns
        personId++;}
    }
}
