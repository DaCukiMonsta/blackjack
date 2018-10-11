Public Class Form1
    'define all the things
    Const minbet As Decimal = 1

    Dim rnd = New Random()

    Dim hasStood As Boolean = False

    Dim bank As Decimal = 10 'initial bank value
    Dim bet As Decimal

    Dim deck As New Dictionary(Of String, Image)
    Dim fulldeck As New Dictionary(Of String, Image)
    Dim mycards As New Dictionary(Of String, Image)
    Dim dealercards As New Dictionary(Of String, Image)
    'all the things have been defined

    'resets the deck and adds a standard set of cards to it
    Sub populate_deck()
        mycards.Clear()
        dealercards.Clear()
        deck.Clear()
        cardImages.Images.Clear()
        deck.Add("AH", My.Resources.AH)
        deck.Add("2H", My.Resources._2H)
        deck.Add("3H", My.Resources._3H)
        deck.Add("4H", My.Resources._4H)
        deck.Add("5H", My.Resources._5H)
        deck.Add("6H", My.Resources._6H)
        deck.Add("7H", My.Resources._7H)
        deck.Add("8H", My.Resources._8H)
        deck.Add("9H", My.Resources._9H)
        deck.Add("10H", My.Resources._10H)
        deck.Add("JH", My.Resources.JH)
        deck.Add("QH", My.Resources.QH)
        deck.Add("KH", My.Resources.KH)
        deck.Add("AC", My.Resources.AC)
        deck.Add("2C", My.Resources._2C)
        deck.Add("3C", My.Resources._3C)
        deck.Add("4C", My.Resources._4C)
        deck.Add("5C", My.Resources._5C)
        deck.Add("6C", My.Resources._6C)
        deck.Add("7C", My.Resources._7C)
        deck.Add("8C", My.Resources._8C)
        deck.Add("9C", My.Resources._9C)
        deck.Add("10C", My.Resources._10C)
        deck.Add("JC", My.Resources.JC)
        deck.Add("QC", My.Resources.QC)
        deck.Add("KC", My.Resources.KC)
        deck.Add("AD", My.Resources.AD)
        deck.Add("2D", My.Resources._2D)
        deck.Add("3D", My.Resources._3D)
        deck.Add("4D", My.Resources._4D)
        deck.Add("5D", My.Resources._5D)
        deck.Add("6D", My.Resources._6D)
        deck.Add("7D", My.Resources._7D)
        deck.Add("8D", My.Resources._8D)
        deck.Add("9D", My.Resources._9D)
        deck.Add("10D", My.Resources._10D)
        deck.Add("JD", My.Resources.JD)
        deck.Add("QD", My.Resources.QD)
        deck.Add("KD", My.Resources.KD)
        deck.Add("AS", My.Resources._AS)
        deck.Add("2S", My.Resources._2S)
        deck.Add("3S", My.Resources._3S)
        deck.Add("4S", My.Resources._4S)
        deck.Add("5S", My.Resources._5S)
        deck.Add("6S", My.Resources._6S)
        deck.Add("7S", My.Resources._7S)
        deck.Add("8S", My.Resources._8S)
        deck.Add("9S", My.Resources._9S)
        deck.Add("10S", My.Resources._10S)
        deck.Add("JS", My.Resources.JS)
        deck.Add("QS", My.Resources.QS)
        deck.Add("KS", My.Resources.KS)
        For Each pair As KeyValuePair(Of String, Image) In deck
            cardImages.Images.Add(pair.Key, pair.Value)
        Next
        cardImages.Images.Add("back", My.Resources.yellow_back)
        ListView1.LargeImageList = cardImages
        ListView1.SmallImageList = cardImages
        ListView2.LargeImageList = cardImages
        ListView2.SmallImageList = cardImages
        fulldeck = deck
    End Sub
    'selects a random card returns it and removes it from the deck
    Function draw_card() As KeyValuePair(Of String, Image)
        Try
            Dim key = deck.Keys.ToList(rnd.Next(0, deck.Keys.ToList.Count))
            For Each pair As KeyValuePair(Of String, Image) In deck
                If pair.Key = key Then
                    deck.Remove(key)
                    Return pair
                End If
            Next
        Catch ex As Exception
            MsgBox("i seem to have ran out of cards lmao rip")
            End
        End Try
    End Function
    'picks a random card from the deck but does not remove it
    'card peek
    'unused but why not leave it in
    Function rand_card() As KeyValuePair(Of String, Image)
        Dim key = deck.Keys.ToList(rnd.Next(0, deck.Keys.ToList.Count))
        For Each pair As KeyValuePair(Of String, Image) In deck
            If pair.Key = key Then
                Return pair
            End If
        Next
    End Function
    'updates GUI controls with bank value
    Sub update_bank()
        'round bank
        bank = Decimal.Round(bank, 2, MidpointRounding.AwayFromZero)
        'update the stuff
        NumericUpDown1.Maximum = bank
        Label1.Text = "Bank: £" + bank.ToString("0.00")
    End Sub
    'this function is largely irrelevant but I added it in because sophie was complaining
    'when i suggested adding it
    Function get_english_card_name(ByVal name As String)
        Dim suit = name.Substring(name.Length - 1)
        Dim val = name.Substring(0, name.Length - 1)
        Select Case suit
            Case "D"
                suit = "Diamonds"
            Case "H"
                suit = "Hearts"
            Case "C"
                suit = "Clubs"
            Case "S"
                suit = "Spades"
        End Select
        Select Case val
            Case "A"
                val = "Ace"
            Case "J"
                val = "Jack"
            Case "Q"
                val = "Queen"
            Case "K"
                val = "King"
        End Select
        Return val + " of " + suit
    End Function
    'gives the user a card
    Sub give_user_card()
        Dim card As KeyValuePair(Of String, Image)
        card = draw_card()
        mycards.Add(card.Key, card.Value)
        ListView1.Items.Add(get_english_card_name(card.Key), card.Key)
    End Sub
    'gives the dealer a card
    Sub give_dealer_card()
        Dim card As KeyValuePair(Of String, Image)
        card = draw_card()
        dealercards.Add(card.Key, card.Value)
        If Not hasStood Then
            If dealercards.Count = 1 Then
                ListView2.Items.Add(get_english_card_name(card.Key), card.Key)
            Else
                ListView2.Items.Add("???", "back")
            End If
        Else
            ListView2.Items.Add(get_english_card_name(card.Key), card.Key)
        End If
    End Sub
    'place bet
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If NumericUpDown1.Value >= minbet Then
            bet = NumericUpDown1.Value
            NumericUpDown1.Enabled = False
            Button1.Enabled = False
            GroupBox1.Enabled = True
            give_user_card()
            give_user_card()
            give_dealer_card()
            give_dealer_card()
            'check blackjacks
            If cards_total(mycards) = 21 Then
                If cards_total(dealercards) = 21 Then
                    'tie, give money back
                    win(0)
                Else
                    'you win lmao
                    win(1)
                End If
            Else
                If cards_total(dealercards) = 21 Then
                    'dealer wins lol rip
                    bust()
                End If
            End If
            GroupBox2.Enabled = True
            status.Text = "Waiting for user action"
        Else
            MessageBox.Show("Invalid bet. You must bet at least £" + minbet.ToString("0.00"), "Invalid bet", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        populate_deck()
        update_bank()
        'enable all the buttons
        NumericUpDown1.Minimum = minbet
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        ListView1.Items.Clear()
        ListView2.Items.Clear()
        hasStood = False
        status.Text = "Waiting for bet to be placed"
    End Sub

    Function cards_total(ByVal cards As Dictionary(Of String, Image))
        Dim total As Integer = 0
        Dim aces As Integer = 0
        For Each pair As KeyValuePair(Of String, Image) In cards
            Dim val As String = pair.Key.Substring(0, pair.Key.Length - 1)
            If IsNumeric(val) Then
                total += Convert.ToInt32(val)
            ElseIf val = "K" Or val = "Q" Or val = "J" Then
                total += 10
            ElseIf val = "A" Then
                total += 11
                aces += 1
            End If
        Next
        If total > 21 And aces > 0 Then
            For i As Integer = 1 To aces
                total -= 10
                If total <= 21 Then
                    Return total
                End If
            Next
        End If
        Return total
    End Function

    Sub bust(Optional ByVal total As String = "")
        bank -= bet
        update_bank()
        If total = "" Then
            MessageBox.Show("The dealer beat you ", "Lmao rip", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            MessageBox.Show("Your total was " + total + ", and you went bust! Thanks for the money bro x", "Lmao rip", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
        If bank < minbet Then
            MessageBox.Show("You have no money left. Get out of my casino", "Get out of my casino", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.Close()
        End If
        NumericUpDown1.Enabled = True
        Button1.Enabled = True
        GroupBox1.Enabled = False
        GroupBox2.Enabled = False
        'reset everything
        Form1_Load(New System.Object, New System.EventArgs)
    End Sub

    Sub win(ByVal multip As Decimal)
        bank += bet * multip
        update_bank()
        MessageBox.Show("You just won £" + (bet * multip).ToString("0.00") + ". Well done", "Ayyy nice one bro", MessageBoxButtons.OK, MessageBoxIcon.Information)
        NumericUpDown1.Enabled = True
        Button1.Enabled = True
        GroupBox1.Enabled = False
        GroupBox2.Enabled = False
        'reset everything
        Form1_Load(New System.Object, New System.EventArgs)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'hit
        give_user_card()
        If cards_total(mycards) > 21 Then
            'bust
            bust(cards_total(mycards))
        Else
            'disable double down
            Button4.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Button4.Enabled = False
        'stand
        If Button4.Enabled Then 'button 4 is disabled after the first action, so this basically means "is it the first action"
            If cards_total(mycards) = 21 Then
                'win back original + *1.5
                win(1.5)
            Else
                'regular stand
                stand()
            End If
        Else 'regular stand
            stand()
        End If
    End Sub

    Sub stand()
        If Not hasStood Then
            hasStood = True
            'disable all the buttons
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            'show dealers cards
            ListView2.Items.Clear()
            For Each card As KeyValuePair(Of String, Image) In dealercards
                ListView2.Items.Add(get_english_card_name(card.Key), card.Key)
            Next
        End If
        If cards_total(dealercards) < 17 Then
            give_dealer_card()
            stand()
        Else
            If cards_total(dealercards) > 21 Then
                win(1)
            Else
                If cards_total(mycards) > cards_total(dealercards) Then
                    win(1)
                ElseIf cards_total(mycards) < cards_total(dealercards) Then
                    bust()
                Else
                    win(0)
                End If
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        'double down
        If bet * 2 > bank Then
            MessageBox.Show("You can't do that. You don't have enough money rip", "No", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            bet = bet * 2
            NumericUpDown1.Value = bet
            give_user_card()
            If cards_total(mycards) > 21 Then
                'bust
                bust(cards_total(mycards))
            Else
                'disable double down
                Button4.Enabled = False
                stand()
            End If
        End If
    End Sub

    Private Sub ListView2_Click(sender As System.Object, e As System.EventArgs) Handles ListView2.Click
        MessageBox.Show("You assaulted the dealer. Get out of my casino", "DONT TOUCH THE DEALERS CARDS", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Me.Close()
    End Sub
End Class
