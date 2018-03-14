using System;

public class CardException : Exception {
    public CardException()
    {

    }
    public CardException(string message) : base(message)
    {

    }

    public CardException(string message, Exception inner) : base(message, inner)
    {

    } 
}
