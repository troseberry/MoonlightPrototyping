using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerResponseType { POSITIVE, NEUTRAL, NEGATIVE, NO_RESPONSE}

public class PlayerResponeController : MonoBehaviour
{
    public void GivePositiveResponse()
    {
        MessageAppController.Instance.CreateNewMessage(MessageType.OUTGOING, "sent positive response", "n/a");
        StartCoroutine(RespondToPlayerResponse(PlayerResponseType.POSITIVE));
    }

    public void GiveNeutralResponse()
    {
        MessageAppController.Instance.CreateNewMessage(MessageType.OUTGOING, "sent neutral response", "n/a");
        StartCoroutine(RespondToPlayerResponse(PlayerResponseType.NEUTRAL));
    }

    public void GiveNegativeResponse()
    {
        MessageAppController.Instance.CreateNewMessage(MessageType.OUTGOING, "sent negative response", "n/a");
        StartCoroutine(RespondToPlayerResponse(PlayerResponseType.NEGATIVE));
    }

    public void GiveNoResponse()
    {
        // MessageAppController.Instance.CreateNewMessage(MessageType.OUTGOING, "sent no response", "n/a");
        StartCoroutine(RespondToPlayerResponse(PlayerResponseType.NO_RESPONSE));
    }

    IEnumerator RespondToPlayerResponse(PlayerResponseType type)
    {
        string[] response = {"Contact response", "response type: " + type.ToString(), "response content here"};
        int randomMessageCount = Random.Range(0,3);
        // Debug.Log("Random Int: " + randomMessageCount);

        yield return new WaitForSeconds(Random.Range(0f, 1f));
        
        switch(randomMessageCount)
        {
            case 0:
                string contentZero = response[0] + System.Environment.NewLine
                + response[1] + System.Environment.NewLine
                + response[2];
                MessageAppController.Instance.CreateNewMessage(MessageType.INCOMING, contentZero, "n/a");
                break;
            case 1:
                string contentOne = response[0] + System.Environment.NewLine + response[1];
                MessageAppController.Instance.CreateNewMessage(MessageType.INCOMING, contentOne, "n/a");
                yield return new WaitForSeconds(Random.Range(0f, 0.5f));
                MessageAppController.Instance.CreateNewMessage(MessageType.INCOMING, response[2], "n/a");
                break;
            case 2:
                for (int i = 0; i < randomMessageCount + 1; i++)
                {
                    MessageAppController.Instance.CreateNewMessage(MessageType.INCOMING, response[i], "n/a");
                    yield return new WaitForSeconds(Random.Range(0f, 0.5f));
                }
                break;
        } 
    }
}
