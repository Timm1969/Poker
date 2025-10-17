

Imports Poker.Poker.Classes

Public Class _Default
    Inherits System.Web.UI.Page

    ''  Deal two poker hands and evaluate the winner
    Protected Sub BtnDeal_Click(sender As Object, e As EventArgs) Handles BtnDeal.Click
        '' Create and shuffle a new deck
        Dim deck As DeckofCards = New DeckofCards()
        deck.Shuffle()

        ' Deal two hands of 5 cards each   
        Dim hand1 As List(Of Card) = deck.DealHand(5)
        Dim hand2 As List(Of Card) = deck.DealHand(5)

        ' Display the hands
        PokerHand1.DataSource = hand1
        PokerHand1.DataBind()
        PokerHand2.DataSource = hand2
        PokerHand2.DataBind()

        EvaluateWinner(hand1, hand2)

    End Sub

    ''  Evaluate the two hands and determine the winner
    Private Sub EvaluateWinner(playerhand1 As List(Of Card), playerhand2 As List(Of Card))

        ' Evaluate the hands
        Dim CompareResult As Integer
        Dim HandEvaluation As HandRanking = New HandRanking()

        ' Get the hand ranks and display results
        Dim Hand1Rank As HandRanking.HandRank = HandEvaluation.CalculateHandRank(playerhand1)
        Hand1Result.Text = GetHandResult(Hand1Rank)
        Dim Hand2Rank As HandRanking.HandRank = HandEvaluation.CalculateHandRank(playerhand2)
        Hand2Result.Text = GetHandResult(Hand2Rank)

        ' Compare the hands
        If Hand1Rank > Hand2Rank Then
            CompareResult = 1
        ElseIf Hand2Rank > Hand1Rank Then
            CompareResult = -1
        ElseIf Hand1Rank = Hand2Rank Then
            Select Case Hand1Rank
                Case HandRanking.HandRank.FullHouse
                    CompareResult = HandEvaluation.CompareThreeofaKind(playerhand1, playerhand2)
                Case HandRanking.HandRank.FourOfAKind
                    CompareResult = HandEvaluation.CompareFourofaKind(playerhand1, playerhand2)
                Case HandRanking.HandRank.ThreeOfAKind
                    CompareResult = HandEvaluation.CompareThreeofaKind(playerhand1, playerhand2)
                Case HandRanking.HandRank.TwoPair
                    CompareResult = HandEvaluation.CompareTwoPair(playerhand1, playerhand2)
                Case HandRanking.HandRank.OnePair
                    CompareResult = HandEvaluation.CompareOnePair(playerhand1, playerhand2)
                Case Else
                    CompareResult = HandEvaluation.CompareHighCard(playerhand1, playerhand2) ' Default for flushes, straights, high card
            End Select
        End If

        If CompareResult = 1 Then
            HandWinner.Text = "Player 1 Wins!"
        ElseIf CompareResult = -1 Then
            HandWinner.Text = "Player 2 Wins!"
        Else
            HandWinner.Text = "It's a Tie!"
        End If

    End Sub

    ''  Get the string representation of a hand rank
    Private Function GetHandResult(handrank As HandRanking.HandRank)
        Dim result As String = String.Empty

        Select Case handrank
            Case HandRanking.HandRank.HighCard
                result = "High Card"
            Case HandRanking.HandRank.OnePair
                result = "One Pair"
            Case HandRanking.HandRank.TwoPair
                result = "Two Pair"
            Case HandRanking.HandRank.ThreeOfAKind
                result = "Three of a Kind"
            Case HandRanking.HandRank.Straight
                result = "Straight"
            Case HandRanking.HandRank.Flush
                result = "Flush"
            Case HandRanking.HandRank.FullHouse
                result = "Full House"
            Case HandRanking.HandRank.FourOfAKind
                result = "Four of a Kind"
            Case HandRanking.HandRank.StraightFlush
                result = "Straight Flush"
            Case HandRanking.HandRank.RoyalFlush
                result = "Royal Flush"
        End Select

        Return result
    End Function
End Class