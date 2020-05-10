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
        PlayerPrefs.SetInt("curLevel",0);
        weapons.Add("Combat Knife");
        weapons.Add("Combat Knife");
        weapons.Add("Combat Knife");
        weapons.Add("Combat Knife");
        weapons.Add("Harpoon");
        weapons.Add("Raygun");
        weapons.Add("Pistol");
        weapons.Add("Pistol");
        headGear.Add("Mining Helmet");
        headGear.Add("Mining Helmet");
        headGear.Add("Brain Bucket");
        headGear.Add("Brain Bucket");
        chestGear.Add("Burster Core");
        chestGear.Add("Burster Core");
        chestGear.Add("Burster Core");
        chestGear.Add("Hazmat Suit");
        legGear.Add("Basic Greaves");
        legGear.Add("Basic Greaves");
        legGear.Add("Basic Greaves");
        legGear.Add("Basic Greaves");
        feetGear.Add("Traction Shoes");
        feetGear.Add("Traction Shoes");
        feetGear.Add("Traction Shoes");
        feetGear.Add("Combat Boots");
        feetGear.Add("Combat Boots");
        armGear.Add("Combat Grip");
        armGear.Add("Combat Grip");
        armGear.Add("Combat Grip");
        armGear.Add("Combat Grip");
        nextPerson();
    }

    public void selectWeapon1(){
        wSel1 = weapons[weaponOne.value];
    }

    public void selectWeapon2(){
        wSel2 = weapons[weaponTwo.value];
    }
    public void selectHead(){
        hSel = headGear[headArmor.value];
    }
    public void selectChest(){
        cSel = chestGear[chestArmor.value];
    }
   public  void selectArm(){
        aSel = armGear[armArmor.value];
    }
    public void selectLeg(){
        lSel = legGear[legArmor.value];
    }
    public void selectFeet(){
        fSel = feetGear[feetArmor.value];
    }


    public void nextPerson(){
        if (personId > 0){
            //save fields
            PlayerPrefs.SetString(personId+"_w1",wSel1);
            PlayerPrefs.SetString(personId+"_w2",wSel2);
            PlayerPrefs.SetString(personId+"_h",hSel);
            PlayerPrefs.SetString(personId+"_c",cSel);
            PlayerPrefs.SetString(personId+"_a",aSel);
            PlayerPrefs.SetString(personId+"_l",lSel);
            PlayerPrefs.SetString(personId+"_f",fSel);
            PlayerPrefs.Save();
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
        }
        if (personId == 4){
            SceneManager.LoadSceneAsync("SampleScene");
        }else{
            Debug.Log("Managing selections.");
           
            //load stuff into dropdowns
            weaponOne.ClearOptions();
            weaponTwo.ClearOptions();
            headArmor.ClearOptions();
            chestArmor.ClearOptions();
            armArmor.ClearOptions();
            legArmor.ClearOptions();
            feetArmor.ClearOptions();
            List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
            foreach (string s in weapons)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                d.image = WeaponFactory.getWeaponByName(s).getImage();
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
                d.image = ArmorFactory.getArmorByName(s).getImage();
                list.Add(d);
                
            }
            headArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in armGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                d.image = ArmorFactory.getArmorByName(s).getImage();
                list.Add(d);
                
            }
            armArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in chestGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                d.image = ArmorFactory.getArmorByName(s).getImage();
                list.Add(d);
                
            }
            chestArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in legGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                d.image = ArmorFactory.getArmorByName(s).getImage();
                list.Add(d);
                
            }
            legArmor.AddOptions(list);
            //
            list.Clear();
            foreach (string s in feetGear)
            {
                TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData();
                d.text = s;
                d.image = ArmorFactory.getArmorByName(s).getImage();
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
