using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMultiblock : IBlock {

    IBlock[] MultiBlockOthers { get; }

    

}
