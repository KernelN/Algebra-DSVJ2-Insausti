using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;

public class Ej1 : MonoBehaviour
{
    Vec3 rojo;
    public Vec3 azul;
    public Vec3 blanco;

    public int ejercicio;
    int ejActual;

    void Start()
    {
        Vector3Debugger.AddVector(rojo, Color.red, "elRojo");
        Vector3Debugger.EnableEditorView("elRojo");
        Vector3Debugger.AddVector(azul, Color.blue, "elAzul");
        Vector3Debugger.EnableEditorView("elAzul");
        Vector3Debugger.AddVector(blanco, Color.white, "elBlanco");
        Vector3Debugger.EnableEditorView("elBlanco");
    }

    // Update is called once per frame
    void Update()
    {
        if (ejActual != ejercicio)
        {
            ejActual = ejercicio;
            rojo = new Vec3(0, 0, 0);
        }

        switch (ejercicio)
        {
            case 1:
                Uno();
                break;
            case 2:
                Uno();
                break;
            case 3:
                Uno();
                break;
            case 4:
                Uno();
                break;
            case 5:
                Uno();
                break;
            case 6:
                Uno();
                break;
            case 7:
                Uno();
                break;
            case 8:
                Uno();
                break;
            case 9:
                Uno();
                break;
            case 10:
                Uno();
                break;
            default:
                break;
        }

        Vector3Debugger.UpdatePosition("elRojo", rojo);
        Vector3Debugger.UpdatePosition("elAzul", azul);
        Vector3Debugger.UpdatePosition("elBlanco", blanco);
    }

    void Uno() //COMPLETO
    {
        rojo = azul + blanco;
    }
}
