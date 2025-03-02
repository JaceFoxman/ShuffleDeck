Option Compare Text
Option Explicit On
Option Strict On
'Jason Permann
'Spring 2025
'RCET2265
'Shffle The Deck
'
Imports System.ComponentModel.Design

Module ShuffleDeck

    Sub Main()
        Dim userInput As String
        Dim _lastBall(1) As Integer
        Do
            Console.Clear()
            DisplayDeck()
            Console.WriteLine("")

            Console.WriteLine("Enter D to draw a ball, C for new game, or Q to quit.")
            userInput = Console.ReadLine()
            Console.WriteLine($"Your card is {Formating(0, 0)}")
            Select Case userInput
                Case "d"
                    DrawCard()
                Case "c"
                    DeckTracker(0, 0,, True) '0,0 dont matter because it just needs something in there sense they are not optional variable, True at the end is the only thing that matters sense it clears.
                    DrawCard(True)
                Case Else

            End Select

        Loop Until userInput = "q"
        Console.Clear()
        Console.WriteLine("Have a nice day!")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="clearCount"></param>
    Sub DrawCard(Optional clearCount As Boolean = False)
        Dim temp(,) As Boolean = DeckTracker(0, 0) 'create a local copy of Deck tracker
        Dim currentCardNumber As Integer
        Dim currentSuite As Integer
        Static cardCounter As Integer

        If clearCount Then
            cardCounter = 0
        Else
            'loop until the current random Card has not already been marked down
            Do
                currentCardNumber = RNG(0, 12) 'get row A,2-10,J,Q,K
                currentSuite = RNG(0, 3) 'get column Hearts,Diamond,Spade,Club
            Loop Until temp(currentCardNumber, currentSuite) = False Or cardCounter >= 52

            'mark current Card as being drawn, updates the display
            DeckTracker(currentCardNumber, currentSuite, True)
            cardCounter += 1
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Sub DisplayDeck()
        Dim displayString As String = " |"
        Dim heading() As String = {" D |", " H |", " C |", " S | "}
        Dim tracker(,) As Boolean = DeckTracker(0, 0)
        Dim columWidth As Integer = 4
        For Each letter In heading
            Console.Write(letter)
        Next

        Console.WriteLine()
        Console.WriteLine(StrDup(columWidth * 4, "_"))

        For currentNumber = 0 To 12

            For CurrentSuite = 0 To 3
                If tracker(currentNumber, CurrentSuite) Then
                    displayString = $"{Formating(currentNumber, CurrentSuite)} |"
                Else
                    displayString = "|"
                End If
                displayString = displayString.PadLeft(columWidth)
                Console.Write(displayString)
            Next
            Console.WriteLine()
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cardNumber"></param>
    ''' <param name="suite"></param>
    ''' <param name="update"></param>
    ''' <param name="clear"></param>
    ''' <returns></returns>
    Function DeckTracker(cardNumber As Integer, suite As Integer,
                          Optional update As Boolean = False,
                          Optional clear As Boolean = False) As Boolean(,)
        'static allows other functions and subs to see a copy of _cardTracker
        Static _cardTracker(12, 3) As Boolean

        If update Then
            _cardTracker(cardNumber, suite) = True
        End If
        If clear Then
            ReDim _cardTracker(12, 3) 'clears the array.
        End If
        Return _cardTracker
    End Function

    ''' <summary>
    ''' Gather values form DrawCard then converts each number 
    ''' </summary>
    ''' <param name="cardNumber"></param>
    ''' <param name="Suite"></param>
    ''' <returns></returns>
    Function Formating(cardNumber As Integer, Suite As Integer) As String
        Dim cardValue As String

        Select Case cardNumber

            Case 0
                cardValue = "A"
            Case 1 To 9
                cardValue = CStr(cardNumber + 1)
            Case 10 To 12
                If cardNumber = 10 Then
                    cardValue = "J"
                ElseIf cardNumber = 11 Then
                    cardValue = "Q"
                ElseIf cardNumber = 12 Then
                    cardValue = "K"
                End If
        End Select

        Return cardValue
    End Function

    ''' <summary>
    ''' Generate a random number
    ''' </summary>
    ''' <param name="Min"></param>
    ''' <param name="Max"></param>
    ''' <returns></returns>
    Function RNG(Min As Integer, Max As Integer) As Integer
        Dim randomNumber As Single
        Randomize()
        randomNumber = Rnd()
        randomNumber *= Max + Min
        randomNumber += Min
        Return CInt(randomNumber)
    End Function

End Module
