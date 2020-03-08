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
    private List<string> weapons = new List<string>(),headGear = new List<string>(),chestGear = new List<string>()
    ,armGear = new List<string>(),legGear = new List<string>(),feetGear = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        weapons.Add("Combat Knife");
        weapons.Add("Combat Knife");
        weapons.Add("Combat Knife");
        weapons.Add("Combat Knife");
        nextPerson();
    }

    void selectWeapon1(){
        wSel1 = weapons[weaponOne.value];
    }

    void selectWeapon2(){
        wSel2 = weapons[weaponTwo.value];
    }
    void selectHead(){
        hSel = headGear[headArmor.value];
    }
    void selectChest(){
        cSel = chestGear[chestArmor.value];
    }
    void selectArm(){
        aSel = armGear[armArmor.value];
    }
    void selectLeg(){
        lSel = legGear[legArmor.value];
    }
    void selectFeet(){
        fSel = feetGear[feetArmor.value];
    }


    public void nextPerson(){
        if (personId > 0){
            //save fields
            PlayerPrefs.SetString(personId+"_w1",wSel1);
            PlayerPrefs.SetString(personId+"_w2",wSel1);
            PlayerPrefs.SetString(personId+"_h",hSel);
            PlayerPrefs.SetString(personId+"_c",cSel);
            PlayerPrefs.SetString(personId+"_a",aSel);
            PlayerPrefs.SetString(personId+"_l",lSel);
            PlayerPrefs.SetString(personId+"_f",fSel);
            PlayerPrefs.Save();
        }
        if (personId == 4){
            SceneManager.LoadSceneAsync("SampleScene");
        }else{
            //remove stuff from lists
            weapons.Remove(wSel1);
            if (!wSel1.Equals(wSel2)){
                weapons.Remove(wSel2);
            }
            headGear.Remove(hSel);
            chestGear.Remove(cSel);
            armGear.Remove(aSel);
            legGear.Remove(lSel);
            feetGear.Remove(fSel);
            //load stuff into dropdowns
            List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
            foreach (string s in weapons)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                list.Add(d);
                
            }
            weaponOne.AddOptions(list);
            weaponTwo.AddOptions(list);
            //
            list.Clear();
            foreach (string s in headGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                list.Add(d);
                
            }
            headArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in armGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                list.Add(d);
                
            }
            armArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in chestGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                list.Add(d);
                
            }
            chestArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in legGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                list.Add(d);
                
            }
            legArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in feetGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                list.Add(d);
                
            }
            feetArmor.AddOptions(list);
            //

            //select first thing in each dropdown
            selectWeapon1();
            selectWeapon2();
            selectHead();
            selectChest();
            selectArm();
            selectLeg();
            selectFeet();
            personId++;
            }
    }
}
