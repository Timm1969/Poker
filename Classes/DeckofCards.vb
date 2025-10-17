Imports Poker.Poker.Classes

'' Represents a standard deck of 52 playing cards
Public Class DeckofCards
    Public Property Deck As New List(Of Card)

    '' Initialize a new deck of 52 playing cards
    Public Sub New()
        For Each suitValue As Suit In [Enum].GetValues(GetType(Suit))
            ' Loop through each Rank
            For Each rankValue As Value In [Enum].GetValues(GetType(Value))
                ' Create a new Card object and add it to the deck
                Dim card As New Card(suitValue, rankValue)
                Deck.Add(card)
            Next
        Next
    End Sub

    '' Shuffle the deck 
    Public Sub Shuffle()
        Dim rndNumber As New Random()
        Dim deckCount As Integer = Me.Deck.Count

        While deckCount > 1
            deckCount -= 1
            Dim cardIndex As Integer = rndNumber.Next(deckCount + 1)
            Dim tmpCard As Card = Me.Deck(cardIndex)
            Me.Deck(cardIndex) = Me.Deck(deckCount)
            Me.Deck(deckCount) = tmpCard
        End While
    End Sub

    '' Deal a hand of specified number of cards
    Public Function DealHand(ByVal numCards As Integer) As List(Of Card)
        Dim hand As New List(Of Card)
        For i As Integer = 0 To numCards - 1
            Dim cardToDeal As Card = Me.Deck(0)
            hand.Add(cardToDeal)
            Me.Deck.RemoveAt(0)
        Next
        Return hand
    End Function
End Class
