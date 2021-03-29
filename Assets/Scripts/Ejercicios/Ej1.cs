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
        switch (ejercicio)
        {
            case 1:
                Uno();
                break;
            case 2:
                Dos();
                break;
            case 3:
                Tres();
                break;
            case 4:
                Cuatro();
                break;
            case 5:
                Cinco();
                break;
            case 6:
                Seis();
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
    void Dos() //COMPLETO
    {
        rojo = azul - blanco;
    }
    void Tres() //COMPLETO
    {
        rojo = azul;
        rojo.Scale(blanco);
    }
    void Cuatro() //COMPLETO
    {
        rojo = Vec3.Cross(azul, blanco);
    }

    float timer;
    void Cinco() //COMPLETO
    {
        rojo = Vec3.Lerp(azul, blanco, timer);
        timer += 0.2f * Time.deltaTime;
        if (Vec3.SqrMagnitude(rojo - azul) >= Vec3.SqrMagnitude(blanco - azul))
        {
            timer = 0;
        }
    }

    void Seis() //COMPLETO
    {
        rojo = Vec3.Max(azul, blanco);
    }
}
