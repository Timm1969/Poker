Imports System
Imports System.Collections.Generic
Imports System.IO.Ports
Imports System.Linq

Namespace Poker.Classes

    Public Enum HandRank
        HighCard = 1
        OnePair = 2
        TwoPair = 3
        ThreeOfAKind = 4
        Straight = 5
        Flush = 6
        FullHouse = 7
        FourOfAKind = 8
        StraightFlush = 9
        RoyalFlush = 10
    End Enum

    ''  Class to evaluate and compare poker hands
    Public Class HandRanking

        ''  Calculate the rank of a given poker hand
        Public Function CalculateHandRank(hand As List(Of Card)) As HandRank

            If IsRoyalFlush(hand) Then Return HandRank.RoyalFlush
            If (IsStraightFlush(hand)) Then Return HandRank.StraightFlush
            If (IsFourOfAKind(hand)) Then Return HandRank.FourOfAKind
            If (IsFullHouse(hand)) Then Return HandRank.FullHouse
            If (IsFlush(hand)) Then Return HandRank.Flush
            If (IsStraight(hand)) Then Return HandRank.Straight
            If (IsThreeOfAKind(hand)) Then Return HandRank.ThreeOfAKind
            If (IsTwoPair(hand)) Then Return HandRank.TwoPair
            If (IsOnePair(hand)) Then Return HandRank.OnePair
            Return HandRank.HighCard

        End Function

        ''  Compare two Four of a Kind hands
        Public Function CompareFourofaKind(playerhand1 As List(Of Card), playerhand2 As List(Of Card)) As Integer

            ' Get the ranks of the four of a kind for hand1
            Dim hand1PairRanks = playerhand1.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 4) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Get the ranks of the the four of a kind for hand2
            Dim hand2PairRanks = playerhand2.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 4) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Compare higher four of a kind
            If hand1PairRanks(0) > hand2PairRanks(0) Then Return 1 ' Hand1 wins
            If hand1PairRanks(0) < hand2PairRanks(0) Then Return -1 ' Hand2 wins

            Return 0 ' Hands are equal

        End Function

        ''  Compare two Three of a Kind (or Full House since it has the three of a kind in it) hands
        Public Function CompareThreeofaKind(playerhand1 As List(Of Card), playerhand2 As List(Of Card)) As Integer
            ' Assuming both hands are confirmed as Two Pair

            ' Get the ranks of the three of a kind for hand1
            Dim hand1PairRanks = playerhand1.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 3) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Get the ranks of the three of a kind for hand2
            Dim hand2PairRanks = playerhand2.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 3) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Compare the higher three of a kind
            If hand1PairRanks(0) > hand2PairRanks(0) Then Return 1 ' Hand1 wins
            If hand1PairRanks(0) < hand2PairRanks(0) Then Return -1 ' Hand2 wins

            Return 0 ' Hands are equal

        End Function

        ''  Compare two Two Pair hands
        Public Function CompareTwoPair(playerhand1 As List(Of Card), playerhand2 As List(Of Card)) As Integer
            ' Assuming both hands are confirmed as Two Pair

            ' Get the ranks of the pairs for hand1
            Dim hand1PairRanks = playerhand1.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 2) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Get the ranks of the pairs for hand2
            Dim hand2PairRanks = playerhand2.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 2) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Compare higher pair
            If hand1PairRanks(0) > hand2PairRanks(0) Then Return 1 ' Hand1 wins
            If hand1PairRanks(0) < hand2PairRanks(0) Then Return -1 ' Hand2 wins

            ' First pairs are equal, compare lower pair
            If hand1PairRanks(1) > hand2PairRanks(1) Then Return 1
            If hand1PairRanks(1) < hand2PairRanks(1) Then Return -1

            ' Both pairs are equal, compare remaining cards
            Dim hand1Kicker = playerhand1.Except(playerhand1.Where(Function(c) c.Value = hand1PairRanks(0) OrElse c.Value = hand1PairRanks(1))) _
                                      .OrderByDescending(Function(c) c.Value).ToList()
            Dim hand2Kicker = playerhand2.Except(playerhand2.Where(Function(c) c.Value = hand2PairRanks(0) OrElse c.Value = hand2PairRanks(1))) _
                                      .OrderByDescending(Function(c) c.Value).ToList()

            ''compare the remaining cards for high card
            Return CompareHighCard(hand1Kicker, hand2Kicker) ' Hands are equal
        End Function

        ''  Compare two One Pair hands
        Public Function CompareOnePair(playerhand1 As List(Of Card), playerhand2 As List(Of Card)) As Integer
            ' Assuming both hands are confirmed as Two Pair

            ' Get the ranks of the pair for hand1
            Dim hand1PairRanks = playerhand1.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 2) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Get the ranks of the pair for hand2
            Dim hand2PairRanks = playerhand2.GroupBy(Function(c) c.Value) _
                                      .Where(Function(g) g.Count() = 2) _
                                      .OrderByDescending(Function(g) g.Key) _
                                      .Select(Function(g) g.Key).ToList()

            ' Compare higher pair
            If hand1PairRanks(0) > hand2PairRanks(0) Then Return 1 ' Hand1 wins
            If hand1PairRanks(0) < hand2PairRanks(0) Then Return -1 ' Hand2 wins

            ' Both pairs are equal, compare remaining cards
            Dim hand1Kicker = playerhand1.Except(playerhand1.Where(Function(c) c.Value = hand1PairRanks(0))) _
                                      .OrderByDescending(Function(c) c.Value).ToList()
            Dim hand2Kicker = playerhand2.Except(playerhand2.Where(Function(c) c.Value = hand2PairRanks(0))) _
                                      .OrderByDescending(Function(c) c.Value).ToList()

            ''compare the remaining cards for high card
            Return CompareHighCard(hand1Kicker, hand2Kicker) ' Hands are equal
        End Function

        ''  Compare two High Card hands
        Public Function CompareHighCard(playerhand1 As List(Of Card), playerhand2 As List(Of Card)) As Integer
            ' Sort cards in descending order by rank
            Dim Hand1Sort = playerhand1.OrderByDescending(Function(c) c.Value).ToList()
            Dim Hand2Sort = playerhand2.OrderByDescending(Function(c) c.Value).ToList()

            For i As Integer = 0 To Hand1Sort.Count - 1
                If Hand1Sort(i).Value > Hand2Sort(i).Value Then
                    Return 1 ' Hand 1 wins
                ElseIf Hand1Sort(i).Value < Hand2Sort(i).Value Then
                    Return -1 ' Hand 2 wins
                End If
            Next

            Return 0 ' Tie
        End Function

        ''  Check for Royal Flush
        Private Function IsRoyalFlush(hand As List(Of Card)) As Boolean
            Return IsStraightFlush(hand) And hand.All(Function(Card) Card.Value >= Value.Ten)
        End Function

        ''  Check for Straight Flush
        Private Function IsStraightFlush(hand As List(Of Card)) As Boolean
            Return IsFlush(hand) And IsStraight(hand)
        End Function

        ''  Check for Four of a Kind
        Private Function IsFourOfAKind(hand As List(Of Card)) As Boolean
            Dim rankGroups = hand.GroupBy(Function(c) c.Value)
            Return rankGroups.Any(Function(group) group.Count() = 4)
        End Function

        ''  Check for Full House
        Private Function IsFullHouse(hand As List(Of Card)) As Boolean
            Dim rankGroups = hand.GroupBy(Function(c) c.Value)
            Return rankGroups.Any(Function(group) group.Count() = 3) AndAlso rankGroups.Any(Function(Group) Group.Count() = 2)
        End Function

        ''  Check for Flush
        Private Function IsFlush(hand As List(Of Card)) As Boolean
            Return hand.GroupBy(Function(group) group.Suit).Count() = 1
        End Function

        ''  Check for Straight
        Private Function IsStraight(hand As List(Of Card)) As Boolean
            Dim sortedRanks As List(Of Integer) = hand.Select(Function(card As Card) CInt(card.Value)).OrderBy(Function(rank As Integer) rank).ToList()
            If sortedRanks.Last() = CInt(Value.Ace) And sortedRanks.First() = CInt(Value.Two) Then
                sortedRanks.RemoveAt(sortedRanks.Count - 1)
                sortedRanks.Insert(0, 1)
            End If

            For i As Integer = 1 To sortedRanks.Count - 1
                If sortedRanks(i) <> sortedRanks(i - 1) + 1 Then
                    Return False
                End If
            Next

            Return True
        End Function

        ''  Check for Three of a Kind
        Private Function IsThreeOfAKind(hand As List(Of Card)) As Boolean
            Dim rankGroups = hand.GroupBy(Function(c) c.Value)
            Return rankGroups.Any(Function(group) group.Count() = 3)

        End Function

        ''  Check for Two Pair
        Private Function IsTwoPair(hand As List(Of Card)) As Boolean
            Dim rankGroups = hand.GroupBy(Function(c) c.Value)
            Return rankGroups.Count(Function(group) group.Count() = 2) = 2
        End Function

        ''  Check for One Pair
        Private Function IsOnePair(hand As List(Of Card)) As Boolean
            Dim rankGroups = hand.GroupBy(Function(c) c.Value)
            Return rankGroups.Any(Function(Group) Group.Count() = 2)
        End Function
    End Class
End Namespace
