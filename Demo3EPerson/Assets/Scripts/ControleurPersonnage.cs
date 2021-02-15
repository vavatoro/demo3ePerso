using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Gestion des déplacements et des rotations du personnage à l'aide des touches en modifiant la velocité 
 * Gestion des animation du prsaonnage
 * @author Vahik Toroussian
 * @version 2018-03-13
*/


public class ControleurPersonnage : MonoBehaviour
{


    public float vitesseDeplacement = 4f;             //Vitesse de déplacement vers l'avant
    public int vietsseRotation = 2;               //Vitesse de rotation du prsonnage
    public float vitesseSaut = 10f;            //Vitesse de saut du prsonnage
    public bool auSol;                          //le personnage est au sol ou pas
    private float vDeplacement;                 // le déplacement à appliquer au personnage selon les touches

    Rigidbody rigidbodyPerso;                   //Référence au Rigidbody du personnage
    Animator animPerso; 

    //Mettre en mémoire la réference au Rigidbody 
    void Start()
    {
        rigidbodyPerso = GetComponent<Rigidbody>();
        animPerso = GetComponent<Animator>();
    }

    //Contrôle les déplacemnts du personnage, sa rotation et ses animaitons 

    void Update()
    {
        // Tourne le personnage à l'aide des touches horizontales (a,d et fléches G et D)
        var rotationPerso = Input.GetAxis("Mouse X") * vietsseRotation;

        transform.Rotate(0, rotationPerso, 0);

        vDeplacement = Input.GetAxis("Vertical") * vitesseDeplacement;

        if (vDeplacement > 0)
        {
            rigidbodyPerso.velocity = (transform.forward * vDeplacement) + new Vector3(0, rigidbodyPerso.velocity.y, 0);     //donne une velocité dans l'axe de Z  de l'obje;
        }
        if (Input.GetKeyDown(KeyCode.Space) && auSol)
        {
            rigidbodyPerso.velocity += new Vector3(0, vitesseSaut,0);
        }

        AnimerPersonnage();
        
    }

    void AnimerPersonnage()
    {
        animPerso.SetFloat("vitesseDep", vDeplacement);

        RaycastHit hit;
        auSol = Physics.SphereCast(transform.position + new Vector3(0, 0.5f, 0), 0.2f, -Vector3.up, out hit, .8f);

        animPerso.SetBool("animeSaut", !auSol);
    }
}