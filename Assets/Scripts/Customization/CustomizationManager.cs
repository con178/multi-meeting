using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gender;
    [SerializeField] private Material[] skin;
    [SerializeField] private Material[] hair;
    [SerializeField] private Material[] shirt;
    [SerializeField] private Material[] pants;
    


    [SerializeField] private GameObject maleHead;
    [SerializeField] private GameObject maleBody;
    [SerializeField] private GameObject malePants;


    [SerializeField] private GameObject femaleHead;
    [SerializeField] private GameObject femaleBody;
    [SerializeField] private GameObject femalePants;


    private SkinnedMeshRenderer rendererMaleHead;
    private SkinnedMeshRenderer rendererMaleBody;
    private SkinnedMeshRenderer rendererMalePants;


    private SkinnedMeshRenderer rendererFemaleHead;
    private SkinnedMeshRenderer rendererFemaleBody;
    private SkinnedMeshRenderer rendererFemalePants;


    private int currentSkin;
    private int currentHair;
    private int currentShirt;
    private int currentPants;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        gameManager.Load();


        rendererMaleHead = maleHead.GetComponent<SkinnedMeshRenderer>();
        rendererMaleBody = maleBody.GetComponent<SkinnedMeshRenderer>();
        rendererMalePants = malePants.GetComponent<SkinnedMeshRenderer>();

        rendererFemaleHead = femaleHead.GetComponent<SkinnedMeshRenderer>();
        rendererFemaleBody = femaleBody.GetComponent<SkinnedMeshRenderer>();
        rendererFemalePants = femalePants.GetComponent<SkinnedMeshRenderer>();

        currentHair = -1;
        currentPants = -1;
        currentShirt = -1;
        currentSkin = -1;
        Skin_Up();
        Hair_Up();
        Shirt_Up();
        Pants_Up();
        




        if (gameManager.genderID == 0)
        {
            gender[0].SetActive(true);
            gender[1].SetActive(false);
        }
        else
        {
            gender[0].SetActive(false);
            gender[1].SetActive(true);
        }



        
    }

    



    public void ChangeGender_ToFemale()
    {
        if (gameManager.genderID == 1)
        {
            gameManager.genderID = 0;
            gender[0].SetActive(true);
            gender[1].SetActive(false);
        }
    }
    public void ChangeGender_ToMale()
    {
        if (gameManager.genderID == 0)
        {
            gameManager.genderID = 1;
            gender[1].SetActive(true);
            gender[0].SetActive(false);
        }
        
    }




    public void Skin_Up()
    {
        if(gameManager.genderID == 0)
        {
            Material[] materialsHead = rendererFemaleHead.materials;
            Material[] materialsBody = rendererFemaleBody.materials;

            currentSkin++;
            if (currentSkin > skin.Length - 1)
            {
                currentSkin = 0;
            }
            materialsHead[0] = skin[currentSkin];
            materialsBody[1] = skin[currentSkin];

            rendererFemaleHead.materials = materialsHead;
            rendererFemaleBody.materials = materialsBody;
        }
        else if (gameManager.genderID == 1)
        {
            Material[] materialsHead = rendererMaleHead.materials;
            Material[] materialsBody = rendererMaleBody.materials;

            currentSkin++;
            if (currentSkin > skin.Length - 1)
            {
                currentSkin = 0;
            }
            materialsHead[0] = skin[currentSkin];
            materialsBody[1] = skin[currentSkin];

            rendererMaleHead.materials = materialsHead;
            rendererMaleBody.materials = materialsBody;
        }
    }


    public void Skin_Down()
    {
        if(gameManager.genderID == 0)
        {
            Material[] materialsHead = rendererFemaleHead.materials;
            Material[] materialsBody = rendererFemaleBody.materials;

            currentSkin--;
            if (currentSkin < 0)
            {
                currentSkin = skin.Length - 1;
            }
            materialsHead[0] = skin[currentSkin];
            materialsBody[1] = skin[currentSkin];

            rendererFemaleHead.materials = materialsHead;
            rendererMaleBody.materials = materialsBody;
        }
        else if(gameManager.genderID == 1)
        {
            Material[] materialsHead = rendererMaleHead.materials;
            Material[] materialsBody = rendererMaleBody.materials;

            currentSkin--;
            if (currentSkin < 0)
            {
                currentSkin = skin.Length - 1;
            }
            materialsHead[0] = skin[currentSkin];
            materialsBody[1] = skin[currentSkin];

            rendererMaleHead.materials = materialsHead;
            rendererMaleBody.materials = materialsBody;
        }
    }



    public void Hair_Up()
    {
        if (gameManager.genderID == 0)
        {
            Material[] materialsHead = rendererFemaleHead.materials;

            currentHair++;
            if (currentHair > hair.Length - 1)
            {
                currentHair = 0;
            }
            materialsHead[1] = hair[currentHair];

            rendererFemaleHead.materials = materialsHead;
        }
        else if (gameManager.genderID == 1)
        {
            Material[] materialsHead = rendererMaleHead.materials;

            currentHair++;
            if (currentHair > hair.Length - 1)
            {
                currentHair = 0;
            }
            materialsHead[1] = hair[currentHair];

            rendererMaleHead.materials = materialsHead;
        }
    }


    public void Hair_Down()
    {
        if (gameManager.genderID == 0)
        {
            Material[] materialsHead = rendererFemaleHead.materials;

            currentHair--;
            if (currentHair < 0)
            {
                currentHair = hair.Length - 1;
            }
            materialsHead[1] = hair[currentHair];

            rendererFemaleHead.materials = materialsHead;

        }
        else if (gameManager.genderID == 1)
        {
            Material[] materialsHead = rendererMaleHead.materials;

            currentHair--;
            if (currentHair < 0)
            {
                currentHair = hair.Length - 1;
            }
            materialsHead[1] = hair[currentHair];

            rendererMaleHead.materials = materialsHead;
        }
    }





    public void Shirt_Up()
    {
        if (gameManager.genderID == 0)
        {
            Material[] materialsBody = rendererFemaleBody.materials;

            currentShirt++;
            if (currentShirt > shirt.Length - 1)
            {
                currentShirt = 0;
            }
            materialsBody[0] = shirt[currentShirt];

            rendererFemaleBody.materials = materialsBody;
        }
        else if (gameManager.genderID == 1)
        {
            Material[] materialsBody = rendererMaleBody.materials;

            currentShirt++;
            if (currentShirt > shirt.Length - 1)
            {
                currentShirt = 0;
            }
            materialsBody[0] = shirt[currentShirt];

            rendererMaleBody.materials = materialsBody;
        }
    }


    public void Shirt_Down()
    {
        if (gameManager.genderID == 0)
        {
            Material[] materialsBody = rendererFemaleBody.materials;

            currentShirt--;
            if (currentShirt < 0)
            {
                currentShirt = hair.Length - 1;
            }
            materialsBody[0] = shirt[currentShirt];

            rendererFemaleBody.materials = materialsBody;

        }
        else if (gameManager.genderID == 1)
        {
            Material[] materialsBody = rendererMaleBody.materials;

            currentShirt--;
            if (currentShirt < 0)
            {
                currentShirt = shirt.Length - 1;
            }
            materialsBody[0] = shirt[currentShirt];

            rendererMaleBody.materials = materialsBody;
        }
    }









    public void Pants_Up()
    {
        if (gameManager.genderID == 0)
        {
            Material[] materialsPants = rendererFemalePants.materials;

            currentPants++;
            if (currentPants > pants.Length - 1)
            {
                currentPants = 0;
            }
            materialsPants[0] = pants[currentPants];

            rendererFemalePants.materials = materialsPants;
        }
        else if (gameManager.genderID == 1)
        {
            Material[] materialsPants = rendererMalePants.materials;

            currentPants++;
            if (currentPants > pants.Length - 1)
            {
                currentPants = 0;
            }
            materialsPants[0] = pants[currentPants];

            rendererMalePants.materials = materialsPants;
        }
    }


    public void Pants_Down()
    {
        if (gameManager.genderID == 0)
        {
            Material[] materialsPants = rendererFemalePants.materials;

            currentPants--;
            if (currentPants < 0)
            {
                currentPants = pants.Length - 1;
            }
            materialsPants[0] = pants[currentPants];

            rendererFemalePants.materials = materialsPants;

        }
        else if (gameManager.genderID == 1)
        {
            Material[] materialsPants = rendererMalePants.materials;

            currentPants--;
            if (currentPants < 0)
            {
                currentPants = pants.Length - 1;
            }
            materialsPants[0] = pants[currentPants];

            rendererMalePants.materials = materialsPants;
        }
    }


    public void ApplyButton()
    {
        gameManager.Save();
        SceneManager.LoadScene("Menu");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
