Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks

Namespace Poker.Classes
    Public Enum Suit
        Hearts = 1
        Diamonds = 2
        Clubs = 3
        Spades = 4
    End Enum

    Public Enum Value
        Two = 2
        Three = 3
        Four = 4
        Five = 5
        Six = 6
        Seven = 7
        Eight = 8
        Nine = 9
        Ten = 10
        Jack = 11
        Queen = 12
        King = 13
        Ace = 14
    End Enum

    ''  Represents a playing card with a suit and value
    Public Class Card

        Private cardsuit As Suit
        Private cardvalue As Value

        ''  Read-only properties to access the suit and value of the card
        Public ReadOnly Property Suit As Suit
            Get
                Return cardsuit
            End Get
        End Property
        Public ReadOnly Property Value As Value
            Get
                Return cardvalue
            End Get
        End Property

        ''  Constructor to initialize a card with a specific suit and value
        Public Sub New(suit As Suit, value As Value)
            cardsuit = suit
            cardvalue = value
        End Sub

        '' Returns the URL of the card image based on its suit and value
        Public ReadOnly Property ImageURL() As String
            Get
                Return String.Format("~/Images/{0}_of_{1}.png", cardvalue, cardsuit)
            End Get
        End Property
    End Class
End Namespace
