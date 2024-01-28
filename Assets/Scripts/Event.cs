using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent
{
  public void Run(EventData eventData);
  public void OnComplete();
}
public class Event : MonoBehaviour, IEvent
{
  public void OnComplete()
  {
    throw new System.NotImplementedException();
  }

  public void Run(EventData eventData)
  {
    throw new System.NotImplementedException();
  }
}

public class FirstSceneEvent : MonoBehaviour, IEvent
{
  public void OnComplete()
  {
    throw new System.NotImplementedException();
  }

  public void Run(EventData eventData)
  {
    //play camera animation
    //create player at the center of the screen
    //show dialogue
    //bilbo chage expression
    //start Type first word
    //event again
    //show dialogue
    //start play first mission
  }
}
