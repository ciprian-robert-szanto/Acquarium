using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    [SerializeField] private string titleDialog;
    [SerializeField] private float dist;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Vector2[] pathToFollow;
    private int pointToFollow;

    [SerializeField] private string[] textToShow;
    [SerializeField] private GameObject rewardObject;
    private int counterText = 0;

    private bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
        pointToFollow = 0;
        PlayerInput.Instance.OnInteractPerformed += Instance_OnInteractPerformed;
    }

    private void Instance_OnInteractPerformed(object sender, System.EventArgs e) {
        if (CanInteract()) {
            if (!PlayerInput.Instance.isInteracting) {
                this.isInteracting = true;
                PlayerInput.Instance.isInteracting = true;
                PlayerInput.Instance.InputActions.Player.Movement.Disable();
                PlayerInput.Instance.ActivateQuestionMark(false);
                //
                // show the dialog
                UIManagerGame.Instance.DialogPanel.SetActive(true);
                UIManagerGame.Instance.DialogTitle.GetComponent<TextMeshProUGUI>().text = "";
                UIManagerGame.Instance.DialogText.GetComponent<TextMeshProUGUI>().text = textToShow[counterText];
            }
            else if (this.isInteracting) {
                this.counterText += 1;
                
                if (this.counterText == textToShow.Length) {
                    //
                    this.GetComponentInChildren<Animator>()?.SetTrigger("isOpening");
                    // close the dialog
                    counterText = 0;
                    UIManagerGame.Instance.DialogPanel.SetActive(false);
                    UIManagerGame.Instance.DialogTitle.GetComponent<TextMeshProUGUI>().text = "";
                    UIManagerGame.Instance.DialogText.GetComponent<TextMeshProUGUI>().text = textToShow[counterText];
                    //
                    this.isInteracting = false;
                    PlayerInput.Instance.isInteracting = false;
                    PlayerInput.Instance.InputActions.Player.Movement.Enable();
                    PlayerInput.Instance.HideUI();
                }
                else if (this.counterText > textToShow.Length){
                    this.counterText = 0;
                }
                else {
                    // show next dialog
                    UIManagerGame.Instance.DialogTitle.GetComponent<TextMeshProUGUI>().text = "";
                    UIManagerGame.Instance.DialogText.GetComponent<TextMeshProUGUI>().text = textToShow[counterText];
                }
            }
        }
    }
    
    private bool CanInteract() {
        if (Vector3.Distance(transform.position, PlayerInput.Instance.transform.position) >= dist) {
            return false;
        }
        if (this.textToShow.Length == 0) {
            return false;
        }
        //
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        TriggerMove();
    }

    void TriggerMove(){
        if (pathToFollow.Length > 0 && !this.isInteracting) {
            Vector3 pointVector = new Vector3(pathToFollow[pointToFollow].x, pathToFollow[pointToFollow].y, 1);

            if (pointVector == this.transform.position) {
                pointToFollow = (pointToFollow + 1) % pathToFollow.Length;
                //
                pointVector.x = pathToFollow[pointToFollow].x;
                pointVector.y = pathToFollow[pointToFollow].y;
            }
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, pointVector, moveSpeed * Time.deltaTime);
        }

    }

    private void OnDisable () {
        PlayerInput.Instance.OnInteractPerformed -= Instance_OnInteractPerformed;
    }
}
