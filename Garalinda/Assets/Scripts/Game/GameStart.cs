using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StartState { Start, Backstory, Busy}

public class GameStart : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public BattleDialogBox box;
    public StartState state=StartState.Start;

    void Update()
    {
     if(state==StartState.Start&&Input.GetKeyDown(KeyCode.X))
        StartCoroutine(StartGame());
    if(state==StartState.Backstory&&Input.GetKeyDown(KeyCode.X))
        StartCoroutine(SwitchScene());
    }

    IEnumerator StartGame(){
        a.SetActive(false);
        b.SetActive(false);  
        box.gameObject.SetActive(true);
        state=StartState.Busy;
        yield return box.TypeDialog("Bienvenido a Garalinda: Rising, en este juego tu objetivo será llegar al final del bosque de los Tutatuás para llegar al castillo, pero cuidado, muchos enemigos te perseguirán. Para luchar contra ellos utiliza a tu fiel equipo de mercenarios, Eli, Kumo, Andre e Inma. ¡Buena suerte! Pulsa X para continuar.");        
        yield return new WaitForSeconds(1f);
        state=StartState.Backstory;
    }

    IEnumerator SwitchScene(){
        yield return SceneManager.LoadSceneAsync(2);
    }
}
