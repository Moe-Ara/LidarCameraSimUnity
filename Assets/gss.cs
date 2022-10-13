using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gss : MonoBehaviour
{
    
    /*
     * relative_freqency= (velocity/propagation_speed_of_waves)*initial_frequency
     * 
     * https://www.parker.com/literature/Electronic%20Controls%20Division/Literature%20files/TGSS_740_instruction_book.pdf
     * 
     * https://en.wikipedia.org/wiki/Radar_speed_gun
     */
    public float initial_frequency;

    public float velocity;

    private float propagation_speed_of_waves;

    private float relative_freqency;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
