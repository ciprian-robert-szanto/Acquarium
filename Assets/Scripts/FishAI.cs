using System.Collections; // Importa namespace per le collezioni base
using System.Collections.Generic; // Importa namespace per collezioni generiche
using TMPro; // Importa namespace per TextMeshPro (testi UI)
using UnityEngine; // Importa namespace base di Unity

// Definisce la classe FishAI che eredita da MonoBehaviour (classe base per script Unity)
public class FishAI : MonoBehaviour
{
    
    // Serialized fields visibili nell'Inspector di Unity    
    [SerializeField] private string titleDialog; // Titolo del dialogo
    [SerializeField] private float dist;
    [SerializeField] private float moveSpeed; // Velocità di movimento del pesce

    [SerializeField] private Vector2[] pathToFollow; // Array di punti per il movimento
    private int pointToFollow;  // Indice del punto corrente da raggiungere

    [SerializeField] private string[] textToShow; // Array di testi per il dialogo
    [SerializeField] private GameObject rewardObject; // Oggetto ricompensa
    private int counterText = 0; // Contatore per scorrere i testi del dialogo

    [SerializeField]
    private DialogConfiguration dialogConfiguration;

    private bool isInteracting = false; // Flag per lo stato di interazione

    // Metodo chiamato all'avvio dello script
    void Start()
    {
        pointToFollow = 0;  // Inizia dal primo punto del percorso
        // Aggiunge un listener all'evento di interazione
        PlayerInput.Instance.OnInteractPerformed += Instance_OnInteractPerformed;
    }

    // Metodo gestore dell'evento di interazione
    private void Instance_OnInteractPerformed(object sender, System.EventArgs e) {
        // Controlla se l'interazione è possibile
        if (CanInteract()) {
            // Se non si sta già interagendo
            if (!PlayerInput.Instance.isInteracting) {
                this.isInteracting = true; // Imposta stato di interazione
                PlayerInput.Instance.isInteracting = true;
                PlayerInput.Instance.InputActions.Player.Movement.Disable(); // Disabilita movimento giocatore
                PlayerInput.Instance.ActivateQuestionMark(false); // Nascondi punto interrogativo
                //
                // // Mostra pannello dialogo
                // UIManagerGame.Instance.DialogPanel.SetActive(true);
                // // Imposta titolo dialogo
                // UIManagerGame.Instance.DialogTitle.GetComponent<TextMeshProUGUI>().text = titleDialog;
                // // Mostra primo testo del dialogo
                // UIManagerGame.Instance.DialogText.GetComponent<TextMeshProUGUI>().text = textToShow[counterText];

                if (dialogConfiguration) {
                    GameObject.FindGameObjectWithTag("DialogController").GetComponent<DialogPanelController>().StartConversation(dialogConfiguration, HandleDialogConversationCompleted);
                }
            }
            // Se già in interazione
            else if (this.isInteracting) {
                // GameObject.FindGameObjectWithTag("DialogController").GetComponent<DialogPanelController>().
                // this.counterText += 1; // Passa al prossimo testo
                
                // // Se sono finiti i testi
                // if (this.counterText == textToShow.Length) {
                //     // Mostra oggetto ricompensa se present
                //     if(this.rewardObject != null){
                //         this.rewardObject.SetActive(true);
                //         this.rewardObject.GetComponentInChildren<Animator>().SetTrigger("rewardTrigger");
                //     }
                    
                //     // Resetta e chiudi dialogo
                //     counterText = 0;
                //     UIManagerGame.Instance.DialogPanel.SetActive(false);
                //     UIManagerGame.Instance.DialogTitle.GetComponent<TextMeshProUGUI>().text = titleDialog;
                //     UIManagerGame.Instance.DialogText.GetComponent<TextMeshProUGUI>().text = textToShow[counterText];
                    
                //     // Ripristina stato e controlli
                //     this.isInteracting = false;
                //     PlayerInput.Instance.isInteracting = false;
                //     PlayerInput.Instance.InputActions.Player.Movement.Enable();
                //     PlayerInput.Instance.HideUI();
                //     // Distruggi oggetto ricompensa dopo un po'
                //     if (this.rewardObject != null) {
                //         Destroy(this.rewardObject, 1.5f);
                //         this.rewardObject = null;
                //     }
                // }

                // // Se il contatore supera la lunghezza dei testi, riparti da zero
                // else if (this.counterText > textToShow.Length){
                //     this.counterText = 0;
                // }
                // else {
                //     // Mostra il prossimo testo del dialogo
                //     UIManagerGame.Instance.DialogTitle.GetComponent<TextMeshProUGUI>().text = titleDialog;
                //     UIManagerGame.Instance.DialogText.GetComponent<TextMeshProUGUI>().text = textToShow[counterText];
                // }
            }
        }
    }
    private void HandleDialogConversationCompleted() {
        this.isInteracting = false; // Imposta stato di interazione
        PlayerInput.Instance.isInteracting = false;
        PlayerInput.Instance.InputActions.Player.Movement.Enable(); // Disabilita movimento giocatore
        PlayerInput.Instance.ActivateQuestionMark(false); // Nascondi punto interrogativo
    }
    
    // Metodo per verificare se l'interazione è possibile
    private bool CanInteract() {
        // Controlla la distanza dal giocatore: se maggiore non interagisce
        if (Vector3.Distance(transform.position, PlayerInput.Instance.transform.position) >= dist) {
            return false;
        }
        // Controlla se ci sono testi di dialogo; se non ci sono non interagisce !!!
        if (this.textToShow.Length == 0) {
            return false;
        }
        // altrimenti interagisce
        return true;
    }

    // Update is called ogni frame
    void Update()
    {
        TriggerMove(); // aggiorna il movimento
    }


    // Metodo per gestire il movimento
    void TriggerMove(){
        // Se ci sono punti da seguire e non si sta interagendo
        if (pathToFollow.Length > 0 && !this.isInteracting) {
            // Converti punto corrente in Vector3
            Vector3 pointVector = new Vector3(pathToFollow[pointToFollow].x, pathToFollow[pointToFollow].y, 1);

            // Se raggiunto il punto, passa al successivo
            if (pointVector == this.transform.position) {
                // vai la punto estremo
                pointToFollow = (pointToFollow + 1) % pathToFollow.Length;
                // Aggiorna coordinate punto
                pointVector.x = pathToFollow[pointToFollow].x;
                pointVector.y = pathToFollow[pointToFollow].y;
            }

            // Muovi verso il punto
            this.transform.position = Vector3.MoveTowards(this.transform.position, pointVector, moveSpeed * Time.deltaTime);
        }

    }

    // Metodo chiamato quando lo script viene disabilitato
    private void OnDisable () {
        // Rimuovi il listener dell'evento
        PlayerInput.Instance.OnInteractPerformed -= Instance_OnInteractPerformed;
    }
}
