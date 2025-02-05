using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerGame : MonoBehaviour
{
    [SerializeField] public GameObject DialogPanel;
    [SerializeField] public GameObject DialogTitle;
    [SerializeField] public GameObject DialogText;

    public static UIManagerGame Instance { get; private set; }

    void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
