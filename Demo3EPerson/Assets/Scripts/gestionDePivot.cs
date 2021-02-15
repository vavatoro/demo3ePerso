using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionDePivot : MonoBehaviour
{

    public GameObject personnage;
    public GameObject camera3ePerso;      //La caméra
    public GameObject positionRayCast;    // mémorise la position de lancement du RayCast de la caméra

    public float hauteur;      // Ajustement de la position du pivot en hauteur                

    public float distanceCameraLoin = -4;       //distance normal
    public float distanceCameraPret = -0.5f;    //distance quand on zoome

    void Start()
    {

    }

    // Positionne le pivot de la caméra au même endroit que le personnage + une certaine hauteur (prêt de la tête)
    // le pivot tourne par le déplacement de la souris en X et Y

    void Update()
    {//

        transform.position = personnage.transform.position + Vector3.up * hauteur;

        transform.Rotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

        //si on veut zoomer lorsqu'un objet est entre la caméra et le personnage
        if (Physics.Raycast(positionRayCast.transform.position, positionRayCast.transform.forward, 4f))
        {
            camera3ePerso.transform.localPosition = new Vector3(0, 0, distanceCameraPret);
        }
        else
        {
            camera3ePerso.transform.localPosition = new Vector3(0, 1, distanceCameraLoin);
        }
    }
}
