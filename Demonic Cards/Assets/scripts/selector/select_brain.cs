using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class select_brain : MonoBehaviour
{
    public TMP_Dropdown weaponOne, weaponTwo, headArmor,chestArmor,armArmor,legArmor,feetArmor,classHolder,backstoryHolder;
    private int personId = 0;
    private string wSel1, wSel2, hSel, cSel,aSel,lSel,fSel;
    private List<string> weapons = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        nextPerson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void selectWeapon1(){
        wSel1 = weapons[weaponOne.value];
    }

    void selectWeapon2(){
        wSel2 = weapons[weaponTwo.value];
    }

    public void nextPerson(){
        if (personId > 0){
            //save fields

        
        }
        if (personId == 4){
            SceneManager.LoadSceneAsync("SampleScene");
        }else{
            //remove stuff from lists
            weapons.Remove(wSel1);
            if (!wSel1.Equals(wSel2)){
                weapons.Remove(wSel2)
            }
            //load stuff into dropdowns
            personId++;
            }
    }
}
