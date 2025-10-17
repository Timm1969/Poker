

Imports Poker.Poker.Classes

Public Class _Default
    Inherits System.Web.UI.Page

    ''  Deal two poker hands and evaluate the winner
    Protected Sub BtnDeal_Click(sender As Object, e As EventArgs) Handles BtnDeal.Click
        '' Create and shuffle a new deck
        Dim deck As DeckofCards = New DeckofCards()
        deck.Shuffle()

        '' Deal two hands of 5 cards each   
        Dim hand1 As List(Of Card) = deck.DealHand(5)
        Dim hand2 As List(Of Card) = deck.DealHand(5)

        '' Display the hands
        PokerHand1.DataSource = hand1
        PokerHand1.DataBind()
        PokerHand2.DataSource = hand2
        PokerHand2.DataBind()

        EvaluateWinner(hand1, hand2)

    End Sub

    ''  Evaluate the two hands and determine the winner
    Private Sub EvaluateWinner(playerhand1 As List(Of Card), playerhand2 As List(Of Card))

        '' Evaluate the hands
        Dim CompareResult As Integer
        Dim HandEvaluation As HandRanking = New HandRanking()

        Dim Hand1Rank As HandRanking.HandRank = HandEvaluation.CalculateHandRank(playerhand1)
        Hand1Result.Text = Hand1Rank.ToString()
        Dim Hand2Rank As HandRanking.HandRank = HandEvaluation.CalculateHandRank(playerhand2)
        Hand2Result.Text = Hand2Rank.ToString()


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


End Class