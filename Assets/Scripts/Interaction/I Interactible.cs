using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{

    bool Inspect { get; }
    bool Examine { get; }

    GameObject Icon { get; }

    GameObject ExamineCam { get; }

    GameObject Puzzle { get; }

    public bool Interact(Interactor interact);

    

  
}

