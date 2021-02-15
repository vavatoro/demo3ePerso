using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Gestion des déplacements à l'aide des touches et orientation selon la direction de la caméra
 * Gestion des animation du prsaonnage
 * @author Vahik Toroussian
 * @version 2021-01-23
*/


public class deplacePersonnageSelonCamera : MonoBehaviour
{

    public float vitesseDeplacement = 4f;             //Vitesse de déplacement vers l'avant
    public int vietsseRotation = 2;               //Vitesse de rotation du prsonnage
    public float vitesseSaut = 10f;            //Vitesse de saut du prsonnage
    public bool auSol;                          //le personnage est au sol ou pas
    private float vDeplacement;                 // le déplacement à appliquer au personnage selon les touches

    Rigidbody rigidbodyPerso;                   //Référence au Rigidbody du personnage
    Animator animPerso;

    public GameObject camera3ePerso;

    //Mettre en mémoire la réference au Rigidbody 
    void Start()
    {
        rigidbodyPerso = GetComponent<Rigidbody>();
        animPerso = GetComponent<Animator>();
    }

    //Contrôle les déplacemnts du personnage, sa rotation et ses animaitons 

    void Update()
    {

        // obtient les valeurs des touches horizontales et verticales 
        float hDeplacement = Input.GetAxisRaw("Horizontal");
        float vDeplacement = Input.GetAxisRaw("Vertical");

        //obtient la nouvelle direction (      (avant/arrièrre)                                        +                               (gauche/droite) )
        Vector3 directionDep = camera3ePerso.transform.forward * vDeplacement + camera3ePerso.transform.right * hDeplacement;

        directionDep.y = 0;   //pas de valeur en y , le cas où la caméra regarde vers le bas ou vers le haut

        if (directionDep != Vector3.zero)       //change de direction s’il y a un changement
        {
            //Oriente le personnage vers la direction de déplacement, et applique la vélocité dans la même direction
            transform.forward = directionDep;
            rigidbodyPerso.velocity = (transform.forward * vitesseDeplacement) + new Vector3(0, rigidbodyPerso.velocity.y, 0);
        }
        else
        {
            rigidbodyPerso.velocity = new Vector3(0, rigidbodyPerso.velocity.y, 0);
        }


        if (Input.GetKeyDown(KeyCode.Space) && auSol)
        {
            rigidbodyPerso.velocity += new Vector3(0, vitesseSaut, 0);
        }

        AnimerPersonnage();

    }

    void AnimerPersonnage()
    {
        animPerso.SetFloat("vitesseDep", rigidbodyPerso.velocity.magnitude);// Mathf.Abs(rigidbodyPerso.velocity.z) + Mathf.Abs(rigidbodyPerso.velocity.x));

        RaycastHit hit;
        auSol = Physics.SphereCast(transform.position + new Vector3(0, 0.5f, 0), 0.2f, -Vector3.up, out hit, .8f);

        animPerso.SetBool("animeSaut", !auSol);
    }
}