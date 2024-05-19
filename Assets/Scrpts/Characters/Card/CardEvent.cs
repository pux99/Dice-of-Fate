using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : Card
{
    [SerializeField] private string _cardText;
    public string cardText { get { return _cardText; } }
    [Serializable]
    public struct Options
    {
        [SerializeField] private string _buttonText;
        [SerializeField] private string _text;
        [SerializeField] private bool _roll;
        [SerializeField] private Rewards _options;

        public string buttonText { get { return _buttonText; } }
        public string text { get { return _text; } }
        public bool roll { get { return _roll; } }
        public Rewards options { get { return _options; } }
    }
    public List<Options> options;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}